using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.Contracts.Services;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.Features.Bookings.Requests.Queries;
using ConferenceRoomBooking.Application.Responces;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Bookings.Handlers.Queries
{
    public class GetBookingListRequestHandler : IRequestHandler<GetBookingListRequest, Result<List<BookingDto>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IBookingPriceCalculationService _priceCalculationService;

        public GetBookingListRequestHandler(IBookingRepository bookingRepository, IMapper mapper, IBookingPriceCalculationService priceCalculationService)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _priceCalculationService = priceCalculationService;
        }

        public async Task<Result<List<BookingDto>>> Handle(GetBookingListRequest request, CancellationToken cancellationToken)
        {
            var bookingResult = await _bookingRepository.GetAsync(request.BookingFilterDto);

            return bookingResult.Match(
            bookings => {
                var bookingDtos = _mapper.Map<List<BookingDto>>(bookings.ToList());

                foreach (var booking in bookings)
                {
                    var bookingDto = bookingDtos.First(b => b.Id == booking.Id);
                    bookingDto.TotalPrice = _priceCalculationService.CalculateTotalPrice(booking);
                }

                return new Result<List<BookingDto>>(bookingDtos);
            },
            exception => new Result<List<BookingDto>>(exception)
        );
        }
    }
}
