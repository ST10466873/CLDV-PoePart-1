using System.ComponentModel.DataAnnotations;

namespace CLDV6211_Part1.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        [Required]
        public int VenueID { get; set; }
        [Required]
        public int EventID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string SpecialistName { get; set; }

        public Venue? Venue { get; set; }
        public Event? Event { get; set; }
    }
}
