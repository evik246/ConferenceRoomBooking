using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands
{
    public class CreateConferenceRoomCommand : IRequest<Result<ConferenceRoomDto>>
    {
        public required CreateConferenceRoomRequestDto CreateConferenceRoomRequestDto { get; set; }
    }
}
