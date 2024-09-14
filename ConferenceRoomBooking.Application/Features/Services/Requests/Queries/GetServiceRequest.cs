using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Services.Requests.Queries
{
    public class GetServiceRequest : IRequest<Result<ServiceDto>>
    {
        public required Guid Id {  get; set; }
    }
}
