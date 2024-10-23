using ConferenceRoomBooking.Dal.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomBooking.Dal.Db.Configurations.Entities
{
    public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {
        }
    }
}
