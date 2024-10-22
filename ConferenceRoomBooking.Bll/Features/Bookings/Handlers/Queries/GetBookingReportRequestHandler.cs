using AutoMapper;
using ConferenceRoomBooking.Bll.Contracts.Repositories;
using ConferenceRoomBooking.Bll.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Handlers.Queries
{
    public class GetBookingReportRequestHandler : IRequestHandler<GetBookingReportRequest, Result<BookingReportDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingReportRequestHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Result<BookingReportDto>> Handle(GetBookingReportRequest request, CancellationToken cancellationToken)
        {
            var filter = new BookingFilterDto
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var bookingsResult = await _bookingRepository.GetAsync(filter);

            return bookingsResult.Match(
                bookings =>
                {
                    var bookingReport = new BookingReportDto
                    {
                        TotalBookings = bookings.Count,
                        TotalRevenue = bookings.Sum(b => b.HourAmount * b.ConferenceRoom.PricePerHour),
                        RoomUsages = bookings.GroupBy(b => b.ConferenceRoom.Name)
                            .Select(g => new ConferenceRoomUsageDto
                            {
                                RoomName = g.Key,
                                TotalBookings = g.Count(),
                                TotalRevenue = g.Sum(b => b.HourAmount * b.ConferenceRoom.PricePerHour)
                            })
                            .ToList(),
                        ServiceUsages = bookings.SelectMany(b => b.Services)
                            .GroupBy(s => s.Name)
                            .Select(g => new ServiceUsageDto
                            {
                                ServiceName = g.Key,
                                TotalBookings = g.Count(),
                                TotalRevenue = g.Sum(s => s.Price)
                            })
                            .ToList()
                    };

                    return new Result<BookingReportDto>(bookingReport);
                },
                exception => new Result<BookingReportDto>(exception)
            );
        }
    }
}
