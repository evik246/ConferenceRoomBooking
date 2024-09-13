using AutoMapper;
using ConferenceRoomBooking.Application.Contracts;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Queries;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Handlers.Queries
{
    public class GetAvailableConferenceRoomListRequestHandler : IRequestHandler<GetAvailableConferenceRoomListRequest, Result<List<ConferenceRoomDto>>>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IMapper _mapper;

        public GetAvailableConferenceRoomListRequestHandler(IConferenceRoomRepository conferenceRoomRepository, IMapper mapper) 
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ConferenceRoomDto>>> Handle(GetAvailableConferenceRoomListRequest request, CancellationToken cancellationToken)
        {
            var validator = new ConferenceRoomFilterDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ConferenceRoomFilterDto);

            if (!validationResult.IsValid)
            {
                return new Result<List<ConferenceRoomDto>>(new ValidationException(validationResult));
            }

            var availableRoomsResult = await _conferenceRoomRepository.GetAvailableRoomsAsync(request.ConferenceRoomFilterDto);

            return availableRoomsResult.Match(
                availableRooms => new Result<List<ConferenceRoomDto>>(_mapper.Map<List<ConferenceRoomDto>>(availableRooms.ToList())),
                exception => new Result<List<ConferenceRoomDto>>(exception)
            );
        }
    }
}
