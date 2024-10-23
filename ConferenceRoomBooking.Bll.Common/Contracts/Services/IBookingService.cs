using ConferenceRoomBooking.Bll.Common.Models.BookingModels;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Services
{
    public interface IBookingService
    {
        decimal CalculateTotalPrice(Booking booking);
        decimal CalculateTotalPrice(DateTime bookingTime, int hourAmount, decimal basePrice, params int[] servicePrices);
    }
}
