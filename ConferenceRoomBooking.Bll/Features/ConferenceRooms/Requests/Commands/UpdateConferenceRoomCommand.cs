using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands
{
    public class UpdateConferenceRoomCommand : IRequest<Result<ConferenceRoomDto>>
    {
        public required Guid Id { get; set; }
        public required UpdateConferenceRoomRequestDto UpdateConferenceRoomRequestDto { get; set; }
    }
}
