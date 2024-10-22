using AutoMapper;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Bll.Common.Entities;
using MediatR;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Bll.Exceptions;
using ConferenceRoomBooking.Bll.Responces;
using ConferenceRoomBooking.Bll.Contracts.Repositories;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Commands
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
                return new Result<ConferenceRoomDto>(new ValidationModelException(validationResult));
            }

            var conferenceRoom = _mapper.Map<ConferenceRoom>(request.CreateConferenceRoomRequestDto);

            if (request.CreateConferenceRoomRequestDto.ServiceIds != null && 
                request.CreateConferenceRoomRequestDto.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilterDto() { Guids = request.CreateConferenceRoomRequestDto.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.MatchSuccess(
                    services => services.ToList()
                );

                if (services.Count != request.CreateConferenceRoomRequestDto.ServiceIds.Count)
                {
                    return new Result<ConferenceRoomDto>(new NotFoundException(nameof(Service)));
                }

                conferenceRoom.Services = services;
            }

            var createdConferenceRoomResult = await _conferenceRoomRepository.AddAsync(conferenceRoom);

            return createdConferenceRoomResult.Match(
                createdConferenceRoom => new Result<ConferenceRoomDto>(_mapper.Map<ConferenceRoomDto>(createdConferenceRoom)),
                exception => new Result<ConferenceRoomDto>(exception)
            );
        }
    }
}
