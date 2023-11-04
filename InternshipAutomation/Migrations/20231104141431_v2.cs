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
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "SendingFiles");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "SendingFiles");

            migrationBuilder.AddColumn<Guid>(
                name: "StudenUserId",
                table: "SendingFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudenUserId",
                table: "SendingFiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "SendingFiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "SendingFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
