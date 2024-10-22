using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Services.Requests.Queries
{
    public class GetServiceRequest : IRequest<Result<ServiceDto>>
    {
        public required Guid Id {  get; set; }
    }
}
