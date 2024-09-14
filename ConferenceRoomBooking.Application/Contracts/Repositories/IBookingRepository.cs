using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking, BookingFilterDto>
    {
    }
}
