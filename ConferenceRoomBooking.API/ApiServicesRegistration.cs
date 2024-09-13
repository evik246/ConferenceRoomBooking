using ConferenceRoomBooking.Application;

namespace ConferenceRoomBooking.API
{
    public static class ApiServicesRegistration
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationServicesRegistration).Assembly
            ));

            return services;
        }
    }
}
