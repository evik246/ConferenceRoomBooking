namespace ConferenceRoomBooking.Application.DTOs.ServiceRequest
{
    public class ServiceFilterDto : BaseFilterDto
    {
        public List<Guid>? Guids { get; set; }
    }
}
