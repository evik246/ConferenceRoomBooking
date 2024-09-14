using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Exceptions;
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
