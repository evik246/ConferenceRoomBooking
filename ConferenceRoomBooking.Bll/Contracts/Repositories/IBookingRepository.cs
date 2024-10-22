using ConferenceRoomBooking.Bll.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Contracts.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking, BookingFilterDto>
    {
    }
}
