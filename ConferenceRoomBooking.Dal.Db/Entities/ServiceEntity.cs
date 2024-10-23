using ConferenceRoomBooking.Dal.Db.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Dal.Db.Entities
{
    public class ServiceEntity : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Precision(8, 2)]
        public decimal Price { get; set; }
        public ICollection<ConferenceRoomEntity> ConferenceRooms { get; set; } = [];
        public ICollection<BookingEntity> Bookings { get; set; } = [];
    }
}
