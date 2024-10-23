using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Contracts.Services;
using ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Handlers.Queries
{
    public class GetBookingListRequestHandler : IRequestHandler<GetBookingListRequest, Result<List<Booking>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingService _priceCalculationService;

        public GetBookingListRequestHandler(IBookingRepository bookingRepository, IBookingService priceCalculationService)
        {
            _bookingRepository = bookingRepository;
            _priceCalculationService = priceCalculationService;
        }

        public async Task<Result<List<Booking>>> Handle(GetBookingListRequest request, CancellationToken cancellationToken)
        {
            var bookingResult = await _bookingRepository.GetAsync(request.BookingFilter);

            return bookingResult.Match(
            bookings => {
                var bookingModels = bookings.ToList();

                foreach (var booking in bookings)
                {
                    var bookingModel = bookingModels.First(b => b.Id == booking.Id);
                    bookingModel.TotalPrice = _priceCalculationService.CalculateTotalPrice(booking);
                }

                return new Result<List<Booking>>(bookingModels);
            },
            exception => new Result<List<Booking>>(exception)
        );
        }
    }
}
