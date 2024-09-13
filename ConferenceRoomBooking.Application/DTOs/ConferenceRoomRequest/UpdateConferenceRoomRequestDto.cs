namespace ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest
{
    public class UpdateConferenceRoomRequestDto
    {
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public ICollection<Guid>? ServiceIds { get; set; }
        public decimal? PricePerHour { get; set; }
    }
}
