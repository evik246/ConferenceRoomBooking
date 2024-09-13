using AutoMapper;
using ConferenceRoomBooking.Application.Contracts;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.DTOs.BookingRequest.Validators;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Features.Bookings.Requests.Commands;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Bookings.Handlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper, IServiceRepository serviceRepository)
        {
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Result<BookingDto>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBookingRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateBookingRequestDto);

            if (!validationResult.IsValid)
            {
                return new Result<BookingDto>(new ValidationException(validationResult));
            }

            var booking = _mapper.Map<Booking>(request.CreateBookingRequestDto);

            if (request.CreateBookingRequestDto.ServiceIds != null &&
                request.CreateBookingRequestDto.ServiceIds.Any())
            {
                var serviceFilterDto = _mapper.Map<ServiceFilterDto>(request.CreateBookingRequestDto);
                var servicesResult = await _serviceRepository.GetAsync(serviceFilterDto);

                servicesResult.MatchSuccess(
                    services => booking.Services = services.ToList()
                );

                return servicesResult.MatchFailure(
                    exception => new Result<BookingDto>(exception)
                );
            }

            var bookingResult = await _bookingRepository.AddAsync(booking);

            return bookingResult.Match(
                createdBooking => new Result<BookingDto>(_mapper.Map<BookingDto>(createdBooking)),
                exception => new Result<BookingDto>(exception)
            );
        }
    }
}
