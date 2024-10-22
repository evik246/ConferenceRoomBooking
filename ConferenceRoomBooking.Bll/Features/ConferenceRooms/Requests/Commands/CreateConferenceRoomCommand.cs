using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands
{
    public class CreateConferenceRoomCommand : IRequest<Result<ConferenceRoomDto>>
    {
        public required CreateConferenceRoomRequestDto CreateConferenceRoomRequestDto { get; set; }
    }
}
