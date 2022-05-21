using Cinemax_Ticket_Booking_System.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinemax_Ticket_Booking_System.Controllers
{
    public class MainShowings : Controller
    {

        private readonly ApplicationDbContext _context;

        private Dictionary<string, List<(DateTime date, float Duration, string startHour)>> _shows =
            new Dictionary<string, List<(DateTime date, float Duration, string startHour)>>();


        private Dictionary<string, (string Category, string ScreenRoom, string Src)> _movieDetails =
            new Dictionary<string, (string Category, string ScreenRoom, string Src)>();

        public MainShowings(ApplicationDbContext context)
        {
            _context = context;
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
                            SRC = movie.FilePath
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
                        _movieDetails.Add(movie.Title, (movie.CategoryName, movie.ScreenRoom, movie.SRC));
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
    }
}
