using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Bll.DTOs.BookingRequest
{
    public class BookingReportDto
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ConferenceRoomUsageDto> RoomUsages { get; set; } = [];
        public List<ServiceUsageDto> ServiceUsages { get; set; } = [];
    }
}
