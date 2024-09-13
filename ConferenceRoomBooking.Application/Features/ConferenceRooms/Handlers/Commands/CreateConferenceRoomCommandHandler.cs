using AutoMapper;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Domain.Entities;
using MediatR;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Application.Contracts;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Handlers.Commands
{
    public class CreateConferenceRoomCommandHandler : IRequestHandler<CreateConferenceRoomCommand, Result<ConferenceRoomDto>>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public CreateConferenceRoomCommandHandler(IConferenceRoomRepository conferenceRoomRepository, IMapper mapper, IServiceRepository serviceRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Result<ConferenceRoomDto>> Handle(CreateConferenceRoomCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateConferenceRoomRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateConferenceRoomRequestDto);

            if (!validationResult.IsValid)
            {
                return new Result<ConferenceRoomDto>(new ValidationException(validationResult));
            }

            var conferenceRoom = _mapper.Map<ConferenceRoom>(request.CreateConferenceRoomRequestDto);

            if (request.CreateConferenceRoomRequestDto.ServiceIds != null && 
                request.CreateConferenceRoomRequestDto.ServiceIds.Any())
            {
                var serviceFilterDto = _mapper.Map<ServiceFilterDto>(request.CreateConferenceRoomRequestDto);
                var servicesResult = await _serviceRepository.GetAsync(serviceFilterDto);

                servicesResult.MatchSuccess(
                    services => conferenceRoom.Services = services.ToList()
                );

                return servicesResult.MatchFailure(
                    exception => new Result<ConferenceRoomDto>(exception)
                );
            }

            var createdConferenceRoomResult = await _conferenceRoomRepository.AddAsync(conferenceRoom);

            return createdConferenceRoomResult.Match(
                createdConferenceRoom => new Result<ConferenceRoomDto>(_mapper.Map<ConferenceRoomDto>(createdConferenceRoom)),
                exception => new Result<ConferenceRoomDto>(exception)
            );
        }
    }
}
