namespace ConferenceRoomBooking.Application.Contracts.Services
{
    public interface IPriceCalculationService
    {
        decimal CalculateTotalPrice(DateTime bookingTime, int hourAmount, decimal basePrice);
    }
}
