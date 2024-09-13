using AutoMapper;
using ConferenceRoomBooking.Application.Contracts;
using ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Handlers.Commands
{
    public class DeleteConferenceRoomCommandHandler : IRequestHandler<DeleteConferenceRoomCommand, Result>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IMapper _mapper;
        public DeleteConferenceRoomCommandHandler(IConferenceRoomRepository conferenceRoomRepository, IMapper mapper)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(DeleteConferenceRoomCommand request, CancellationToken cancellationToken)
        {
            var conferenceRoom = new ConferenceRoom() { Id = request.Id };
            var deletedConferenceRoomResult = await _conferenceRoomRepository.DeleteAsync(conferenceRoom);

            return deletedConferenceRoomResult;
        }
    }
}
