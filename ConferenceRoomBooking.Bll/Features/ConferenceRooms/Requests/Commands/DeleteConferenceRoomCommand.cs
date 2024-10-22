using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands
{
    public class DeleteConferenceRoomCommand : IRequest<Result>
    {
        public required Guid Id { get; set; }
    }
}
