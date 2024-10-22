namespace ConferenceRoomBooking.Bll.DTOs.ServiceRequest
{
    public class ServiceFilterDto : BaseFilterDto
    {
        public List<Guid>? Guids { get; set; }
    }
}
