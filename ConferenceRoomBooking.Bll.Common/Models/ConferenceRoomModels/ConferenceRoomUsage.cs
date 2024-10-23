namespace ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels
{
    public class ConferenceRoomUsage
    {
        public string RoomName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
