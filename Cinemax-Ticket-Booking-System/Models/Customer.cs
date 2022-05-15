using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Customer
    {
        [Key]
        public int IdC { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        List<Booking> Bookings { get; set; }
    }
}
