using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternshipUser");

            migrationBuilder.AddColumn<string>(
                name: "CompanyUserName",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherUserName",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "InternshipPeriods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Internships_UserId",
                table: "Internships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipPeriods_UserId",
                table: "InternshipPeriods",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternshipPeriods_AspNetUsers_UserId",
                table: "InternshipPeriods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_AspNetUsers_UserId",
                table: "Internships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternshipPeriods_AspNetUsers_UserId",
                table: "InternshipPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_AspNetUsers_UserId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_UserId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_InternshipPeriods_UserId",
                table: "InternshipPeriods");

            migrationBuilder.DropColumn(
                name: "CompanyUserName",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "TeacherUserName",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InternshipPeriods");

            migrationBuilder.CreateTable(
                name: "InternshipUser",
                columns: table => new
                {
                    InternshipsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipUser", x => new { x.InternshipsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_InternshipUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternshipUser_Internships_InternshipsId",
                        column: x => x.InternshipsId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternshipUser_UsersId",
                table: "InternshipUser",
                column: "UsersId");
        }
    }
}
