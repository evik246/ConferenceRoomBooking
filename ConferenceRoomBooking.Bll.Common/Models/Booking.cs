namespace ConferenceRoomBooking.Bll.Common.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; } = new();
        public DateTime DateTime { get; set; }
        public int HourAmount { get; set; }
        public ICollection<Service> Services { get; set; } = [];
    }
}
