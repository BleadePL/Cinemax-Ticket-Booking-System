using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemax_Ticket_Booking_System.Data.Migrations
{
    public partial class ScreenRoom_Pattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenPattern",
                table: "ScreeningRoom",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenPattern",
                table: "ScreeningRoom");
        }
    }
}
