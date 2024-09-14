using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Exceptions;
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

            return servicesResult.Match<ActionResult>(
                result =>
                {
                    if (result.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(result);
                },
                exception =>
                {
                    return exception switch
                    {
                        ValidationModelException validationEx => BadRequest(validationEx.Errors),
                        _ => StatusCode(500),
                    };
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> Get(Guid id)
        {
            var request = new GetServiceRequest() { Id = id };
            var serviceResult = await _mediator.Send(request);

            return serviceResult.Match<ActionResult>(
                result => Ok(result),
                exception =>
                {
                    return exception switch
                    {
                        NotFoundException notFoundEx => NotFound(notFoundEx.Message),
                        ValidationModelException validationEx => BadRequest(validationEx.Errors),
                        _ => StatusCode(500),
                    };
                }
            );
        }
    }
}
