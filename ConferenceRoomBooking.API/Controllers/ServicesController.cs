using ConferenceRoomBooking.API.Extensions;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Features.Services.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
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
            var request = new GetServiceListRequest() { ServiceFilterDto = value };
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
