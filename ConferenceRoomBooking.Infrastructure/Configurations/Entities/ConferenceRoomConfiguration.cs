using ConferenceRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomBooking.Infrastructure.Configurations.Entities
{
    public class ConferenceRoomConfiguration : IEntityTypeConfiguration<ConferenceRoom>
    {
        public void Configure(EntityTypeBuilder<ConferenceRoom> builder)
        {
            builder.HasData
            (
                new ConferenceRoom
                {
                    Id = Guid.NewGuid(),
                    Name = "A",
                    Capacity = 50,
                    PricePerHour = 2000,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                },
                new ConferenceRoom
                {
                    Id = Guid.NewGuid(),
                    Name = "B",
                    Capacity = 100,
                    PricePerHour = 3500,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                },
                new ConferenceRoom
                {
                    Id = Guid.NewGuid(),
                    Name = "C",
                    Capacity = 30,
                    PricePerHour = 1500,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                }
            );
        }
    }
}
