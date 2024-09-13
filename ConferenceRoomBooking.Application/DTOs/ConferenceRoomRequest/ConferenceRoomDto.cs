using ConferenceRoomBooking.Application.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest
{
    public class ConferenceRoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public ICollection<ServiceDto> Services { get; set; } = [];
        public decimal PricePerHour { get; set; }
    }
}
