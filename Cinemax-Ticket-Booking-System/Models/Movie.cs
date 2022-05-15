using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Movie
    {
        [Key]
        public int IdM { get; set; }
        
        [Display(Name = "Movie Title")]
        [Required(ErrorMessage = "Movie needs a title")]
        public string Title { get; set; }
        [Display(Name = "Short movie description")]
        public string Description { get; set; }
        public float Duration { get; set; }

        
        public int IDCategory { get; set; }
        [ForeignKey("IDCategory")]
        public Category Category { get; set; }

        List<Showing> Showings { get; set; }
    }
}
