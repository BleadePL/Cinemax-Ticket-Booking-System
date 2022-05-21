using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Movie Title")]
        [Required(ErrorMessage = "Movie needs a title")]
        public string Title { get; set; }
        [Display(Name = "Short movie description")]
        public string Description { get; set; }
        public float Duration { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FilePath { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        List<Showing> Showings { get; set; }
    }
}
