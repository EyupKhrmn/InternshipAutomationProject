using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_AspNetUsers_UserId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "CompanyUserName",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "TeacherUserName",
                table: "Internships");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyUser",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentUser",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherUser",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_AspNetUsers_UserId",
                table: "Internships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_AspNetUsers_UserId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "CompanyUser",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "StudentUser",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "TeacherUser",
                table: "Internships");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Internships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_AspNetUsers_UserId",
                table: "Internships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
