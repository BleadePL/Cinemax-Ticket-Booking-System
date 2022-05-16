using Microsoft.AspNetCore.Identity;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Customer Customer { get; set; }
    }
}
