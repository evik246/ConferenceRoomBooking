using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Queries
{
    public class GetAvailableConferenceRoomListRequest : IRequest<Result<List<ConferenceRoom>>>
    {
        public required ConferenceRoomFilter ConferenceRoomFilter { get; set; }
    }
}
