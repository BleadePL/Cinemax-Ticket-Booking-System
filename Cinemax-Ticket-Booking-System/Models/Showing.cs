using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinemax_Ticket_Booking_System.Models
{
    public class Showing
    {
        [Key]
        public int IDS { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShowStart { get; set; }
        public int AvailibleSeats { get; set; }
        public double Price { get; set; }

        public int IDMovie { get; set; }
        [ForeignKey("IDMovie")]
        public Movie Movie { get; set; }

        public int IDScreenRoom { get; set; }
        [ForeignKey("IDScreenRoom")]
        public ScreeningRoom ScreeningRoom { get; set; }

        List<ShowSeat> ShowSeats { get; set; }
    }
}
