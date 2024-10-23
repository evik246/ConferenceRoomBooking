using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Requests.Commands
{
    public class CreateBookingCommand : IRequest<Result<Booking>>
    {
        public required Booking CreateBookingRequest { get; set; }
    }
}
