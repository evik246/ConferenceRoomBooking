using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands
{
    public class UpdateConferenceRoomCommand : IRequest<Result<ConferenceRoom>>
    {
        public required ConferenceRoom UpdateConferenceRoomRequest { get; set; }
    }
}
