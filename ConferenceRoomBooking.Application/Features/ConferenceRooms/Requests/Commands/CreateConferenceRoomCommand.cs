using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands
{
    public class CreateConferenceRoomCommand : IRequest<Result<ConferenceRoomDto>>
    {
        public required CreateConferenceRoomRequestDto CreateConferenceRoomRequestDto { get; set; }
    }
}
