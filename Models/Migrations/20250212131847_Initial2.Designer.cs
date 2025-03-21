﻿// <auto-generated />
using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(StandardContest2023Context))]
    [Migration("20250212131847_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entities.Models.Contest", b =>
                {
                    b.Property<string>("ContestID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<string>("AppSecret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContestUniqueCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionContest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntryExclusionFields")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessageAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvalidSmsresponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvalidWhatsappResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissingFieldResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameContest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepeatValidation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepeatedOnlinePageResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepeatedSmsresponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepeatedWhatsappResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SMSSubmitFields")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TerminationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TestDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TierAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ValidOnlineCompletionResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValidOnlinePageResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValidSmsresponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValidWhatsappResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValidationRegexFull")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinnerExclusionFields")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContestID");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("Entities.Models.ContestFieldDetails", b =>
                {
                    b.Property<int>("FieldDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FieldDetailID"), 1L, 1);

                    b.Property<string>("ContestID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FieldLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormControl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsUnique")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("RegexValidationID")
                        .HasColumnType("int");

                    b.Property<bool?>("ShowOnlineCompletion")
                        .HasColumnType("bit");

                    b.Property<bool?>("ShowOnlinePage")
                        .HasColumnType("bit");

                    b.HasKey("FieldDetailID");

                    b.HasIndex("ContestID");

                    b.HasIndex("RegexValidationID");

                    b.ToTable("ContestFieldDetails");
                });

            modelBuilder.Entity("Entities.Models.RegexValidation", b =>
                {
                    b.Property<int>("RegexID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegexID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pattern")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegexID");

                    b.ToTable("RegexValidations");

                    b.HasData(
                        new
                        {
                            RegexID = -1,
                            Description = "Regex for Name",
                            Name = "Name",
                            Pattern = "^[a-zA-Z ]+$"
                        },
                        new
                        {
                            RegexID = -2,
                            Description = "Regex for Mobile Number",
                            Name = "Mobile Number",
                            Pattern = "^\\+*\\d+$"
                        },
                        new
                        {
                            RegexID = -3,
                            Description = "Regex for Receipt Number",
                            Name = "Receipt Number",
                            Pattern = "^\\S*\\d\\S*$"
                        });
                });

            modelBuilder.Entity("Entities.Models.ContestFieldDetails", b =>
                {
                    b.HasOne("Entities.Models.Contest", "Contest")
                        .WithMany("ContestFieldDetails")
                        .HasForeignKey("ContestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.RegexValidation", "RegexValidation")
                        .WithMany("ContestFieldDetails")
                        .HasForeignKey("RegexValidationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contest");

                    b.Navigation("RegexValidation");
                });

            modelBuilder.Entity("Entities.Models.Contest", b =>
                {
                    b.Navigation("ContestFieldDetails");
                });

            modelBuilder.Entity("Entities.Models.RegexValidation", b =>
                {
                    b.Navigation("ContestFieldDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
