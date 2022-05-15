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
    public class RoomSeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomSeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomSeats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomSeat.Include(r => r.ScreeningRoom);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomSeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomSeat = await _context.RoomSeat
                .Include(r => r.ScreeningRoom)
                .FirstOrDefaultAsync(m => m.IdRS == id);
            if (roomSeat == null)
            {
                return NotFound();
            }

            return View(roomSeat);
        }

        // GET: RoomSeats/Create
        public IActionResult Create()
        {
            ViewData["IDScreeningRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name");
            return View();
        }

        // POST: RoomSeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRS,Row,Column,IDScreeningRoom")] RoomSeat roomSeat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomSeat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDScreeningRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name", roomSeat.IDScreeningRoom);
            return View(roomSeat);
        }

        // GET: RoomSeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomSeat = await _context.RoomSeat.FindAsync(id);
            if (roomSeat == null)
            {
                return NotFound();
            }
            ViewData["IDScreeningRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name", roomSeat.IDScreeningRoom);
            return View(roomSeat);
        }

        // POST: RoomSeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRS,Row,Column,IDScreeningRoom")] RoomSeat roomSeat)
        {
            if (id != roomSeat.IdRS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomSeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomSeatExists(roomSeat.IdRS))
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
            ViewData["IDScreeningRoom"] = new SelectList(_context.ScreeningRoom, "IDSR", "Name", roomSeat.IDScreeningRoom);
            return View(roomSeat);
        }

        // GET: RoomSeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomSeat = await _context.RoomSeat
                .Include(r => r.ScreeningRoom)
                .FirstOrDefaultAsync(m => m.IdRS == id);
            if (roomSeat == null)
            {
                return NotFound();
            }

            return View(roomSeat);
        }

        // POST: RoomSeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomSeat = await _context.RoomSeat.FindAsync(id);
            _context.RoomSeat.Remove(roomSeat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomSeatExists(int id)
        {
            return _context.RoomSeat.Any(e => e.IdRS == id);
        }
    }
}
