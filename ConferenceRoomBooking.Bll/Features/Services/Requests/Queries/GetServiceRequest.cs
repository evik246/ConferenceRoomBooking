using ConferenceRoomBooking.Bll.Common.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Services.Requests.Queries
{
    public class GetServiceRequest : IRequest<Result<ServiceDto>>
    {
        public required Guid Id {  get; set; }
    }
}
