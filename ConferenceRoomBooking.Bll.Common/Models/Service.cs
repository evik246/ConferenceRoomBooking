using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Bll.Common.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Precision(8, 2)]
        public decimal Price { get; set; }
        public ICollection<ConferenceRoom> ConferenceRooms { get; set; } = [];
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
