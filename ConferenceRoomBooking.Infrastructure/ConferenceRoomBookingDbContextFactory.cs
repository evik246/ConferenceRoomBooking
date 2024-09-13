using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ConferenceRoomBooking.Infrastructure
{
    public class ConferenceRoomBookingDbContextFactory :
        IDesignTimeDbContextFactory<ConferenceRoomBookingDbContext>
    {
        public ConferenceRoomBookingDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();

            var builder = new DbContextOptionsBuilder<ConferenceRoomBookingDbContext>();
            var connectionString = configuration.GetConnectionString("ConferenceRoomBookingConnectionString");

            builder.UseSqlServer(connectionString);

            return new ConferenceRoomBookingDbContext(builder.Options);
        }
    }
}
