using System.ComponentModel.DataAnnotations;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Category
    {
        [Key]
        public int IdC { get; set; }
        public string Name { get; set; }
    }
}
