using ConferenceRoomBooking.Bll.Common.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries
{
    public class GetBookingListRequest : IRequest<Result<List<BookingDto>>>
    {
        public required BookingFilterDto BookingFilterDto { get; set; }
    }
}
