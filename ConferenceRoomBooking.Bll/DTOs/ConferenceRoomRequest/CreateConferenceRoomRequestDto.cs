namespace ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest
{
    public class CreateConferenceRoomRequestDto
    {
        public required string Name { get; set; }
        public required int Capacity { get; set; }
        public ICollection<Guid>? ServiceIds { get; set; }
        public required decimal PricePerHour { get; set; }
    }
}
