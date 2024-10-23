using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConferenceRoomBooking.Bll
{
    public static class BllServicesRegistration
    {
        public static IServiceCollection ConfigureBllServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}