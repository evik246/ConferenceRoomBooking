using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries
{
    public class GetBookingListRequest : IRequest<Result<List<Booking>>>
    {
        public required BookingFilter BookingFilter { get; set; }
    }
}
