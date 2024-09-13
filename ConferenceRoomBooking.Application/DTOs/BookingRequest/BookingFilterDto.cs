namespace ConferenceRoomBooking.Application.DTOs.BookingRequest
{
    public class BookingFilterDto : BaseFilterDto
    {
        public List<Guid>? Guids { get; set; }
    }
}
