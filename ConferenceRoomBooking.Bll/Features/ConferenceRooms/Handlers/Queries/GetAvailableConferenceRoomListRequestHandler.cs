using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest.Validators;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Queries
{
    public class GetAvailableConferenceRoomListRequestHandler : IRequestHandler<GetAvailableConferenceRoomListRequest, Result<List<ConferenceRoom>>>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public GetAvailableConferenceRoomListRequestHandler(IConferenceRoomRepository conferenceRoomRepository) 
        {
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<Result<List<ConferenceRoom>>> Handle(GetAvailableConferenceRoomListRequest request, CancellationToken cancellationToken)
        {
            var validator = new ConferenceRoomFilterValidator();
            var validationResult = await validator.ValidateAsync(request.ConferenceRoomFilter);

            if (!validationResult.IsValid)
            {
                return new Result<List<ConferenceRoom>>(new ValidationModelException(validationResult));
            }

            var availableRoomsResult = await _conferenceRoomRepository.GetAvailableRoomsAsync(request.ConferenceRoomFilter);

            return availableRoomsResult.Match(
                availableRooms => new Result<List<ConferenceRoom>>(availableRooms.ToList()),
                exception => new Result<List<ConferenceRoom>>(exception)
            );
        }
    }
}
