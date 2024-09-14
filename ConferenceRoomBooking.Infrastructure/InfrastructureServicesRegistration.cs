using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.Contracts.Services;
using ConferenceRoomBooking.Infrastructure.Repositories;
using ConferenceRoomBooking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceRoomBooking.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ConferenceRoomBookingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConferenceRoomBookingConnectionString")
            ));

            services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBookingPriceCalculationService, BookingPriceCalculationService>();

            return services;
        }
    }
}
