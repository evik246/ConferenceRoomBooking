namespace ConferenceRoomBooking.Services.API.DTOs.ServiceRequest
{
    public class ServiceUsageDto
    {
        public string ServiceName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
