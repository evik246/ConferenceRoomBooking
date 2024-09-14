using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Features.Bookings.Requests.Queries;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Bookings.Handlers.Queries
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
