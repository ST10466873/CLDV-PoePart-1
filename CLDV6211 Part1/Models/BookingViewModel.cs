namespace CLDV6211_Part1.Models
{
    public class BookingViewModel
    {
        public int BookingID { get; set; }
        public string VenueName { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SpecialistName { get; set; }
        public string VenueImageUrl { get; set; }
    }
}
