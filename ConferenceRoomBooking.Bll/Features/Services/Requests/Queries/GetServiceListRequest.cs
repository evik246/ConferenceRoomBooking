using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Services.Requests.Queries
{
    public class GetServiceListRequest : IRequest<Result<List<Service>>>
    {
        public required ServiceFilter ServiceFilterDto { get; set; }
    }
}
