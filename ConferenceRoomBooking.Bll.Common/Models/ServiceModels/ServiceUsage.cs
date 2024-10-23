namespace ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

public class ServiceUsage
{
    public string ServiceName { get; set; } = string.Empty;
    public int TotalBookings { get; set; }
    public decimal TotalRevenue { get; set; }
}
