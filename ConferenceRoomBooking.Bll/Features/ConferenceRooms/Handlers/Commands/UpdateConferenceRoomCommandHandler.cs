using AutoMapper;
using ConferenceRoomBooking.Bll.Contracts.Repositories;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest.Validators;
using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Exceptions;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Bll.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.ConferenceRooms.Handlers.Commands
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

            var conferenceRoomFilter = new ConferenceRoomFilterDto() { Guids = [request.Id] };
            var ConferenceRoomResult = await _conferenceRoomRepository.GetAsync(conferenceRoomFilter);

            var conferenceRoom = ConferenceRoomResult.MatchSuccess(
                conferenceRooms => conferenceRooms.FirstOrDefault()
            );
            
            if (conferenceRoom == null)
            {
                return new Result<ConferenceRoomDto>(new NotFoundException(nameof(ConferenceRoom)));
            }

            _mapper.Map(request.UpdateConferenceRoomRequestDto, conferenceRoom);

            if (request.UpdateConferenceRoomRequestDto.ServiceIds != null &&
                request.UpdateConferenceRoomRequestDto.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilterDto() { Guids = request.UpdateConferenceRoomRequestDto.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.MatchSuccess(
                    services => services.ToList()
                );

                if (services.Count != request.UpdateConferenceRoomRequestDto.ServiceIds.Count)
                {
                    return new Result<ConferenceRoomDto>(new NotFoundException(nameof(Service)));
                }

                conferenceRoom.Services = services;
            }

            var updatedConferenceRoomResult = await _conferenceRoomRepository.UpdateAsync(conferenceRoom);

            return updatedConferenceRoomResult.Match(
                updatedConferenceRoom => new Result<ConferenceRoomDto>(_mapper.Map<ConferenceRoomDto>(updatedConferenceRoom)),
                exception => new Result<ConferenceRoomDto>(exception)
            );
        }
    }
}
