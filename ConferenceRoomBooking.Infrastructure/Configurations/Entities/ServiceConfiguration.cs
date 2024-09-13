using ConferenceRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomBooking.Infrastructure.Configurations.Entities
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasData
            (
                new Service
                {
                    Id = Guid.NewGuid(),
                    Name = "Проєктор",
                    Price = 500
                },
                new Service
                {
                    Id = Guid.NewGuid(),
                    Name = "Wi-Fi",
                    Price = 300
                },
                new Service
                {
                    Id = Guid.NewGuid(),
                    Name = "Звук",
                    Price = 700
                }
            );
        }
    }
}
