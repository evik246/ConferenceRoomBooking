using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Services.Requests.Queries
{
    public class GetServiceListRequest : IRequest<Result<List<ServiceDto>>>
    {
        public required ServiceFilterDto ServiceFilterDto { get; set; }
    }
}
