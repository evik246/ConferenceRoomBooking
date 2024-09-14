using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts.Services
{
    public interface IBookingService
    {
        decimal CalculateTotalPrice(Booking booking);
        decimal CalculateTotalPrice(DateTime bookingTime, int hourAmount, decimal basePrice, params int[] servicePrices);
    }
}
