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

        public ICollection<Booking>? Bookings { get; set; }
    }
}
