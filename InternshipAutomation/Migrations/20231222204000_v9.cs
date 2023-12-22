using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_AspNetUsers_StudentUserId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_StudentUserId",
                table: "Internships");

            migrationBuilder.RenameColumn(
                name: "StudentUserId",
                table: "Internships",
                newName: "StudentUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentUser",
                table: "Internships",
                newName: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_StudentUserId",
                table: "Internships",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_AspNetUsers_StudentUserId",
                table: "Internships",
                column: "StudentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
