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

        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>>> Get([FromQuery] ServiceFilterDto value)
        {
            var request = new GetServiceListRequest() { ServiceFilterDto = value };
            var servicesResult = await _mediator.Send(request);

            return Ok(servicesResult.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> Get(Guid id)
        {
            var request = new GetServiceListRequest() { ServiceFilterDto = new() { Guids = [id] } };
            var serviceResult = await _mediator.Send(request);

            return Ok(serviceResult.Value);
        }
    }
}
