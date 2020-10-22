using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

using api.Models;

namespace api.Swagger
{
    public class GraphEndpoint : IDocumentFilter
    {
        public const string graphEndpoint = @"/graphql";

        public const string query = "{\"query\":\"" +
        "query GetProject{projects {id, fusionProjectId, createDate, " +
        "evaluations {createDate, id, participants " +
        "{organization}, progression, id, questions {text, actions {title}}}}}\"}";

        public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
        {
            var pathItem = new OpenApiPathItem();

            var operation = new OpenApiOperation();
            operation.Tags.Add(new OpenApiTag { Name = "GraphQL" });
            operation.RequestBody = new OpenApiRequestBody()
            {
                Content = new Dictionary<string, OpenApiMediaType> {
                    {"application/json",
                    new OpenApiMediaType()
                    {
                        Schema = context.SchemaGenerator
                        .GenerateSchema(typeof(Project), context.SchemaRepository),
                        Example = new OpenApiString(query)
                    }
                    }
                }
            };

            pathItem.AddOperation(OperationType.Post, operation);
            openApiDocument?.Paths.Add(graphEndpoint, pathItem);
        }
    }

}
