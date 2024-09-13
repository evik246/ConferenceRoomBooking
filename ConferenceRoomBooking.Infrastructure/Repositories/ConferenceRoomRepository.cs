using ConferenceRoomBooking.Application.Contracts;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConferenceRoomBooking.Infrastructure.Repositories
{
    public class ConferenceRoomRepository : GenericRepository<ConferenceRoom, ConferenceRoomFilterDto>, IConferenceRoomRepository
    {
        public ConferenceRoomRepository(ConferenceRoomBookingDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Result<ICollection<ConferenceRoom>>> GetAsync(ConferenceRoomFilterDto filter)
        {
            try
            {
                var query = _dbContext.Set<ConferenceRoom>().AsQueryable();

                if (filter.Guids != null && filter.Guids.Any()) 
                {
                    query = query.Where(cr => filter.Guids.Contains(cr.Id));
                }

                if (filter.Names != null && filter.Names.Any()) 
                {
                    query = query.Where(cr => filter.Names.Contains(cr.Name));
                }

                if (filter.ServiceIds != null && filter.ServiceIds.Any()) 
                {
                    query = query.Where(cr => cr.Services.Any(s => filter.ServiceIds.Contains(s.Id)));
                }

                if (filter.Capacity.HasValue)
                {
                    query = query.Where(cr => cr.Capacity >= filter.Capacity.Value);
                }

                query = query.Skip(filter.Skip).Take(filter.PageSize);

                var conferenceRooms = await query.ToListAsync();

                return new Result<ICollection<ConferenceRoom>>(conferenceRooms);
            }
            catch (Exception ex) 
            {
                return new Result<ICollection<ConferenceRoom>>(ex);
            }
        }

        public async Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilterDto filter)
        {
            try
            {
                var query = _dbContext.Set<ConferenceRoom>().AsQueryable();

                if (filter.Guids != null && filter.Guids.Any())
                {
                    query = query.Where(cr => filter.Guids.Contains(cr.Id));
                }

                if (filter.Names != null && filter.Names.Any())
                {
                    query = query.Where(cr => filter.Names.Contains(cr.Name));
                }

                if (filter.ServiceIds != null && filter.ServiceIds.Any())
                {
                    query = query.Where(cr => cr.Services.Any(s => filter.ServiceIds.Contains(s.Id)));
                }

                if (filter.Capacity.HasValue)
                {
                    query = query.Where(cr => cr.Capacity >= filter.Capacity.Value);
                }

                if (filter.Date.HasValue || filter.StartTime.HasValue || filter.EndTime.HasValue)
                {
                    var bookingsQuery = _dbContext.Set<Booking>().AsQueryable();

                    if (filter.Date.HasValue)
                    {
                        bookingsQuery = bookingsQuery.Where(b =>
                            filter.Date.Value.ToDateTime(TimeOnly.MinValue) == b.DateTime);
                    }

                    if (filter.StartTime.HasValue)
                    {
                        var startOfDay = filter.Date.HasValue ? filter.Date.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue;
                        bookingsQuery = bookingsQuery.Where(b => b.DateTime >= startOfDay && b.DateTime.TimeOfDay < filter.StartTime.Value.ToTimeSpan());
                    }

                    if (filter.EndTime.HasValue)
                    {
                        var endOfDay = filter.Date.HasValue ? filter.Date.Value.ToDateTime(TimeOnly.MaxValue) : DateTime.MaxValue;
                        bookingsQuery = bookingsQuery.Where(b => b.DateTime <= endOfDay && b.DateTime.TimeOfDay.Add(TimeSpan.FromHours(b.HourAmount)) > filter.EndTime.Value.ToTimeSpan());
                    }

                    var bookedRoomIds = await bookingsQuery
                        .Select(b => b.ConferenceRoomId)
                        .Distinct()
                        .ToListAsync();

                    query = query.Where(cr => !bookedRoomIds.Contains(cr.Id));
                }

                query = query.Skip(filter.Skip).Take(filter.PageSize);

                var conferenceRooms = await query.ToListAsync();

                return new Result<ICollection<ConferenceRoom>>(conferenceRooms);
            }
            catch (Exception ex) 
            {
                return new Result<ICollection<ConferenceRoom>>(ex);
            }
        }
    }
}
