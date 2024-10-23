using ConferenceRoomBooking.Dal.Db.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Dal.Db.Entities
{
    public class ConferenceRoomEntity : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public ICollection<ServiceEntity> Services { get; set; } = [];
        [Precision(8, 2)]
        public decimal PricePerHour { get; set; }
    }
}
