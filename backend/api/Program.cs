using System;
using Microsoft.AspNetCore.Authorization;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

var builder = WebApplication.CreateBuilder(args);
var azureAppConfigConnectionString =
    builder.Configuration.GetSection("AppConfiguration").GetValue<string>("ConnectionString");
var environment = builder.Configuration.GetSection("AppConfiguration").GetValue<string>("Environment");
Console.WriteLine("Loading config for: " + environment);

var configBuilder = new ConfigurationBuilder();
configBuilder.AddAzureAppConfiguration(options =>
    options
        .Connect(azureAppConfigConnectionString)
        .ConfigureKeyVault(x => x.SetCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions
            { ExcludeSharedTokenCacheCredential = true })))
        .Select(KeyFilter.Any)
        .Select(KeyFilter.Any, environment)
);

var config = configBuilder.Build();
builder.Configuration.AddConfiguration(config);

var sqlConnectionString = config["Database:ConnectionString"];
if (string.IsNullOrEmpty(sqlConnectionString))
{
    var dbContextOptionsBuilder = new DbContextOptionsBuilder<BmtDbContext>();
    sqlConnectionString = new SqliteConnectionStringBuilder()
        { DataSource = "file::memory:", Mode = SqliteOpenMode.ReadWriteCreate, Cache = SqliteCacheMode.Shared }.ToString();

    // In-memory sqlite requires an open connection throughout the whole lifetime of the database
    var connectionToInMemorySqlite = new SqliteConnection(sqlConnectionString);
    connectionToInMemorySqlite.Open();
    dbContextOptionsBuilder.UseSqlite(connectionToInMemorySqlite);

    using var context = new BmtDbContext(dbContextOptionsBuilder.Options);
    context.Database.EnsureCreated();
    InitContent.PopulateDb(context);
}

builder.Services.AddRouting();
builder.Services.AddMicrosoftIdentityWebApiAuthentication(config);
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

const string accessControlPolicyName = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(accessControlPolicyName, policyBuilder =>
    {
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5000",
            "http://localhost:5000/graphql",
            "https://fusion.equinor.com",
            "https://pro-s-portal-ci.azurewebsites.net",
            "https://pro-s-portal-fqa.azurewebsites.net",
            "https://pro-s-portal-fprd.azurewebsites.net"
        ).SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

if (environment == "localdev")
{
    builder.Services.AddDbContext<BmtDbContext>(
        options => options.UseSqlServer(sqlConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
    );
} else
{
    builder.Services.AddDbContext<BmtDbContext>(
        options => options.UseSqlite(sqlConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
    );
}

builder.Services.AddErrorFilter<ErrorFilter>();

builder.Services.AddScoped<GraphQuery>();
builder.Services.AddScoped<Mutation>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ParticipantService>();
builder.Services.AddScoped<EvaluationService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<AnswerService>();
builder.Services.AddScoped<ActionService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<ClosingRemarkService>();
builder.Services.AddScoped<QuestionTemplateService>();
builder.Services.AddScoped<ProjectCategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddGraphQLServer()
    .AddProjections()
    .AddAuthorization()
    .AddFiltering()
    .AddQueryType<GraphQuery>()
    .AddMutationType<Mutation>();

builder.Services.AddControllers();

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddHealthChecks().AddCheck<EvaluationService>("ModelsFromDB");

builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme

    {
        Type = SecuritySchemeType.OAuth2,

        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"{config["AzureAd:Instance"]}/{config["AzureAd:TenantId"]}/oauth2/token"),
                AuthorizationUrl = new Uri($"{config["AzureAd:Instance"]}/{config["AzureAd:TenantId"]}/oauth2/authorize"),
                Scopes = { { $"api://{config["AzureAd:ClientId"]}/user_impersonation", "User Impersonation" } },
            },
        },

    });
    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
            },
            Array.Empty<string>()
        },
    });
    swaggerGenOptions.DocumentFilter<GraphEndpoint>();
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(accessControlPolicyName);

if (environment == "localdev")
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
    options.OAuthAppName("Fusion-BMT");
    options.OAuthClientId(config["AzureAd:ClientId"]);
    options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
        { { "resource", $"{config["AzureAd:ClientId"]}" } });
});

app.MapHealthChecks("/health").AllowAnonymous();
app.MapGraphQL();
app.MapControllers();

app.Run();
