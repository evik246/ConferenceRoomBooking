using AutoMapper;
using ConferenceRoomBooking.Bll.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Contracts.Services;
using ConferenceRoomBooking.Bll.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.DTOs.BookingRequest.Validators;
using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Exceptions;
using ConferenceRoomBooking.Bll.Features.Bookings.Requests.Commands;
using ConferenceRoomBooking.Bll.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;
using MediatR;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Handlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IBookingService _priceCalculationService;
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper, IServiceRepository serviceRepository, IBookingService priceCalculationService, IConferenceRoomRepository conferenceRoomRepository)
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

            var conferenceRoom = conferenceRoomResult.MatchSuccess(
                rooms => rooms.FirstOrDefault()
            );

            if (conferenceRoom == null)
            {
                return new Result<BookingDto>(new NotFoundException(nameof(ConferenceRoom)));
            }

            var booking = _mapper.Map<Booking>(request.CreateBookingRequestDto);
            booking.ConferenceRoom = conferenceRoom;

            if (request.CreateBookingRequestDto.ServiceIds != null && 
                request.CreateBookingRequestDto.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilterDto() { Guids = request.CreateBookingRequestDto.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.MatchSuccess(
                    services => services.ToList()
                );

                if (services.Count != request.CreateBookingRequestDto.ServiceIds.Count)
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
