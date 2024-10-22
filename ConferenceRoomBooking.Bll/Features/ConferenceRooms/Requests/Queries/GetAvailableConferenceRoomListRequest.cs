using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Queries
{
    public class GetAvailableConferenceRoomListRequest : IRequest<Result<List<ConferenceRoomDto>>>
    {
        public required ConferenceRoomFilterDto ConferenceRoomFilterDto { get; set; }
    }
}
