using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipDailyReportFile_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFile");

            migrationBuilder.DropIndex(
                name: "IX_InternshipDailyReportFile_StudentUserId",
                table: "InternshipDailyReportFile");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "InternshipDailyReportFile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentUserId",
                table: "InternshipDailyReportFile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InternshipDailyReportFile_StudentUserId",
                table: "InternshipDailyReportFile",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipDailyReportFile_AspNetUsers_StudentUserId",
                table: "InternshipDailyReportFile",
                column: "StudentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
