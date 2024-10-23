using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.Services.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Features.Services.Handlers.Queries
{
    public class GetServiceRequestHandler : IRequestHandler<GetServiceRequest, Result<Service>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceRequestHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Result<Service>> Handle(GetServiceRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = await _serviceRepository.GetAsync(new ServiceFilter { Guids = [request.Id] });

            return serviceResult.Match(
                result =>
                {
                    if (result.Count == 0)
                    {
                        return new Result<Service>(new NotFoundException(nameof(Service)));
                    }
                    return new Result<Service>(result.First());
                },
                exception => new Result<Service>(exception)
            );
        }
    }
}
