using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class ShowSeat
    {
        [Key]
        public int IDSS { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public int IDShowing { get; set; }

        [ForeignKey("IDShowing")]
        public Showing Showing { get; set; }
    }
}
