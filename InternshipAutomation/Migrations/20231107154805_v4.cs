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
                name: "FK_Internships_AspNetUsers_AdminUserId",
                table: "Internships");

            migrationBuilder.DropTable(
                name: "BackUpFiles");

            migrationBuilder.DropTable(
                name: "InternshipApplicationFiles");

            migrationBuilder.DropTable(
                name: "InternshipBookPages");

            migrationBuilder.DropTable(
                name: "SendingFiles");

            migrationBuilder.DropIndex(
                name: "IX_Internships_AdminUserId",
                table: "Internships");

            migrationBuilder.DropColumn(
                name: "AdminUser_InternshipId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Class",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherNameSurname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherNameSurname",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "StudentNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Class",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminUser_InternshipId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BackUpFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackUpFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackUpFiles_AspNetUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternshipApplicationFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipApplicationFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipApplicationFiles_AspNetUsers_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipApplicationFiles_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternshipBookPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WritingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipBookPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipBookPages_Internships_InternshipId",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendingFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudenUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendingFiles_AspNetUsers_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Internships_AdminUserId",
                table: "Internships",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BackUpFiles_AdminUserId",
                table: "BackUpFiles",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipApplicationFiles_InternshipId",
                table: "InternshipApplicationFiles",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipApplicationFiles_StudentUserId",
                table: "InternshipApplicationFiles",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipBookPages_InternshipId",
                table: "InternshipBookPages",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_SendingFiles_StudentUserId",
                table: "SendingFiles",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_AspNetUsers_AdminUserId",
                table: "Internships",
                column: "AdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
