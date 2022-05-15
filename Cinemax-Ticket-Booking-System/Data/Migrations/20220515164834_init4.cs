using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemax_Ticket_Booking_System.Data.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShowSeat",
                columns: table => new
                {
                    IDSS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    IDShowing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowSeat", x => x.IDSS);
                    table.ForeignKey(
                        name: "FK_ShowSeat_Showing_IDShowing",
                        column: x => x.IDShowing,
                        principalTable: "Showing",
                        principalColumn: "IDS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowSeat_IDShowing",
                table: "ShowSeat",
                column: "IDShowing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowSeat");
        }
    }
}
