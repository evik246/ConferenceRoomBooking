using ConferenceRoomBooking.Application.Contracts.Services;

namespace ConferenceRoomBooking.Infrastructure.Services
{
    public class PriceCalculationService : IPriceCalculationService
    {
        public decimal CalculateTotalPrice(DateTime bookingTime, int hourAmount, decimal basePrice)
        {
            decimal totalPrice = 0;

            for (int i = 0; i < hourAmount; i++)
            {
                var currentHour = bookingTime.AddHours(i).Hour;

                if (currentHour >= 9 && currentHour < 18)
                {
                    totalPrice += basePrice;
                }
                else if (currentHour >= 18 && currentHour < 23)
                {
                    totalPrice += basePrice * 0.8m;
                }
                else if (currentHour >= 6 && currentHour < 9)
                {
                    totalPrice += basePrice * 0.9m;
                }
                else if (currentHour >= 12 && currentHour < 14)
                {
                    totalPrice += basePrice * 1.15m;
                }
            }

            return totalPrice;
        }
    }
}
