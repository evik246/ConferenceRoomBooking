using ConferenceRoomBooking.Dal.Db.Entities.Common;

namespace ConferenceRoomBooking.Dal.Db.Entities
{
    public class BookingEntity : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public Guid ConferenceRoomId { get; set; }
        public ConferenceRoomEntity ConferenceRoom { get; set; } = new();
        public DateTime DateTime { get; set; }
        public int HourAmount { get; set; }
        public ICollection<ServiceEntity> Services { get; set; } = [];
    }
}
