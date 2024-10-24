using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Responces;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Dal.Db.Entities;
using ConferenceRoomBooking.Dal.Db.Mappers;

namespace ConferenceRoomBooking.Dal.Db.Repositories
{
    public class BookingRepository : RepositoryBase<Booking, BookingEntity, BookingFilter>, IBookingRepository
    {
        public BookingRepository(ConferenceRoomBookingDbContext dbContext, IEntityMapper<Booking, BookingEntity> mapper) : base(dbContext, mapper)
        {
        }

        public async override Task<Result<ICollection<Booking>>> GetAsync(BookingFilter filter)
        {
            try 
            {
                var query = _dbContext.Set<BookingEntity>()
                    .Include(b => b.ConferenceRoom)
                    .Include(b => b.Services)
                    .AsQueryable();

                if (filter.Guids != null && filter.Guids.Any())
                {
                    query = query.Where(b => filter.Guids.Contains(b.Id));
                }

                if (filter.StartDate.HasValue)
                {
                    query = query.Where(b => b.DateTime >= filter.StartDate.Value);
                }

                if (filter.EndDate.HasValue)
                {
                    query = query.Where(b => b.DateTime <= filter.EndDate.Value);
                }

                query = query.Skip(filter.Skip).Take(filter.PageSize);

                var bookingEntities = await query.ToListAsync();
                var bookingModels = bookingEntities.Select(_mapper.MapToModel).ToList();

                return new Result<ICollection<Booking>>(bookingModels);
            }
            catch (Exception ex)
            {
                return new Result<ICollection<Booking>>(ex);
            }
        }
    }
}
