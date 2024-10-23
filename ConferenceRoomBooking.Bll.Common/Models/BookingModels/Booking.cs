using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Common.Models.BookingModels
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; } = new();
        public DateTime DateTime { get; set; }
        public int HourAmount { get; set; }
        public ICollection<Guid> ServiceIds { get; set; } = [];
        public ICollection<Service> Services { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}
