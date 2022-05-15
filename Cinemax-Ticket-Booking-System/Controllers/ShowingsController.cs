using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinemax_Ticket_Booking_System.Data;
using Cinemax_Ticket_Booking_System.Models;

namespace Cinemax_Ticket_Booking_System.Controllers
{
    public class ShowingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Showings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Showing.Include(s => s.Movie).Include(s => s.ScreeningRoom);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Showings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showing
                .Include(s => s.Movie)
                .Include(s => s.ScreeningRoom)
                .FirstOrDefaultAsync(m => m.IDS == id);
            if (showing == null)
            {
                return NotFound();
            }

            return View(showing);
        }

        // GET: Showings/Create
        public IActionResult Create()
        {
            ViewData["IDMovie"] = new SelectList(_context.Movie, "Id", "Title");
            ViewData["IDScreenRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name");
            return View();
        }

        // POST: Showings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDS,ShowStart,AvailibleSeats,Price,IDMovie,IDScreenRoom")] Showing showing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDMovie"] = new SelectList(_context.Movie, "Id", "Title", showing.IDMovie);
            ViewData["IDScreenRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name", showing.IDScreenRoom);
            return View(showing);
        }

        // GET: Showings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showing.FindAsync(id);
            if (showing == null)
            {
                return NotFound();
            }
            ViewData["IDMovie"] = new SelectList(_context.Movie, "Id", "Title", showing.IDMovie);
            ViewData["IDScreenRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name", showing.IDScreenRoom);
            return View(showing);
        }

        // POST: Showings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDS,ShowStart,AvailibleSeats,Price,IDMovie,IDScreenRoom")] Showing showing)
        {
            if (id != showing.IDS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowingExists(showing.IDS))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDMovie"] = new SelectList(_context.Movie, "Id", "Title", showing.IDMovie);
            ViewData["IDScreenRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name", showing.IDScreenRoom);
            return View(showing);
        }

        // GET: Showings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showing
                .Include(s => s.Movie)
                .Include(s => s.ScreeningRoom)
                .FirstOrDefaultAsync(m => m.IDS == id);
            if (showing == null)
            {
                return NotFound();
            }

            return View(showing);
        }

        // POST: Showings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showing = await _context.Showing.FindAsync(id);
            _context.Showing.Remove(showing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowingExists(int id)
        {
            return _context.Showing.Any(e => e.IDS == id);
        }
    }
}
