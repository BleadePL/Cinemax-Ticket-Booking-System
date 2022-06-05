using Cinemax_Ticket_Booking_System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Cinemax_Ticket_Booking_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Cinemax_Ticket_Booking_System.Controllers
{
    public class MainShowingsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private Dictionary<string, List<(DateTime date, float Duration, string startHour, int IdShowing)>> _shows =
            new Dictionary<string, List<(DateTime date, float Duration, string startHour, int IdShowing)>>();


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
                date = DateTime.Now;
                ViewData["date"] = $"{date.Value.DayOfWeek} {date.Value.ToString("dd/MM/yyyy")}";
                
            }
            else
            {
                date = DateTime.Parse(newDate);
                ViewData["date"] = $"{date.Value.DayOfWeek} {date.Value.ToString("dd/MM/yyyy")}";
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
                        new List<(DateTime date, float Duration, string startHour, int IdShowing)>()
                            {(
                            movie.Date,
                            movie.Duration, 
                            movie.StartHour,
                            movie.IdShowing
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
                                             movie.StartHour,
                                             movie.IdShowing
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

            int[,] cinemaGrid = new int[6,5];

            for (int i = 0; i < cinemaGrid.GetLength(0); i++)
            {
                for (int j = 0; j < cinemaGrid.GetLength(1); j++)
                {
                    cinemaGrid[i,j] = -1;
                }
            }

            foreach (var seat in seats)
            {
                if (seat.IsPurchased)
                {
                    cinemaGrid[seat.Row - 1, seat.Column - 1] = 2;
                }
                else cinemaGrid[seat.Row - 1, seat.Column - 1] = 1;
            }

            //string takenSeats = JsonConvert.SerializeObject(seats);

            var pattern =
                from showing in _context.Showing
                join screeningRoom in _context.ScreeningRoom on showing.IDScreenRoom equals screeningRoom.IDSR
                where showing.IDS == showId
                select new
                {
                    pattern = screeningRoom.ScreenPattern
                };

            var screenPattern = pattern.First().pattern;

            ViewData["pattern"] = screenPattern;
            ViewData["takenSeats"] = cinemaGrid;
            ViewData["showId"] = showId;

            return View();
        }

        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> BookingTickets()
        {
            var cookies = Request.Cookies;
            var request = cookies.Where(ck => ck.Key.Equals("Reservation")).First().Value.ToString();
            var parseJson = JArray.Parse(request);


            foreach (JObject item in parseJson)
            {
                int id = int.Parse(item.GetValue("showId").ToString());
                int col = int.Parse(item.GetValue("col").ToString());
                int row = int.Parse(item.GetValue("row").ToString());
                bool IsPurchased = bool.Parse(item.GetValue("isPurchased").ToString());

                string customerId = _userManager.GetUserId(HttpContext.User);

                if (customerId == null)
                {
                    customerId = "71795eb4-61cc-4db6-89ed-572e5f9751f9";
                }

                ShowSeat showSeat = new ShowSeat()
                {
                    Row = row,
                    Column = col,
                    IDShowing = id
                };


                _context.Add(showSeat);
                await _context.SaveChangesAsync();

                Booking booking = new Booking()
                {
                    IDShowSeat = showSeat.IDSS,
                    IDCustomer = customerId,
                    IsPurchased = IsPurchased
                };

                _context.Booking.Add(booking);
                await _context.SaveChangesAsync();


            }
            return View();
        }
    }
}
