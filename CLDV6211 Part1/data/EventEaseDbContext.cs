using Microsoft.EntityFrameworkCore;
using CLDV6211_Part1.Models; 
using System.Collections.Generic;

namespace CLDV6211_Part1.Data
{
    public class EventEaseDbContext : DbContext
    {
        public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}