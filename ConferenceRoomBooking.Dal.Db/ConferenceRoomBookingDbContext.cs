using ConferenceRoomBooking.Bll.Common.Entities;
using ConferenceRoomBooking.Bll.Common.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Dal.Db
{
    public class ConferenceRoomBookingDbContext : DbContext
    {
        public ConferenceRoomBookingDbContext(
            DbContextOptions<ConferenceRoomBookingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ConferenceRoomBookingDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added) 
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
