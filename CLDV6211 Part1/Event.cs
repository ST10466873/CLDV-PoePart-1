//CLDV poe Part 3 st10466873
using System.ComponentModel.DataAnnotations;

namespace CLDV6211_Part1.Models
{
    public class Event
    {
        public int EventID { get; set; }
        [Required]
        public string EventName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        public string EventType { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
