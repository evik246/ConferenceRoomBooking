using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands
{
    public class DeleteConferenceRoomCommand : IRequest<Result>
    {
        public required Guid Id { get; set; }
    }
}
