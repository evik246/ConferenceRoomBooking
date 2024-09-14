using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Application.DTOs.BookingRequest
{
    public class BookingReportDto
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ConferenceRoomUsageDto> RoomUsages { get; set; } = [];
        public List<ServiceUsageDto> ServiceUsages { get; set; } = [];
    }
}
