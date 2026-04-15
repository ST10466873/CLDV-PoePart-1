using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLDV6211_Part1.Data;   
using CLDV6211_Part1.Models; 

namespace CLDV6211_Part1.Controllers
{
    public class BookingsController : Controller
    {
        private readonly EventEaseDbContext _context;

        public BookingsController(EventEaseDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueID,EventID,StartDate,EndDate,SpecialistName")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                bool conflict = _context.Bookings.Any(b =>
                    b.VenueID == booking.VenueID &&
                    ((booking.StartDate >= b.StartDate && booking.StartDate < b.EndDate) ||
                     (booking.EndDate > b.StartDate && booking.EndDate <= b.EndDate) ||
                     (booking.StartDate <= b.StartDate && booking.EndDate >= b.EndDate)));

                if (conflict)
                {
                    ModelState.AddModelError("", "The selected venue is already booked for this time.");
                    ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
                    ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", booking.VenueID);
                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }
    }
}