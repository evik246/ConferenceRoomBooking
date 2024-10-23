using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Services.API.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Services.API.DTOs.BookingRequest
{
    public class BookingReportDto
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ConferenceRoomUsageDto> RoomUsages { get; set; } = [];
        public List<ServiceUsageDto> ServiceUsages { get; set; } = [];
    }
}
