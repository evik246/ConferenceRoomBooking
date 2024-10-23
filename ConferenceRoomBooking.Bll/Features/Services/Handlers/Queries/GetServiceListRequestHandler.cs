using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Features.Services.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Features.Services.Handlers.Queries
{
    public class GetServiceListRequestHandler : IRequestHandler<GetServiceListRequest, Result<List<Service>>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceListRequestHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<Result<List<Service>>> Handle(GetServiceListRequest request, CancellationToken cancellationToken)
        {
            var servicesResult = await _serviceRepository.GetAsync(request.ServiceFilterDto);

            return servicesResult.Match(
                result => new Result<List<Service>>(result.ToList()),
                exception => new Result<List<Service>>(exception)
            );
        }
    }
}
