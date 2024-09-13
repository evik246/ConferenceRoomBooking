namespace ConferenceRoomBooking.Application.DTOs.BookingRequest
{
    public class CreateBookingRequestDto
    {
        public required Guid ConferenceRoomId { get; set; }
        public required DateTime DateTime { get; set; }
        public required int HourAmount { get; set; }
        public ICollection<Guid>? ServiceIds { get; set; }
    }
}
