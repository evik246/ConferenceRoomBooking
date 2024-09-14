using ConferenceRoomBooking.API.Extensions;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.Features.Bookings.Requests.Commands;
using ConferenceRoomBooking.Application.Features.Bookings.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of all bookings.
        /// </summary>
        /// <param name="value">The filter criteria for bookings</param>
        /// <returns>A list of bookings matching the filter criteria.</returns>
        /// <response code="200">Returns a list of bookings that match the filter criteria.</response>
        /// <response code="204">No bookings match the filter criteria; no content is returned.</response>
        /// <response code="400">The request parameters are invalid or incomplete.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> Get([FromQuery] BookingFilterDto value)
        {
            var request = new GetBookingListRequest() { BookingFilterDto = value };
            var bookingsResult = await _mediator.Send(request);

            return bookingsResult.ToActionResult();
        }

        /// <summary>
        /// Creates a new booking for a conference room.
        /// </summary>
        /// <param name="value">The details of the booking to be created.</param>
        /// <returns>The created booking with its details.</returns>
        /// <response code="201">The booking was created successfully.</response>
        /// <response code="400">The provided booking details are invalid.</response>
        /// <response code="404">The specified room or services are not available.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post([FromBody] CreateBookingRequestDto value)
        {
            var command = new CreateBookingCommand() { CreateBookingRequestDto = value };
            var createdBookingResult = await _mediator.Send(command);

            return createdBookingResult.ToActionResult(StatusCodes.Status201Created);
        }
    }
}
