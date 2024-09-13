using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Bookings.Requests.Queries
{
    public class GetBookingListRequest : IRequest<Result<List<BookingDto>>>
    {
        public required BookingFilterDto BookingFilterDto { get; set; }
    }
}
