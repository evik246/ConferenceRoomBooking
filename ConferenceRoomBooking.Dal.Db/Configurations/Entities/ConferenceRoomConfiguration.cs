using ConferenceRoomBooking.Dal.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomBooking.Dal.Db.Configurations.Entities
{
    public class ConferenceRoomConfiguration : IEntityTypeConfiguration<ConferenceRoomEntity>
    {
        public void Configure(EntityTypeBuilder<ConferenceRoomEntity> builder)
        {
            builder.HasData
            (
                new ConferenceRoomEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "A",
                    Capacity = 50,
                    PricePerHour = 2000,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                },
                new ConferenceRoomEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "B",
                    Capacity = 100,
                    PricePerHour = 3500,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                },
                new ConferenceRoomEntity
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
