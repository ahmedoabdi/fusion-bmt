﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Context;

namespace api.Migrations
{
    [DbContext(typeof(BmtDbContext))]
    [Migration("20211015080654_AddAdminOrderToQuestionTemplate")]
    partial class AddAdminOrderToQuestionTemplate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ProjectCategoryQuestionTemplate", b =>
                {
                    b.Property<string>("ProjectCategoriesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestionTemplatesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProjectCategoriesId", "QuestionTemplatesId");

                    b.HasIndex("QuestionTemplatesId");

                    b.ToTable("ProjectCategoryQuestionTemplate");
                });

            modelBuilder.Entity("api.Models.Action", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssignedToId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsVoided")
                        .HasColumnType("bit");

                    b.Property<bool>("OnHold")
                        .HasColumnType("bit");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("QuestionId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("api.Models.Answer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AnsweredById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Progression")
                        .HasColumnType("int");

                    b.Property<string>("QuestionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Severity")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnsweredById");

                    b.HasIndex("QuestionId", "AnsweredById", "Progression")
                        .IsUnique()
                        .HasFilter("[AnsweredById] IS NOT NULL");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("api.Models.ClosingRemark", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("CreatedById");

                    b.ToTable("ClosingRemarks");
                });

            modelBuilder.Entity("api.Models.Evaluation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousEvaluationId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Progression")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("WorkshopCompleteDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("api.Models.Note", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("api.Models.Participant", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AzureUniqueId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EvaluationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Organization")
                        .HasColumnType("int");

                    b.Property<int>("Progression")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId");

                    b.HasIndex("AzureUniqueId", "EvaluationId")
                        .IsUnique();

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("api.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FusionProjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("api.Models.ProjectCategory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectCategories");
                });

            modelBuilder.Entity("api.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Barrier")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EvaluationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Organization")
                        .HasColumnType("int");

                    b.Property<string>("QuestionTemplateId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SupportNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationId");

                    b.HasIndex("QuestionTemplateId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("api.Models.QuestionTemplate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AdminOrder")
                        .HasColumnType("int");

                    b.Property<int>("Barrier")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Organization")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SupportNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("previousId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("previousId");

                    b.ToTable("QuestionTemplates");
                });

            modelBuilder.Entity("ProjectCategoryQuestionTemplate", b =>
                {
                    b.HasOne("api.Models.ProjectCategory", null)
                        .WithMany()
                        .HasForeignKey("ProjectCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.QuestionTemplate", null)
                        .WithMany()
                        .HasForeignKey("QuestionTemplatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Action", b =>
                {
                    b.HasOne("api.Models.Participant", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.HasOne("api.Models.Participant", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("api.Models.Question", "Question")
                        .WithMany("Actions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("CreatedBy");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("api.Models.Answer", b =>
                {
                    b.HasOne("api.Models.Participant", "AnsweredBy")
                        .WithMany()
                        .HasForeignKey("AnsweredById");

                    b.HasOne("api.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnsweredBy");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("api.Models.ClosingRemark", b =>
                {
                    b.HasOne("api.Models.Action", "Action")
                        .WithMany("ClosingRemarks")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Participant", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("Action");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("api.Models.Evaluation", b =>
                {
                    b.HasOne("api.Models.Project", "Project")
                        .WithMany("Evaluations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("api.Models.Note", b =>
                {
                    b.HasOne("api.Models.Action", "Action")
                        .WithMany("Notes")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Participant", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("Action");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("api.Models.Participant", b =>
                {
                    b.HasOne("api.Models.Evaluation", "Evaluation")
                        .WithMany("Participants")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evaluation");
                });

            modelBuilder.Entity("api.Models.Question", b =>
                {
                    b.HasOne("api.Models.Evaluation", "Evaluation")
                        .WithMany("Questions")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.QuestionTemplate", "QuestionTemplate")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evaluation");

                    b.Navigation("QuestionTemplate");
                });

            modelBuilder.Entity("api.Models.QuestionTemplate", b =>
                {
                    b.HasOne("api.Models.QuestionTemplate", "previous")
                        .WithMany()
                        .HasForeignKey("previousId");

                    b.Navigation("previous");
                });

            modelBuilder.Entity("api.Models.Action", b =>
                {
                    b.Navigation("ClosingRemarks");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("api.Models.Evaluation", b =>
                {
                    b.Navigation("Participants");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("api.Models.Project", b =>
                {
                    b.Navigation("Evaluations");
                });

            modelBuilder.Entity("api.Models.Question", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Answers");
                });

            modelBuilder.Entity("api.Models.QuestionTemplate", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}