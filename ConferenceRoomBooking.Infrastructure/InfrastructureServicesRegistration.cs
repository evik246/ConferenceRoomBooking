using ConferenceRoomBooking.Application.Contracts;
using ConferenceRoomBooking.Infrastructure.Repositories;
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

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            return services;
        }
    }
}
