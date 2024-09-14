using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.ConferenceRooms.Handlers.Commands
{
    public class UpdateConferenceRoomCommandHandler : IRequestHandler<UpdateConferenceRoomCommand, Result<ConferenceRoomDto>>
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public UpdateConferenceRoomCommandHandler(IConferenceRoomRepository conferenceRoomRepository, IMapper mapper, IServiceRepository serviceRepository)
        {
            _conferenceRoomRepository = conferenceRoomRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Result<ConferenceRoomDto>> Handle(UpdateConferenceRoomCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateConferenceRoomRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateConferenceRoomRequestDto);

            if (!validationResult.IsValid)
            {
                return new Result<ConferenceRoomDto>(new ValidationModelException(validationResult));
            }

            var conferenceRoom = _mapper.Map<ConferenceRoom>(request.UpdateConferenceRoomRequestDto);
            conferenceRoom.Id = request.Id;

            if (request.UpdateConferenceRoomRequestDto.ServiceIds != null &&
                request.UpdateConferenceRoomRequestDto.ServiceIds.Any())
            {
                var serviceFilterDto = _mapper.Map<ServiceFilterDto>(request.UpdateConferenceRoomRequestDto);
                var servicesResult = await _serviceRepository.GetAsync(serviceFilterDto);

                servicesResult.MatchSuccess(
                    services => conferenceRoom.Services = services.ToList()
                );

                return servicesResult.MatchFailure(
                    exception => new Result<ConferenceRoomDto>(exception)
                );
            }

            var updatedConferenceRoomResult = await _conferenceRoomRepository.UpdateAsync(conferenceRoom);

            return updatedConferenceRoomResult.Match(
                updatedConferenceRoom => new Result<ConferenceRoomDto>(_mapper.Map<ConferenceRoomDto>(updatedConferenceRoom)),
                exception => new Result<ConferenceRoomDto>(exception)
            );
        }
    }
}
