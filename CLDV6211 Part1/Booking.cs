using System.ComponentModel.DataAnnotations;

namespace CLDV6211_Part1.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        
        public int VenueID { get; set; }
        
        public int EventID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SpecialistName { get; set; }

        public Venue? Venue { get; set; }
        public Event? Event { get; set; }
    }
}
