using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v62 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipDailyReportFiles_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_InternshipApplicationFiles_InternshipApplicationFileId",
                table: "Internships");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_InternshipDailyReportFiles_InternshipDailyReportFileId",
                table: "Internships");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_StateContributionFiles_StateContributionFileId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_InternshipDailyReportFileId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_StateContributionFileId",
                table: "Internships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateContributionFiles",
                table: "StateContributionFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipDailyReportFiles",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipApplicationFiles",
                table: "InternshipApplicationFiles");

            migrationBuilder.DropColumn(
                name: "InternshipDailyReportFileId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "StateContributionFileId",
                table: "Internships");

            migrationBuilder.RenameTable(
                name: "StateContributionFiles",
                newName: "StateContributionFile");

            migrationBuilder.RenameTable(
                name: "InternshipDailyReportFiles",
                newName: "InternshipDailyReportFile");

            migrationBuilder.RenameTable(
                name: "InternshipApplicationFiles",
                newName: "InternshipApplicationFile");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipDailyReportFiles_StudentUserId",
                table: "InternshipDailyReportFile",
                newName: "IX_InternshipDailyReportFile_StudentUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Internships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationDate",
                table: "Internships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "InternshipId",
                table: "StateContributionFile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InternshipId",
                table: "InternshipDailyReportFile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateContributionFile",
                table: "StateContributionFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipDailyReportFile",
                table: "InternshipDailyReportFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipApplicationFile",
                table: "InternshipApplicationFile",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InternshipEvaluationFormForCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SelfConfidence = table.Column<int>(type: "int", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false),
                    InterestForWork = table.Column<int>(type: "int", nullable: false),
                    Creativity = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Appearance = table.Column<int>(type: "int", nullable: false),
                    CommunicationWithSupervisors = table.Column<int>(type: "int", nullable: false),
                    CommunicationWithWorkFriends = table.Column<int>(type: "int", nullable: false),
                    CommunicationWithCustomers = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<int>(type: "int", nullable: false),
                    EfficiencyInTimeUsing = table.Column<int>(type: "int", nullable: false),
                    ProblemSolving = table.Column<int>(type: "int", nullable: false),
                    FamiliarityOfTeamWorks = table.Column<int>(type: "int", nullable: false),
                    TechnicalKnowledge = table.Column<int>(type: "int", nullable: false),
                    SuitabilityForJobStandards = table.Column<int>(type: "int", nullable: false),
                    TakingOnResponsibility = table.Column<int>(type: "int", nullable: false),
                    FulfillingTheDuties = table.Column<int>(type: "int", nullable: false),
                    EffectiveUserOfResources = table.Column<int>(type: "int", nullable: false),
                    FamiliarityOfTechnology = table.Column<int>(type: "int", nullable: false),
                    BeingInnovative = table.Column<int>(type: "int", nullable: false),
                    SupervisorNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkAgain = table.Column<bool>(type: "bit", nullable: false),
                    DevelopmentSuggestionForStudentUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipEvaluationFormForCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipEvaluationFormForCompanies_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternshipResultReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolTerm = table.Column<int>(type: "int", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipResultReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipResultReports_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateContributionFile_InternshipId",
                table: "StateContributionFile",
                column: "InternshipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipDailyReportFile_InternshipId",
                table: "InternshipDailyReportFile",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipEvaluationFormForCompanies_InternshipId",
                table: "InternshipEvaluationFormForCompanies",
                column: "InternshipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternshipResultReports_InternshipId",
                table: "InternshipResultReports",
                column: "InternshipId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipDailyReportFile_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFile",
                column: "StudentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipDailyReportFile_Internships_InternshipId",
                table: "InternshipDailyReportFile",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_InternshipApplicationFile_InternshipApplicationFileId",
                table: "Internships",
                column: "InternshipApplicationFileId",
                principalTable: "InternshipApplicationFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StateContributionFile_Internships_InternshipId",
                table: "StateContributionFile",
                column: "InternshipId",
                principalTable: "Internships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipDailyReportFile_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFile");

            migrationBuilder.DropForeignKey(
                name: "FK_InternshipDailyReportFile_Internships_InternshipId",
                table: "InternshipDailyReportFile");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_InternshipApplicationFile_InternshipApplicationFileId",
                table: "Internships");

            migrationBuilder.DropForeignKey(
                name: "FK_StateContributionFile_Internships_InternshipId",
                table: "StateContributionFile");

            migrationBuilder.DropTable(
                name: "InternshipEvaluationFormForCompanies");

            migrationBuilder.DropTable(
                name: "InternshipResultReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateContributionFile",
                table: "StateContributionFile");

            migrationBuilder.DropIndex(
                name: "IX_StateContributionFile_InternshipId",
                table: "StateContributionFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipDailyReportFile",
                table: "InternshipDailyReportFile");

            migrationBuilder.DropIndex(
                name: "IX_InternshipDailyReportFile_InternshipId",
                table: "InternshipDailyReportFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InternshipApplicationFile",
                table: "InternshipApplicationFile");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "LastModificationDate",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "StateContributionFile");

            migrationBuilder.DropColumn(
                name: "InternshipId",
                table: "InternshipDailyReportFile");

            migrationBuilder.RenameTable(
                name: "StateContributionFile",
                newName: "StateContributionFiles");

            migrationBuilder.RenameTable(
                name: "InternshipDailyReportFile",
                newName: "InternshipDailyReportFiles");

            migrationBuilder.RenameTable(
                name: "InternshipApplicationFile",
                newName: "InternshipApplicationFiles");

            migrationBuilder.RenameIndex(
                name: "IX_InternshipDailyReportFile_StudentUserId",
                table: "InternshipDailyReportFiles",
                newName: "IX_InternshipDailyReportFiles_StudentUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "InternshipDailyReportFileId",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StateContributionFileId",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateContributionFiles",
                table: "StateContributionFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipDailyReportFiles",
                table: "InternshipDailyReportFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InternshipApplicationFiles",
                table: "InternshipApplicationFiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_InternshipDailyReportFileId",
                table: "Internships",
                column: "InternshipDailyReportFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_StateContributionFileId",
                table: "Internships",
                column: "StateContributionFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipDailyReportFiles_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFiles",
                column: "StudentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_InternshipApplicationFiles_InternshipApplicationFileId",
                table: "Internships",
                column: "InternshipApplicationFileId",
                principalTable: "InternshipApplicationFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_InternshipDailyReportFiles_InternshipDailyReportFileId",
                table: "Internships",
                column: "InternshipDailyReportFileId",
                principalTable: "InternshipDailyReportFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_StateContributionFiles_StateContributionFileId",
                table: "Internships",
                column: "StateContributionFileId",
                principalTable: "StateContributionFiles",
                principalColumn: "Id");
        }
    }
}
