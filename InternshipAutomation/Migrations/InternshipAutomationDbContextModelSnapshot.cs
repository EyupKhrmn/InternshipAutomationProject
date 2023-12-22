﻿// <auto-generated />
using System;
using InternshipAutomation.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternshipAutomation.Migrations
{
    [DbContext(typeof(InternshipAutomationDbContext))]
    partial class InternshipAutomationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipApplicationFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyEMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanySector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinishedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartedDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("StudentAGNO")
                        .HasColumnType("real");

                    b.Property<string>("StudentNameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentProgram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentTCKN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InternshipApplicationFile");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipDailyReportFile", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyManagerNameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CurrentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionOfWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentNameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopicTitleOfWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InternshipId");

                    b.ToTable("InternshipDailyReportFile");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipEvaluationFormForCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Appearance")
                        .HasColumnType("int");

                    b.Property<int>("Attendance")
                        .HasColumnType("int");

                    b.Property<int>("BeingInnovative")
                        .HasColumnType("int");

                    b.Property<int>("CommunicationWithCustomers")
                        .HasColumnType("int");

                    b.Property<int>("CommunicationWithSupervisors")
                        .HasColumnType("int");

                    b.Property<int>("CommunicationWithWorkFriends")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Creativity")
                        .HasColumnType("int");

                    b.Property<string>("DevelopmentSuggestionForStudentUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EffectiveUserOfResources")
                        .HasColumnType("int");

                    b.Property<int>("EfficiencyInTimeUsing")
                        .HasColumnType("int");

                    b.Property<int>("FamiliarityOfTeamWorks")
                        .HasColumnType("int");

                    b.Property<int>("FamiliarityOfTechnology")
                        .HasColumnType("int");

                    b.Property<int>("FulfillingTheDuties")
                        .HasColumnType("int");

                    b.Property<int>("Initiative")
                        .HasColumnType("int");

                    b.Property<int>("InterestForWork")
                        .HasColumnType("int");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Leadership")
                        .HasColumnType("int");

                    b.Property<int>("ProblemSolving")
                        .HasColumnType("int");

                    b.Property<int>("SelfConfidence")
                        .HasColumnType("int");

                    b.Property<string>("StudentUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuitabilityForJobStandards")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorNameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TakingOnResponsibility")
                        .HasColumnType("int");

                    b.Property<int>("TechnicalKnowledge")
                        .HasColumnType("int");

                    b.Property<bool>("WorkAgain")
                        .HasColumnType("bit");

                    b.Property<string>("WorkingArea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WorkingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InternshipId")
                        .IsUnique();

                    b.ToTable("InternshipEvaluationFormForCompanies");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipResultReport", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademicYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyManager")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SchoolTerm")
                        .HasColumnType("int");

                    b.Property<string>("StudentNameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentProgram")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherNameSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InternshipId")
                        .IsUnique();

                    b.ToTable("InternshipResultReports");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.StateContributionFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankAccountHolder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyTCKN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyVKN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompnayIBAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeCount")
                        .HasColumnType("int");

                    b.Property<Guid>("InternshipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StudentBirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentNameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentProgram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentTCKN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InternshipId")
                        .IsUnique();

                    b.ToTable("StateContributionFile");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyWorkingArea")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.Internship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("InternshipApplicationFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InternshipPeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Note")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("StudentUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeacherUser")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("InternshipApplicationFileId");

                    b.HasIndex("InternshipPeriodId");

                    b.ToTable("Internships");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.InternshipPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StartedDate")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("InternshipPeriods");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AdminUserNameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyUserNameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherNameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("IntershipOtomation.Domain.Entities.User.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipDailyReportFile", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.Internship", "Internship")
                        .WithMany("InternshipDailyReportFiles")
                        .HasForeignKey("InternshipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Internship");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipEvaluationFormForCompany", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.Internship", "Internship")
                        .WithOne("InternshipEvaluationFormForCompany")
                        .HasForeignKey("InternshipAutomation.Domain.Entities.Files.InternshipEvaluationFormForCompany", "InternshipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Internship");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.InternshipResultReport", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.Internship", "Internship")
                        .WithOne("InternshipResultReport")
                        .HasForeignKey("InternshipAutomation.Domain.Entities.Files.InternshipResultReport", "InternshipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Internship");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Files.StateContributionFile", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.Internship", "Internship")
                        .WithOne("StateContributionFile")
                        .HasForeignKey("InternshipAutomation.Domain.Entities.Files.StateContributionFile", "InternshipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Internship");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.Internship", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.Company", null)
                        .WithMany("Internships")
                        .HasForeignKey("CompanyId");

                    b.HasOne("InternshipAutomation.Domain.Entities.Files.InternshipApplicationFile", "InternshipApplicationFile")
                        .WithMany()
                        .HasForeignKey("InternshipApplicationFileId");

                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.InternshipPeriod", "InternshipPeriod")
                        .WithMany("Internships")
                        .HasForeignKey("InternshipPeriodId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("InternshipApplicationFile");

                    b.Navigation("InternshipPeriod");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.InternshipPeriod", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.User.User", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.Entities.Internship.Company", null)
                        .WithMany("CompanyUsers")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("IntershipOtomation.Domain.Entities.User.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("IntershipOtomation.Domain.Entities.User.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternshipAutomation.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("InternshipAutomation.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.Company", b =>
                {
                    b.Navigation("CompanyUsers");

                    b.Navigation("Internships");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.Internship", b =>
                {
                    b.Navigation("InternshipDailyReportFiles");

                    b.Navigation("InternshipEvaluationFormForCompany");

                    b.Navigation("InternshipResultReport");

                    b.Navigation("StateContributionFile");
                });

            modelBuilder.Entity("InternshipAutomation.Domain.Entities.Internship.InternshipPeriod", b =>
                {
                    b.Navigation("Internships");
                });
#pragma warning restore 612, 618
        }
    }
}
