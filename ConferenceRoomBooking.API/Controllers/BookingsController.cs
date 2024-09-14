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

        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> Get([FromQuery] BookingFilterDto value)
        {
            var request = new GetBookingListRequest() { BookingFilterDto = value };
            var bookingsResult = await _mediator.Send(request);

            return bookingsResult.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post([FromBody] CreateBookingRequestDto value)
        {
            var command = new CreateBookingCommand() { CreateBookingRequestDto = value };
            var createdBookingResult = await _mediator.Send(command);

            return createdBookingResult.ToActionResult(StatusCodes.Status201Created);
        }
    }
}
