using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Bll.Common.Models.ServiceModels
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Precision(8, 2)]
        public decimal Price { get; set; }
        public ICollection<Guid> ConferenceRoomIds { get; set; } = [];
        public ICollection<ConferenceRoom> ConferenceRooms { get; set; } = [];
        public ICollection<Guid> BookingIds { get; set; } = [];
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
