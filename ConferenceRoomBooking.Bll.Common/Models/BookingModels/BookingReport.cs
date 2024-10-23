using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Common.Models.BookingModels
{
    public class BookingReport
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ConferenceRoomUsage> RoomUsages { get; set; } = [];
        public List<ServiceUsage> ServiceUsages { get; set; } = [];
    }
}
