using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.Services.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Services.Handlers.Queries
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
