using ConferenceRoomBooking.Domain.Entities.Common;

namespace ConferenceRoomBooking.Domain.Entities
{
    public class Booking : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public Guid ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; } = new();
        public DateTime DateTime { get; set; }
        public int HourAmount { get; set; }
        public ICollection<Service> Services { get; set; } = [];
    }
}
