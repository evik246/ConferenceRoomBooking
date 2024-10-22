using AutoMapper;
using ConferenceRoomBooking.Bll.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Contracts.Services;
using ConferenceRoomBooking.Bll.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Features.Bookings.Requests.Queries;
using ConferenceRoomBooking.Bll.Responces;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Handlers.Queries
{
    public class GetBookingListRequestHandler : IRequestHandler<GetBookingListRequest, Result<List<BookingDto>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IBookingService _priceCalculationService;

        public GetBookingListRequestHandler(IBookingRepository bookingRepository, IMapper mapper, IBookingService priceCalculationService)
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
