namespace ConferenceRoomBooking.Bll.Common.DTOs.ServiceRequest
{
    public class ServiceFilterDto : BaseFilterDto
    {
        public List<Guid>? Guids { get; set; }
    }
}
