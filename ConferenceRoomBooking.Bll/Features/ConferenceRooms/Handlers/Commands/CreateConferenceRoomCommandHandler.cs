using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Common.Responces;
using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Commands
{
    public class CreateConferenceRoomCommandHandler : IRequestHandler<CreateConferenceRoomCommand, Result<ConferenceRoom>>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IServiceRepository _serviceRepository;

        public CreateConferenceRoomCommandHandler(IConferenceRoomRepository conferenceRoomRepository, IServiceRepository serviceRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Result<ConferenceRoom>> Handle(CreateConferenceRoomCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateConferenceRoomValidator();
            var validationResult = await validator.ValidateAsync(request.CreateConferenceRoomRequest);

            if (!validationResult.IsValid)
            {
                return new Result<ConferenceRoom>(new ValidationModelException(validationResult));
            }

            var conferenceRoom = request.CreateConferenceRoomRequest;

            if (request.CreateConferenceRoomRequest.ServiceIds != null && 
                request.CreateConferenceRoomRequest.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilter() { Guids = request.CreateConferenceRoomRequest.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.MatchSuccess(
                    services => services.ToList()
                );

                if (services.Count != request.CreateConferenceRoomRequest.ServiceIds.Count)
                {
                    return new Result<ConferenceRoom>(new NotFoundException(nameof(Service)));
                }

                conferenceRoom.Services = services;
            }

            var createdConferenceRoomResult = await _conferenceRoomRepository.AddAsync(conferenceRoom);

            return createdConferenceRoomResult.Match(
                createdConferenceRoom => new Result<ConferenceRoom>(createdConferenceRoom),
                exception => new Result<ConferenceRoom>(exception)
            );
        }
    }
}
