using ConferenceRoomBooking.Bll;
using System.Reflection;

namespace ConferenceRoomBooking.Services.API
{
    public static class ApiServicesRegistration
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(BllServicesRegistration).Assembly
            ));

            return services;
        }
    }
}
