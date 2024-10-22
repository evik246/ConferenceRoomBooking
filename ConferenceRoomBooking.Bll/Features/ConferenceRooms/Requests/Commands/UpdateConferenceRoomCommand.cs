using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands
{
    public class UpdateConferenceRoomCommand : IRequest<Result<ConferenceRoomDto>>
    {
        public required Guid Id { get; set; }
        public required UpdateConferenceRoomRequestDto UpdateConferenceRoomRequestDto { get; set; }
    }
}
