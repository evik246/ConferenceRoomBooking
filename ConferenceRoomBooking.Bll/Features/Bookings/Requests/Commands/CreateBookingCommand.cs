using ConferenceRoomBooking.Services.API.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Requests.Commands
{
    public class CreateBookingCommand : IRequest<Result<BookingDto>>
    {
        public required CreateBookingRequestDto CreateBookingRequestDto { get; set; }
    }
}
