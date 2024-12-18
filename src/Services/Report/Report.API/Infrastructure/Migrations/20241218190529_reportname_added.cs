using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reportname_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportName",
                table: "ReportItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportName",
                table: "ReportItems");
        }
    }
}
