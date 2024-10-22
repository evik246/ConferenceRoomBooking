using ConferenceRoomBooking.Bll.Common.Contracts.Services;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Dal.Db.Services
{
    public class BookingService : IBookingService
    {
        public decimal CalculateTotalPrice(Booking booking)
        {
            var servicePrices = booking.Services?.Select(service => (int)service.Price).ToArray() ?? new int[0];

            var totalPrice = CalculateTotalPrice(booking.DateTime, booking.HourAmount, booking.ConferenceRoom.PricePerHour, servicePrices);

            return totalPrice;
        }

        public decimal CalculateTotalPrice(DateTime bookingTime, int hourAmount, decimal basePrice, params int[] servicePrices)
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

            foreach (var servicePrice in servicePrices)
            {
                totalPrice += servicePrice;
            }

            return totalPrice;
        }
    }
}
