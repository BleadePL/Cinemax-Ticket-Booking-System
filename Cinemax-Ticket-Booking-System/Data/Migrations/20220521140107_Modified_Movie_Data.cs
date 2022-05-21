using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemax_Ticket_Booking_System.Data.Migrations
{
    public partial class Modified_Movie_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Movie");
        }
    }
}
