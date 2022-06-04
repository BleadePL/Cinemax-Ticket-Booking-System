using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinemax_Ticket_Booking_System.Data;
using Cinemax_Ticket_Booking_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cinemax_Ticket_Booking_System.Controllers
{
    public class ScreeningRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScreeningRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScreeningRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScreeningRoom.ToListAsync());
        }

        // GET: ScreeningRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screeningRoom = await _context.ScreeningRoom
                .FirstOrDefaultAsync(m => m.IDSR == id);
            if (screeningRoom == null)
            {
                return NotFound();
            }

            return View(screeningRoom);
        }

        // GET: ScreeningRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScreeningRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDSR,Name,ScreenPattern")] ScreeningRoom screeningRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(screeningRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(screeningRoom);
        }

        // GET: ScreeningRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screeningRoom = await _context.ScreeningRoom.FindAsync(id);
            if (screeningRoom == null)
            {
                return NotFound();
            }
            return View(screeningRoom);
        }

        // POST: ScreeningRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDSR,Name,ScreenPattern")] ScreeningRoom screeningRoom)
        {
            if (id != screeningRoom.IDSR)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(screeningRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScreeningRoomExists(screeningRoom.IDSR))
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
            return View(screeningRoom);
        }

        // GET: ScreeningRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screeningRoom = await _context.ScreeningRoom
                .FirstOrDefaultAsync(m => m.IDSR == id);
            if (screeningRoom == null)
            {
                return NotFound();
            }

            return View(screeningRoom);
        }

        // POST: ScreeningRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var screeningRoom = await _context.ScreeningRoom.FindAsync(id);
            _context.ScreeningRoom.Remove(screeningRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScreeningRoomExists(int id)
        {
            return _context.ScreeningRoom.Any(e => e.IDSR == id);
        }
    }
}
