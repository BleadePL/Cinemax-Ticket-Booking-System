using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Booking
    {
        [Key]
        public int IDShowing { get; set; }
        [ForeignKey("IDShowing")]
        public ShowSeat ShowSeat { get; set; }

        public int IDCustomer { get; set; }
        [ForeignKey("IDCustomer")]
        public Customer Customer { get; set; }

        public bool IsPurchased { get; set; }

    }
}
