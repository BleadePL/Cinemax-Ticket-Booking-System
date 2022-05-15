using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemax_Ticket_Booking_System.Data.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScreeningRoom",
                columns: table => new
                {
                    IDSR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningRoom", x => x.IDSR);
                });

            migrationBuilder.CreateTable(
                name: "RoomSeat",
                columns: table => new
                {
                    IdRS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    IDScreeningRoom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSeat", x => x.IdRS);
                    table.ForeignKey(
                        name: "FK_RoomSeat_ScreeningRoom_IDScreeningRoom",
                        column: x => x.IDScreeningRoom,
                        principalTable: "ScreeningRoom",
                        principalColumn: "IDSR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomSeat_IDScreeningRoom",
                table: "RoomSeat",
                column: "IDScreeningRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomSeat");

            migrationBuilder.DropTable(
                name: "ScreeningRoom");
        }
    }
}
