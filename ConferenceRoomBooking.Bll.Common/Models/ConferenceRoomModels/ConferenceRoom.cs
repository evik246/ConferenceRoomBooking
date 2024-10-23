using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels
{
    public class ConferenceRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public ICollection<Guid> ServiceIds { get; set; } = [];
        public ICollection<Service> Services { get; set; } = [];
        [Precision(8, 2)]
        public decimal PricePerHour { get; set; }
    }
}
