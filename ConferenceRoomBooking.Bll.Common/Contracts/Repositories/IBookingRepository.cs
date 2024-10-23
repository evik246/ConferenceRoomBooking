using ConferenceRoomBooking.Bll.Common.Models.BookingModels;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking, BookingFilter>
    {
    }
}
