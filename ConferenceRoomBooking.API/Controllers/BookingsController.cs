using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.Exceptions;
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

        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> Get([FromQuery] BookingFilterDto value)
        {
            var request = new GetBookingListRequest() { BookingFilterDto = value };
            var bookingsResult = await _mediator.Send(request);

            return bookingsResult.Match<ActionResult>(
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

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post([FromBody] CreateBookingRequestDto value)
        {
            var command = new CreateBookingCommand() { CreateBookingRequestDto = value };
            var createdBookingResult = await _mediator.Send(command);

            return createdBookingResult.Match<ActionResult>(
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
