using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts
{
    public interface IBookingRepository : IGenericRepository<Booking, BookingFilterDto>
    {
    }
}
