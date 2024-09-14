using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Features.Services.Requests.Queries;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Services.Handlers.Queries
{
    public class GetServiceRequestHandler : IRequestHandler<GetServiceRequest, Result<ServiceDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public GetServiceRequestHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Result<ServiceDto>> Handle(GetServiceRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = await _serviceRepository.GetAsync(new ServiceFilterDto { Guids = [request.Id] });

            return serviceResult.Match(
                result =>
                {
                    if (result.Count == 0)
                    {
                        return new Result<ServiceDto>(new NotFoundException(nameof(Service)));
                    }
                    return new Result<ServiceDto>(_mapper.Map<ServiceDto>(result.First()));
                },
                exception => new Result<ServiceDto>(exception)
            );
        }
    }
}
