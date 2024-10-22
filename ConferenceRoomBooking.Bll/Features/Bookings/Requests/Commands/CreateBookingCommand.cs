using ConferenceRoomBooking.Bll.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Requests.Commands
{
    public class CreateBookingCommand : IRequest<Result<BookingDto>>
    {
        public required CreateBookingRequestDto CreateBookingRequestDto { get; set; }
    }
}
