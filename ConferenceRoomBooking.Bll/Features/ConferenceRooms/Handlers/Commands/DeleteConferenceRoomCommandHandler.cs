using AutoMapper;
using ConferenceRoomBooking.Bll.Contracts.Repositories;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Exceptions;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Bll.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Commands
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
            var conferenceRoomFilter = new ConferenceRoomFilterDto() { Guids = [request.Id] };
            var ConferenceRoomResult = await _conferenceRoomRepository.GetAsync(conferenceRoomFilter);

            var conferenceRoom = ConferenceRoomResult.MatchSuccess(
                conferenceRooms => conferenceRooms.FirstOrDefault()
            );

            if (conferenceRoom == null)
            {
                return new Result<ConferenceRoomDto>(new NotFoundException(nameof(ConferenceRoom)));
            }

            var deletedConferenceRoomResult = await _conferenceRoomRepository.DeleteAsync(conferenceRoom);

            return deletedConferenceRoomResult;
        }
    }
}
