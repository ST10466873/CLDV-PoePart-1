//St10466873 CLDV6211 poePart3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLDV6211_Part1.Data;
using CLDV6211_Part1.Models;

namespace CLDV6211_Part1.Controllers
{
    public class Bookings1Controller : Controller
    {
        private readonly EventEaseDbContext _context;

        public Bookings1Controller(EventEaseDbContext context)
        {
            _context = context;
        }

        // GET: Bookings1
        public async Task<IActionResult> Index(string searchString, string eventType, DateTime? startDate, DateTime? endDate)
        {
            ViewData["EventTypes"] = new List<string> { "Conference", "Wedding", "Concert", "Corporate", "Party", "Other" };

            var bookingsQuery = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                bookingsQuery = bookingsQuery.Where(s => s.Event.EventName.Contains(searchString)
                                                || s.BookingID.ToString() == searchString);
            }

            if (!string.IsNullOrEmpty(eventType))
            {
                bookingsQuery = bookingsQuery.Where(x => x.Event.EventType == eventType);
            }

            if (startDate.HasValue)
            {
                bookingsQuery = bookingsQuery.Where(x => x.StartDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                bookingsQuery = bookingsQuery.Where(x => x.EndDate <= endDate.Value);
            }

            var finalResult = await bookingsQuery.Select(b => new BookingViewModel
            {
                BookingID = b.BookingID,
                VenueName = b.Venue.Name,
                EventName = b.Event.EventName,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                SpecialistName = b.SpecialistName,
                VenueImageUrl = b.Venue.ImageUrl
            }).ToListAsync();

            return View(finalResult);
        }

        // GET: Bookings1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings1/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName");
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location");
            return View();
        }

        // POST: Bookings1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,VenueID,EventID,StartDate,EndDate,SpecialistName")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location", booking.VenueID);
            return View(booking);
        }

        // GET: Bookings1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location", booking.VenueID);
            return View(booking);
        }

        // POST: Bookings1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,VenueID,EventID,StartDate,EndDate,SpecialistName")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Location", booking.VenueID);
            return View(booking);
        }

        // GET: Bookings1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
    }
}
