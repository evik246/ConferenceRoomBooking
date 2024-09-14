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
                var query = _dbContext.Set<Booking>().AsQueryable();
                query = query.Include(b => b.ConferenceRoom);

                if (filter.Guids != null && filter.Guids.Any())
                {
                    query = query.Where(b => filter.Guids.Contains(b.Id));
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
