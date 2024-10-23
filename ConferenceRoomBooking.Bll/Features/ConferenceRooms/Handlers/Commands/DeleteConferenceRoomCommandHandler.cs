using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Commands
{
    public class DeleteConferenceRoomCommandHandler : IRequestHandler<DeleteConferenceRoomCommand, Result>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        public DeleteConferenceRoomCommandHandler(IConferenceRoomRepository conferenceRoomRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<Result> Handle(DeleteConferenceRoomCommand request, CancellationToken cancellationToken)
        {
            var conferenceRoomFilter = new ConferenceRoomFilter() { Guids = [request.Id] };
            var ConferenceRoomResult = await _conferenceRoomRepository.GetAsync(conferenceRoomFilter);

            var conferenceRoom = ConferenceRoomResult.MatchSuccess(
                conferenceRooms => conferenceRooms.FirstOrDefault()
            );

            if (conferenceRoom == null)
            {
                return new Result<ConferenceRoom>(new NotFoundException(nameof(ConferenceRoom)));
            }

            var deletedConferenceRoomResult = await _conferenceRoomRepository.DeleteAsync(conferenceRoom);

            return deletedConferenceRoomResult;
        }
    }
}
