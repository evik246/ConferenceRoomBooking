using AutoMapper;
using ConferenceRoomBooking.Application.Contracts;
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

        public GetBookingListRequestHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<BookingDto>>> Handle(GetBookingListRequest request, CancellationToken cancellationToken)
        {
            var bookingResult = await _bookingRepository.GetAsync(request.BookingFilterDto);

            return bookingResult.Match(
                bookings => new Result<List<BookingDto>>(_mapper.Map<List<BookingDto>>(bookings.ToList())),
                exception => new Result<List<BookingDto>>(exception)
            );
        }
    }
}
