using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Bll.DTOs.BookingRequest
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public ConferenceRoomDto ConferenceRoom { get; set; } = new();
        public DateTime DateTime { get; set; }
        public int HourAmount { get; set; }
        public ICollection<ServiceDto> Services { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}
