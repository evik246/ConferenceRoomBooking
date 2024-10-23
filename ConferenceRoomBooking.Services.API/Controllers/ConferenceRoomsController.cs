using ConferenceRoomBooking.Services.API.Extensions;
using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Commands;
using ConferenceRoomBooking.Bll.Features.ConferenceRooms.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ConferenceRoomBooking.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceRoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ConferenceRoomsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of available conference rooms based on the specified criteria.
        /// </summary>
        /// <param name="value">The filter criteria used to retrieve available conference rooms.</param>
        /// <returns>A list of available conference rooms that match the filter criteria.</returns>
        /// <response code="200">Returns a list of available conference rooms that meet the filter criteria.</response>
        /// <response code="204">No conference rooms match the filter criteria; no content is returned.</response>
        /// <response code="400">The filter criteria provided are invalid or incomplete.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("available/")]
        public async Task<ActionResult<List<ConferenceRoomDto>>> GetAvailable([FromQuery] ConferenceRoomFilterDto value)
        {
            var request = new GetAvailableConferenceRoomListRequest() { ConferenceRoomFilterDto = value };
            var availableConferenceRoomsResult = await _mediator.Send(request);

            return availableConferenceRoomsResult.ToActionResult();
        }

        /// <summary>
        /// Creates a new conference room.
        /// </summary>
        /// <param name="value">The details of the conference room to be created.</param>
        /// <returns>The created conference room with its details.</returns>
        /// <response code="201">The conference room was created successfully.</response>
        /// <response code="400">The provided conference room details are invalid.</response>
        /// <response code="404">The services are not available.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost]
        public async Task<ActionResult<ConferenceRoomDto>> Post([FromBody] CreateConferenceRoomRequestDto value)
        {
            var command = new CreateConferenceRoomCommand() { CreateConferenceRoomRequestDto = value };
            var createdConferenceRoomResult = await _mediator.Send(command);

            return createdConferenceRoomResult.ToActionResult(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Updates the details of an existing conference room.
        /// </summary>
        /// <param name="id">The ID of the conference room to be updated.</param>
        /// <param name="value">The updated details of the conference room.</param>
        /// <returns>The updated conference room with its details.</returns>
        /// <response code="200">The conference room was updated successfully.</response>
        /// <response code="400">The provided update details are invalid.</response>
        /// <response code="404">The conference room with the specified ID was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ConferenceRoomDto>> Put(Guid id, [FromBody] UpdateConferenceRoomRequestDto value)
        {
            var command = new UpdateConferenceRoomCommand() { Id = id, UpdateConferenceRoomRequestDto = value };
            var updatedConferenceRoomResult = await _mediator.Send(command);

            return updatedConferenceRoomResult.ToActionResult();
        }

        /// <summary>
        /// Deletes an existing conference room.
        /// </summary>
        /// <param name="id">The ID of the conference room to be deleted.</param>
        /// <returns>A confirmation of the deletion.</returns>
        /// <response code="204">The conference room was deleted successfully.</response>
        /// <response code="404">The conference room with the specified ID was not found.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteConferenceRoomCommand() { Id = id };
            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }
    }
}
