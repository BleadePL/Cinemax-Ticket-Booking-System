using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Booking
    {
        [Key, ForeignKey("ShowSeat")]
        public int IDShowSeat { get; set; }
        
        public ShowSeat ShowSeat { get; set; }

        public string IDCustomer { get; set; }
        [ForeignKey("IDCustomer")]
        public Customer Customer { get; set; }

        public bool IsPurchased { get; set; }

    }
}
