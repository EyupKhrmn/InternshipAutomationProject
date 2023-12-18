using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "CompanyOfficerNameSurname",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "DayCount",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "EducationDate",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "SchoolTerm",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "TeachingStaffNameSurname",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "WritingDate",
                table: "InternshipDailyReportFiles");

            migrationBuilder.AlterColumn<string>(
                name: "StudentNameSurname",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyManagerNameSurname",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "InternshipDailyReportFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DescriptionOfWork",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentUserId",
                table: "InternshipDailyReportFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TopicTitleOfWork",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipDailyReportFiles_StudentUserId",
                table: "InternshipDailyReportFiles",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipDailyReportFiles_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFiles",
                column: "StudentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipDailyReportFiles_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropIndex(
                name: "IX_InternshipDailyReportFiles_StudentUserId",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "CompanyManagerNameSurname",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "DescriptionOfWork",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "InternshipDailyReportFiles");

            migrationBuilder.DropColumn(
                name: "TopicTitleOfWork",
                table: "InternshipDailyReportFiles");

            migrationBuilder.AlterColumn<string>(
                name: "StudentNameSurname",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyOfficerNameSurname",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayCount",
                table: "InternshipDailyReportFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EducationDate",
                table: "InternshipDailyReportFiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolTerm",
                table: "InternshipDailyReportFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeachingStaffNameSurname",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "InternshipDailyReportFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WritingDate",
                table: "InternshipDailyReportFiles",
                type: "datetime2",
                nullable: true);
        }
    }
}
