using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Services.Requests.Queries
{
    public class GetServiceRequest : IRequest<Result<Service>>
    {
        public required Guid Id {  get; set; }
    }
}
