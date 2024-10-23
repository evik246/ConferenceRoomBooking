namespace ConferenceRoomBooking.Bll.Common.Filters.BookingRequest
{
    public class BookingFilter : BaseFilter
    {
        public List<Guid>? Guids { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
