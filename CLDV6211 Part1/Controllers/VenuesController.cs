using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLDV6211_Part1.Data;   
using CLDV6211_Part1.Models; 

namespace CLDV6211_Part1.Controllers
{
    public class VenuesController : Controller
    {
        private readonly EventEaseDbContext _context;

        public VenuesController(EventEaseDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.Include(v => v.Bookings).FirstOrDefaultAsync(m => m.VenueID == id);

            if (venue != null && venue.Bookings != null && venue.Bookings.Any())
            {
                TempData["Error"] = "Cannot delete venue with active bookings.";
                return RedirectToAction(nameof(Index));
            }

            if (venue != null) _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
