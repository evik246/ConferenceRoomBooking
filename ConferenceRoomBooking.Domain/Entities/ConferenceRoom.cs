using ConferenceRoomBooking.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Domain.Entities
{
    public class ConferenceRoom : BaseDomainEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public ICollection<Service> Services { get; set; } = [];
        [Precision(8, 2)]
        public decimal PricePerHour { get; set; }
    }
}
