using ConferenceRoomBooking.Services.API.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Common.Models;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking, BookingFilterDto>
    {
    }
}
