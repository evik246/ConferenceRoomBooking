using ConferenceRoomBooking.Bll;

namespace ConferenceRoomBooking.Services.API
{
    public static class ApiServicesRegistration
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(BllServicesRegistration).Assembly
            ));

            return services;
        }
    }
}
