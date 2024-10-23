using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Contracts.Services;
using ConferenceRoomBooking.Bll.Common.Exceptions;
using ConferenceRoomBooking.Bll.Features.Bookings.Requests.Commands;
using ConferenceRoomBooking.Bll.Common.Responces;
using MediatR;
using ConferenceRoomBooking.Services.API.DTOs.BookingRequest.Validators;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Bll.Features.Bookings.Handlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IBookingService _priceCalculationService;
        private readonly IConferenceRoomRepository _conferenceRoomRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IServiceRepository serviceRepository, IBookingService priceCalculationService, IConferenceRoomRepository conferenceRoomRepository)
        {
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
            _priceCalculationService = priceCalculationService;
            _conferenceRoomRepository = conferenceRoomRepository;
        }

        public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBookingValidator();
            var validationResult = await validator.ValidateAsync(request.CreateBookingRequest);

            if (!validationResult.IsValid)
            {
                return new Result<Booking>(new ValidationModelException(validationResult));
            }

            var conferenceRoomFilter = new ConferenceRoomFilter() { Guids = [request.CreateBookingRequest.ConferenceRoomId] };
            var conferenceRoomResult = await _conferenceRoomRepository.GetAsync(conferenceRoomFilter);

            var conferenceRoom = conferenceRoomResult.MatchSuccess(
                rooms => rooms.FirstOrDefault()
            );

            if (conferenceRoom == null)
            {
                return new Result<Booking>(new NotFoundException(nameof(ConferenceRoom)));
            }

            var booking = request.CreateBookingRequest;
            booking.ConferenceRoom = conferenceRoom;

            if (request.CreateBookingRequest.ServiceIds != null && 
                request.CreateBookingRequest.ServiceIds.Any())
            {
                var serviceFilter = new ServiceFilter() { Guids = request.CreateBookingRequest.ServiceIds.ToList() };
                var servicesResult = await _serviceRepository.GetAsync(serviceFilter);

                var services = servicesResult.MatchSuccess(
                    services => services.ToList()
                );

                if (services.Count != request.CreateBookingRequest.ServiceIds.Count)
                {
                    return new Result<Booking>(new NotFoundException(nameof(Service)));
                }

                var availableServiceIds = conferenceRoom.Services.Select(s => s.Id).ToHashSet();
                if (!request.CreateBookingRequest.ServiceIds.All(id => availableServiceIds.Contains(id)))
                {
                    return new Result<Booking>(new NotFoundException(nameof(Service)));
                }

                booking.Services = services;
            }

            var totalPrice = _priceCalculationService.CalculateTotalPrice(booking);

            var bookingResult = await _bookingRepository.AddAsync(booking);

            return bookingResult.Match(
                createdBooking =>
                {
                    createdBooking.TotalPrice = totalPrice;
                    return new Result<Booking>(createdBooking);
                },
                exception => new Result<Booking>(exception)
            );
        }
    }
}
