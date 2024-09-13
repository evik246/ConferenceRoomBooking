using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Queries
{
    public class GetAvailableConferenceRoomListRequest : IRequest<Result<List<ConferenceRoomDto>>>
    {
        public required ConferenceRoomFilterDto ConferenceRoomFilterDto { get; set; }
    }
}
