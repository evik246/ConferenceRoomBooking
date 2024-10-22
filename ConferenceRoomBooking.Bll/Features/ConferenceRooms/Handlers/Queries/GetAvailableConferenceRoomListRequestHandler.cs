using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Queries
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
                return new Result<List<ConferenceRoomDto>>(new ValidationModelException(validationResult));
            }

            var availableRoomsResult = await _conferenceRoomRepository.GetAvailableRoomsAsync(request.ConferenceRoomFilterDto);

            return availableRoomsResult.Match(
                availableRooms => new Result<List<ConferenceRoomDto>>(_mapper.Map<List<ConferenceRoomDto>>(availableRooms.ToList())),
                exception => new Result<List<ConferenceRoomDto>>(exception)
            );
        }
    }
}
