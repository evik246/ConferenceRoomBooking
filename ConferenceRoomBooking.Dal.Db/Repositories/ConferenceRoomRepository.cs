﻿using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Responces;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Dal.Db.Entities;
using ConferenceRoomBooking.Dal.Db.Mappers;

namespace ConferenceRoomBooking.Dal.Db.Repositories
{
    public class ConferenceRoomRepository : RepositoryBase<ConferenceRoom, ConferenceRoomEntity, ConferenceRoomFilter>, IConferenceRoomRepository
    {
        public ConferenceRoomRepository(ConferenceRoomBookingDbContext dbContext, IEntityMapper<ConferenceRoom, ConferenceRoomEntity> mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<ICollection<ConferenceRoom>>> GetAsync(ConferenceRoomFilter filter)
        {
            try
            {
                var query = _dbContext.Set<ConferenceRoomEntity>()
                    .Include(b => b.Services)
                    .AsQueryable();

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

                var conferenceRoomEntities = await query.ToListAsync();
                var conferenceRoomModels = conferenceRoomEntities.Select(_mapper.MapToModel).ToList();

                return new Result<ICollection<ConferenceRoom>>(conferenceRoomModels);
            }
            catch (Exception ex) 
            {
                return new Result<ICollection<ConferenceRoom>>(ex);
            }
        }

        public async Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilter filter)
        {
            try
            {
                var query = _dbContext.Set<ConferenceRoomEntity>()
                    .Include(b => b.Services)
                    .AsQueryable();

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
                    var bookingsQuery = _dbContext.Set<BookingEntity>().AsQueryable();

                    DateTime startOfDay = filter.Date.HasValue ? filter.Date.Value.ToDateTime(TimeOnly.MinValue) : DateTime.MinValue;
                    DateTime endOfDay = filter.Date.HasValue ? filter.Date.Value.ToDateTime(TimeOnly.MaxValue) : DateTime.MaxValue;

                    if (filter.Date.HasValue)
                    {
                        bookingsQuery = bookingsQuery.Where(b => b.DateTime.Date == startOfDay.Date);
                    }

                    if (filter.StartTime.HasValue || filter.EndTime.HasValue)
                    {
                        if (filter.StartTime.HasValue)
                        {
                            var startTime = startOfDay.Add(filter.StartTime.Value.ToTimeSpan());
                            bookingsQuery = bookingsQuery.Where(b => b.DateTime.AddHours(b.HourAmount) > startTime);
                        }

                        if (filter.EndTime.HasValue)
                        {
                            var endTime = startOfDay.Add(filter.EndTime.Value.ToTimeSpan());
                            bookingsQuery = bookingsQuery.Where(b => b.DateTime < endTime);
                        }
                    }

                    var bookedRoomIds = await bookingsQuery
                        .Select(b => b.ConferenceRoomId)
                        .Distinct()
                        .ToListAsync();

                    query = query.Where(cr => !bookedRoomIds.Contains(cr.Id));
                }

                query = query.Skip(filter.Skip).Take(filter.PageSize);

                var conferenceRoomEntities = await query.ToListAsync();
                var conferenceRoomModels = conferenceRoomEntities.Select(_mapper.MapToModel).ToList();

                return new Result<ICollection<ConferenceRoom>>(conferenceRoomModels);
            }
            catch (Exception ex) 
            {
                return new Result<ICollection<ConferenceRoom>>(ex);
            }
        }
    }
}
