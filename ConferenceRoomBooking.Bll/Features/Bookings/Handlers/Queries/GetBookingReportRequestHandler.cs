using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Handlers.Queries
{
    public class GetBookingReportRequestHandler : IRequestHandler<GetBookingReportRequest, Result<BookingReport>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingReportRequestHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<BookingReport>> Handle(GetBookingReportRequest request, CancellationToken cancellationToken)
        {
            var filter = new BookingFilter
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var bookingsResult = await _bookingRepository.GetAsync(filter);

            return bookingsResult.Match(
                bookings =>
                {
                    var bookingReport = new BookingReport
                    {
                        TotalBookings = bookings.Count,
                        TotalRevenue = bookings.Sum(b => b.HourAmount * b.ConferenceRoom.PricePerHour),
                        RoomUsages = bookings.GroupBy(b => b.ConferenceRoom.Name)
                            .Select(g => new ConferenceRoomUsage
                            {
                                RoomName = g.Key,
                                TotalBookings = g.Count(),
                                TotalRevenue = g.Sum(b => b.HourAmount * b.ConferenceRoom.PricePerHour)
                            })
                            .ToList(),
                        ServiceUsages = bookings.SelectMany(b => b.Services)
                            .GroupBy(s => s.Name)
                            .Select(g => new ServiceUsage
                            {
                                ServiceName = g.Key,
                                TotalBookings = g.Count(),
                                TotalRevenue = g.Sum(s => s.Price)
                            })
                            .ToList()
                    };

                    return new Result<BookingReport>(bookingReport);
                },
                exception => new Result<BookingReport>(exception)
            );
        }
    }
}
