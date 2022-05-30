using Cinemax_Ticket_Booking_System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Cinemax_Ticket_Booking_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinemax_Ticket_Booking_System.Controllers
{
    public class MainShowingsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private Dictionary<string, List<(DateTime date, float Duration, string startHour)>> _shows =
            new Dictionary<string, List<(DateTime date, float Duration, string startHour)>>();


        private Dictionary<string, (string Category, string ScreenRoom, int IdShowing, string Src)> _movieDetails =
            new Dictionary<string, (string Category, string ScreenRoom, int IdShowing, string Src)>();


        public MainShowingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("[Controller]/{newDate?}")]
        public IActionResult Index(string? newDate)
        {

            DateTime? date = null;

            if (newDate == null)
            {
                ViewData["date"] = DateTime.UtcNow;
                date = DateTime.UtcNow;
                
            }
            else
            {
                date = DateTime.Parse(newDate);
                ViewData["date"] = date;
            }

            var shows =
                from show in _context.Showing
                join movie in _context.Movie on show.IDMovie equals movie.Id
                join category in _context.Category on movie.CategoryId equals category.Id
                join screen in _context.ScreeningRoom on show.IDScreenRoom equals screen.IDSR
                where show.ShowStart.Date == date.Value.Date
                select new {
                            Title = movie.Title,
                            Date = show.ShowStart,
                            Duration = movie.Duration, 
                            StartHour = show.ShowStart.ToString("H:mm"), 
                            CategoryName = category.Name, 
                            ScreenRoom = screen.Name,
                            SRC = movie.FilePath,
                            IdShowing = show.IDS,
                            PatternId = screen.ScreenPattern
                            };


            foreach (var movie in shows)
            {
                if (!_shows.ContainsKey(movie.Title))
                {
                    _shows.Add(movie.Title, 
                        new List<(DateTime date, float Duration, string startHour)>()
                            {(
                            movie.Date,
                            movie.Duration, 
                            movie.StartHour
                            )});

                    if (!_movieDetails.ContainsKey(movie.Title))
                    {
                        _movieDetails.Add(movie.Title, (movie.CategoryName, movie.ScreenRoom, movie.IdShowing, movie.SRC));
                    }

                }
                else
                {
                    _shows[movie.Title].Add((
                                             movie.Date, 
                                             movie.Duration, 
                                             movie.StartHour
                                             ));

                }
            }

            ViewData["shows"] = _shows;

            ViewData["Titles"] = _shows.Keys.ToList();

            ViewData["MovieDescr"] = _movieDetails;

            return View();
        }

        [Route("[Controller]/[Action]/{showInfo}")]
        public IActionResult ScreenRoom(string? showInfo)
        {
            int showId = int.Parse(showInfo);

            var seats =
                from showing in _context.Showing
                join showSeat in _context.ShowSeat on showing.IDS equals showSeat.IDShowing
                join booking in _context.Booking on showSeat.IDSS equals booking.IDShowSeat
                join screenRoom in _context.ScreeningRoom on showing.IDScreenRoom equals screenRoom.IDSR
                where showing.IDS == showId
                select new
                {
                    Row = showSeat.Row,
                    Column = showSeat.Column,
                    IsPurchased = booking.IsPurchased,
                    PatternId = screenRoom.ScreenPattern,
                    ShowId = showing.IDS
                };


            string takenSeats = JsonConvert.SerializeObject(seats);
            ViewData["pattern"] = seats.FirstOrDefault().PatternId;
            ViewData["takenSeats"] = takenSeats;

            return View();
        }

        [Route("[Controller]/[Action]/{tickets}")]
        public IActionResult BookingTickets(string? tickets)
        {
            dynamic parseJson = JsonConvert.DeserializeObject<dynamic>(tickets);

            foreach (var ticket in parseJson)
            {

                string customerId = _userManager.GetUserId(HttpContext.User);

                if (customerId == null)
                {
                    customerId = "NULL";
                }
                
                ShowSeat showSeat = new ShowSeat()
                {
                    Row = ticket.Row.Value,
                    Column = ticket.Column.Value,
                    IDShowing = ticket.ShowId.Value
                };

                Booking booking = new Booking()
                {
                    IDShowSeat = showSeat.IDSS,
                    IDCustomer = customerId,
                    IsPurchased = ticket.IsPurchased.Value
                };

                _context.ShowSeat.Add(showSeat);
                _context.Booking.Add(booking);

                //Added after paying?
                _context.SaveChanges();
            }

            return View();
        }
    }
}
