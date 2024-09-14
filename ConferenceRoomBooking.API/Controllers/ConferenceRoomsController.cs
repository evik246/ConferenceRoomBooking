using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Application.Features.ConferenceRooms.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceRoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConferenceRoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available/")]
        public async Task<ActionResult<List<ConferenceRoomDto>>> GetAvailable([FromQuery] ConferenceRoomFilterDto value)
        {
            var request = new GetAvailableConferenceRoomListRequest() { ConferenceRoomFilterDto = value };
            var availableConferenceRoomsResult = await _mediator.Send(request);

            return availableConferenceRoomsResult.Match<ActionResult>(
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
                        ValidationModelException validationEx => BadRequest(validationEx.Message),
                        _ => StatusCode(500),
                    };
                }
            );
        }

        [HttpPost]
        public async Task<ActionResult<ConferenceRoomDto>> Post([FromBody] CreateConferenceRoomRequestDto value)
        {
            var command = new CreateConferenceRoomCommand() { CreateConferenceRoomRequestDto = value };
            var createdConferenceRoomResult = await _mediator.Send(command);

            return Ok(createdConferenceRoomResult.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConferenceRoomDto>> Put(Guid id, [FromBody] UpdateConferenceRoomRequestDto value)
        {
            var command = new UpdateConferenceRoomCommand() { Id = id, UpdateConferenceRoomRequestDto = value };
            var updatedConferenceRoomResult = await _mediator.Send(command);

            return Ok(updatedConferenceRoomResult.Value);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteConferenceRoomCommand() { Id = id };
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
