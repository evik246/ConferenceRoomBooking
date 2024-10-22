using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Queries
{
    public class GetAvailableConferenceRoomListRequest : IRequest<Result<List<ConferenceRoomDto>>>
    {
        public required ConferenceRoomFilterDto ConferenceRoomFilterDto { get; set; }
    }
}
