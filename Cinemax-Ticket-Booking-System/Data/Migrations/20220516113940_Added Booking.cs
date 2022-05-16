using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemax_Ticket_Booking_System.Data.Migrations
{
    public partial class AddedBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    IDShowSeat = table.Column<int>(type: "int", nullable: false),
                    IDCustomer = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsPurchased = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.IDShowSeat);
                    table.ForeignKey(
                        name: "FK_Booking_Customer_IDCustomer",
                        column: x => x.IDCustomer,
                        principalTable: "Customer",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_ShowSeat_IDShowSeat",
                        column: x => x.IDShowSeat,
                        principalTable: "ShowSeat",
                        principalColumn: "IDSS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IDCustomer",
                table: "Booking",
                column: "IDCustomer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
