using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tickets.Website.Migrations
{
    public partial class PersonsTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketUsers_Tickets_TicketId",
                table: "TicketUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketUsers_AspNetUsers_UserId",
                table: "TicketUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketUsers",
                table: "TicketUsers");

            migrationBuilder.RenameTable(
                name: "TicketUsers",
                newName: "TicketPersons");

            migrationBuilder.RenameIndex(
                name: "IX_TicketUsers_UserId",
                table: "TicketPersons",
                newName: "IX_TicketPersons_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketUsers_TicketId",
                table: "TicketPersons",
                newName: "IX_TicketPersons_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketPersons",
                table: "TicketPersons",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    PassportNum = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPersons_Tickets_TicketId",
                table: "TicketPersons",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPersons_AspNetUsers_UserId",
                table: "TicketPersons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPersons_Tickets_TicketId",
                table: "TicketPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPersons_AspNetUsers_UserId",
                table: "TicketPersons");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketPersons",
                table: "TicketPersons");

            migrationBuilder.RenameTable(
                name: "TicketPersons",
                newName: "TicketUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TicketPersons_UserId",
                table: "TicketUsers",
                newName: "IX_TicketUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketPersons_TicketId",
                table: "TicketUsers",
                newName: "IX_TicketUsers_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketUsers",
                table: "TicketUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUsers_Tickets_TicketId",
                table: "TicketUsers",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUsers_AspNetUsers_UserId",
                table: "TicketUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
