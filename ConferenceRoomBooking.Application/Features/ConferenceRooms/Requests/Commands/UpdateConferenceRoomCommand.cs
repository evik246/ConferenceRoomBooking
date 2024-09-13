using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands
{
    public class UpdateConferenceRoomCommand : IRequest<Result<ConferenceRoomDto>>
    {
        public required Guid Id { get; set; }
        public required UpdateConferenceRoomRequestDto UpdateConferenceRoomRequestDto { get; set; }
    }
}
