using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries
{
    public class GetBookingReportRequest : IRequest<Result<BookingReport>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
