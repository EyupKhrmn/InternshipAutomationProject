using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipAutomation.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCheckCompany",
                table: "InternshipDailyReportFile",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckCompany",
                table: "InternshipDailyReportFile");
        }
    }
}
