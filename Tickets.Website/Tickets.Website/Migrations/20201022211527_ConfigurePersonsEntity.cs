using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.Website.Migrations
{
    public partial class ConfigurePersonsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPersons_AspNetUsers_UserId",
                table: "TicketPersons");

            migrationBuilder.DropIndex(
                name: "IX_TicketPersons_UserId",
                table: "TicketPersons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketPersons");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPersons_PersonId",
                table: "TicketPersons",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPersons_Persons_PersonId",
                table: "TicketPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPersons_Persons_PersonId",
                table: "TicketPersons");

            migrationBuilder.DropIndex(
                name: "IX_TicketPersons_PersonId",
                table: "TicketPersons");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TicketPersons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketPersons_UserId",
                table: "TicketPersons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPersons_AspNetUsers_UserId",
                table: "TicketPersons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
