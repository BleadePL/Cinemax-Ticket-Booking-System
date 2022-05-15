using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class RoomSeat
    {
        [Key]
        public int IdRS { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public int IDScreeningRoom { get; set; }

        [ForeignKey("IDScreeningRoom")]
        public ScreeningRoom ScreeningRoom { get; set; }
    }
}
