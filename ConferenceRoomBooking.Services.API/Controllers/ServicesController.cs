using ConferenceRoomBooking.Services.API.Extensions;
using ConferenceRoomBooking.Services.API.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Features.Services.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ServicesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all services based on the specified filter criteria.
        /// </summary>
        /// <param name="value">The filter criteria used to retrieve services.</param>
        /// <returns>A list of services matching the filter criteria.</returns>
        /// <response code="200">Returns a list of services that meet the filter criteria.</response>
        /// <response code="204">No services match the filter criteria; no content is returned.</response>
        /// <response code="400">The filter criteria provided are invalid or incomplete.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>>> Get([FromQuery] ServiceFilterDto value)
        {
            var serviceFilter = _mapper.Map<ServiceFilter>(value);
            var request = new GetServiceListRequest() { ServiceFilterDto = serviceFilter };
            var servicesResult = await _mediator.Send(request);

            return servicesResult.ToActionResult();
        }

        /// <summary>
        /// Retrieves details of a specific service by its ID.
        /// </summary>
        /// <param name="id">The ID of the service to retrieve.</param>
        /// <returns>The details of the specified service.</returns>
        /// <response code="200">Returns the details of the specified service.</response>
        /// <response code="404">The service with the specified ID was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> Get(Guid id)
        {
            var request = new GetServiceRequest() { Id = id };
            var serviceResult = await _mediator.Send(request);

            return serviceResult.ToActionResult();
        }
    }
}
