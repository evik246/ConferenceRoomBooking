using ConferenceRoomBooking.Dal.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomBooking.Dal.Db.Configurations.Entities
{
    public class ServiceConfiguration : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceEntity> builder)
        {
            builder.HasData
            (
                new ServiceEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Проєктор",
                    Price = 500,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                },
                new ServiceEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Wi-Fi",
                    Price = 300,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                },
                new ServiceEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Звук",
                    Price = 700,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                }
            );
        }
    }
}
