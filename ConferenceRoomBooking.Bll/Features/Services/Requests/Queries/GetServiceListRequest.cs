using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Services.Requests.Queries
{
    public class GetServiceListRequest : IRequest<Result<List<ServiceDto>>>
    {
        public required ServiceFilterDto ServiceFilterDto { get; set; }
    }
}
