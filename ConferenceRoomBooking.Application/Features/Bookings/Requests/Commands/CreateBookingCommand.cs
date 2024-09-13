using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Bookings.Requests.Commands
{
    public class CreateBookingCommand : IRequest<Result<BookingDto>>
    {
        public required CreateBookingRequestDto CreateBookingRequestDto { get; set; }
    }
}
