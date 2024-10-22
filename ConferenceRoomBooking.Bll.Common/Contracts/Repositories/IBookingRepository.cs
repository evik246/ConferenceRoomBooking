using ConferenceRoomBooking.Bll.Common.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking, BookingFilterDto>
    {
    }
}
