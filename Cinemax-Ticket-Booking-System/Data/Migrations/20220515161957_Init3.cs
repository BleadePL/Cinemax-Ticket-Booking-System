using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemax_Ticket_Booking_System.Data.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Showing",
                columns: table => new
                {
                    IDS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailibleSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IDMovie = table.Column<int>(type: "int", nullable: false),
                    IDScreenRoom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showing", x => x.IDS);
                    table.ForeignKey(
                        name: "FK_Showing_Movie_IDMovie",
                        column: x => x.IDMovie,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showing_ScreeningRoom_IDScreenRoom",
                        column: x => x.IDScreenRoom,
                        principalTable: "ScreeningRoom",
                        principalColumn: "IDSR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Showing_IDMovie",
                table: "Showing",
                column: "IDMovie");

            migrationBuilder.CreateIndex(
                name: "IX_Showing_IDScreenRoom",
                table: "Showing",
                column: "IDScreenRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Showing");
        }
    }
}
