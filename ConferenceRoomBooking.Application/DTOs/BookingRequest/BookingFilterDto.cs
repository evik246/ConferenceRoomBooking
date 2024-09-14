namespace ConferenceRoomBooking.Application.DTOs.BookingRequest
{
    public class BookingFilterDto : BaseFilterDto
    {
        public List<Guid>? Guids { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
