using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Features.Services.Requests.Queries;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Services.Handlers.Queries
{
    public class GetServiceListRequestHandler : IRequestHandler<GetServiceListRequest, Result<List<ServiceDto>>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public GetServiceListRequestHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ServiceDto>>> Handle(GetServiceListRequest request, CancellationToken cancellationToken)
        {
            var servicesResult = await _serviceRepository.GetAsync(request.ServiceFilterDto);

            return servicesResult.Match(
                result => new Result<List<ServiceDto>>(_mapper.Map<List<ServiceDto>>(result.ToList())),
                exception => new Result<List<ServiceDto>>(exception)
            );
        }
    }
}
