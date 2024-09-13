using ConferenceRoomBooking.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Domain.Entities
{
    public class Service : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Precision(8, 2)]
        public decimal Price { get; set; }
        public ICollection<ConferenceRoom> ConferenceRooms { get; set; } = [];
        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
