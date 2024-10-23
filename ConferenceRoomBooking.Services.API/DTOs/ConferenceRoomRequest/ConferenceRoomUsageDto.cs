namespace ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest
{
    public class ConferenceRoomUsageDto
    {
        public string RoomName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
