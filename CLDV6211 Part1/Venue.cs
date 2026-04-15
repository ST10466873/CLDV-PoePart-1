using System.ComponentModel.DataAnnotations;

namespace CLDV6211_Part1.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        public ICollection<Booking>? Bookings { get; set; }
    }
}
