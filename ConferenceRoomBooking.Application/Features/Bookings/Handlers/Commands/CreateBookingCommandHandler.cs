using AutoMapper;
using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.Contracts.Services;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.DTOs.BookingRequest.Validators;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Features.Bookings.Requests.Commands;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace ConferenceRoomBooking.Application.Features.Bookings.Handlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IBookingPriceCalculationService _priceCalculationService;
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper, IServiceRepository serviceRepository, IBookingPriceCalculationService priceCalculationService, IConferenceRoomRepository conferenceRoomRepository)
        {
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _priceCalculationService = priceCalculationService;
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<Result<BookingDto>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBookingRequestDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateBookingRequestDto);

            if (!validationResult.IsValid)
            {
                return new Result<BookingDto>(new ValidationModelException(validationResult));
            }

            var conferenceRoomFilter = new ConferenceRoomFilterDto() { Guids = [request.CreateBookingRequestDto.ConferenceRoomId] };
            var conferenceRoomResult = await _conferenceRoomRepository.GetAsync(conferenceRoomFilter);

            var conferenceRoom = conferenceRoomResult.Match(
                rooms => rooms.FirstOrDefault(),
                exception => null
            );

            if (conferenceRoom == null)
            {
                return new Result<BookingDto>(new NotFoundException(nameof(ConferenceRoom)));
            }

            var booking = _mapper.Map<Booking>(request.CreateBookingRequestDto);
            booking.ConferenceRoom = conferenceRoom;

            if (request.CreateBookingRequestDto.ServiceIds != null && request.CreateBookingRequestDto.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilterDto() { Guids = request.CreateBookingRequestDto.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.Match(
                    services => services.ToList(),
                    exception => null
                );

                if (services == null || services.Count != request.CreateBookingRequestDto.ServiceIds.Count)
                {
                    return new Result<BookingDto>(new NotFoundException(nameof(Service)));
                }

                var availableServiceIds = conferenceRoom.Services.Select(s => s.Id).ToHashSet();
                if (!request.CreateBookingRequestDto.ServiceIds.All(id => availableServiceIds.Contains(id)))
                {
                    return new Result<BookingDto>(new NotFoundException(nameof(Service)));
                }

                booking.Services = services;
            }

            var totalPrice = _priceCalculationService.CalculateTotalPrice(booking);

            var bookingResult = await _bookingRepository.AddAsync(booking);

            return bookingResult.Match(
                createdBooking =>
                {
                    var bookingDto = _mapper.Map<BookingDto>(createdBooking);
                    bookingDto.TotalPrice = totalPrice;
                    return new Result<BookingDto>(bookingDto);
                },
                exception => new Result<BookingDto>(exception)
            );
        }
    }
}
