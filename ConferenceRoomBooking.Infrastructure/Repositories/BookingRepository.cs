using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Infrastructure.Repositories
{
    public class BookingRepository : RepositoryBase<Booking, BookingFilterDto>, IBookingRepository
    {
        public BookingRepository(ConferenceRoomBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Result<ICollection<Booking>>> GetAsync(BookingFilterDto filter)
        {
            try
            {
                var query = _dbContext.Set<Booking>()
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

                var bookings = await query.ToListAsync();
                return new Result<ICollection<Booking>>(bookings);
            }
            catch (Exception ex)
            {
                return new Result<ICollection<Booking>>(ex);
            }
        }
    }
}
