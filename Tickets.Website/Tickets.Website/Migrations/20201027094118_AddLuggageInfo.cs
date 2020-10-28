using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.Website.Migrations
{
    public partial class AddLuggageInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHandLuggageIncluded",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLuggageIncluded",
                table: "Tickets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHandLuggageIncluded",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsLuggageIncluded",
                table: "Tickets");
        }
    }
}
