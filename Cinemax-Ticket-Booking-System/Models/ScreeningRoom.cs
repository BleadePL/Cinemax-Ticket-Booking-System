using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class ScreeningRoom
    {
        [Key]
        public int IDSR { get; set; }
        public string Name { get; set; }

        List<RoomSeat> Seats { get; set; }
        List<Showing> Showings { get; set; }

        public int ScreenPattern { get; set; }
    }
}
