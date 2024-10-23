using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Commands
{
    public class UpdateConferenceRoomCommandHandler : IRequestHandler<UpdateConferenceRoomCommand, Result<ConferenceRoom>>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IServiceRepository _serviceRepository;

        public UpdateConferenceRoomCommandHandler(IConferenceRoomRepository conferenceRoomRepository, IServiceRepository serviceRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Result<ConferenceRoom>> Handle(UpdateConferenceRoomCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateConferenceRoomValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateConferenceRoomRequest);

            if (!validationResult.IsValid)
            {
                return new Result<ConferenceRoom>(new ValidationModelException(validationResult));
            }

            var conferenceRoomFilter = new ConferenceRoomFilter() { Guids = [request.UpdateConferenceRoomRequest.Id] };
            var ConferenceRoomResult = await _conferenceRoomRepository.GetAsync(conferenceRoomFilter);

            var conferenceRoom = ConferenceRoomResult.MatchSuccess(
                conferenceRooms => conferenceRooms.FirstOrDefault()
            );
            
            if (conferenceRoom == null)
            {
                return new Result<ConferenceRoom>(new NotFoundException(nameof(ConferenceRoom)));
            }

            if (request.UpdateConferenceRoomRequest.ServiceIds != null &&
                request.UpdateConferenceRoomRequest.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilter() { Guids = request.UpdateConferenceRoomRequest.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.MatchSuccess(
                    services => services.ToList()
                );

                if (services.Count != request.UpdateConferenceRoomRequest.ServiceIds.Count)
                {
                    return new Result<ConferenceRoom>(new NotFoundException(nameof(Service)));
                }

                request.UpdateConferenceRoomRequest.Services = services;
            }

            var updatedConferenceRoomResult = await _conferenceRoomRepository.UpdateAsync(request.UpdateConferenceRoomRequest);

            return updatedConferenceRoomResult.Match(
                updatedConferenceRoom => new Result<ConferenceRoom>(updatedConferenceRoom),
                exception => new Result<ConferenceRoom>(exception)
            );
        }
    }
}
