using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Cinemax_Ticket_Booking_System.Models;

namespace Cinemax_Ticket_Booking_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cinemax_Ticket_Booking_System.Models.Category> Category { get; set; }
        public DbSet<Cinemax_Ticket_Booking_System.Models.Movie> Movie { get; set; }
        public DbSet<Cinemax_Ticket_Booking_System.Models.ScreeningRoom> ScreeningRoom { get; set; }
        public DbSet<Cinemax_Ticket_Booking_System.Models.RoomSeat> RoomSeat { get; set; }
        public DbSet<Cinemax_Ticket_Booking_System.Models.Showing> Showing { get; set; }
        public DbSet<Cinemax_Ticket_Booking_System.Models.ShowSeat> ShowSeat { get; set; }

        public DbSet<Cinemax_Ticket_Booking_System.Models.Customer> Customer { get; set; }
        public DbSet<Cinemax_Ticket_Booking_System.Models.Booking> Booking { get; set; }
    }
}
