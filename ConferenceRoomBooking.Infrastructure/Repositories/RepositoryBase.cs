using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs;
using ConferenceRoomBooking.Application.Responces;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Infrastructure.Repositories
{
    public class RepositoryBase<T, F> : IRepositoryBase<T, F> 
        where T : class 
        where F : BaseFilterDto
    {
        protected readonly ConferenceRoomBookingDbContext _dbContext;

        public RepositoryBase(ConferenceRoomBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return new Result<T>(entity);
            }
            catch (Exception ex) 
            {
                return new Result<T>(ex);
            }
        }

        public virtual async Task<Result> DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public virtual async Task<Result<ICollection<T>>> GetAsync(F filter)
        {
            try
            {
                var entities = await _dbContext.Set<T>()
                    .Skip(filter.Skip)
                    .Take(filter.PageSize)
                    .ToListAsync();

                return new Result<ICollection<T>>(entities);
            }
            catch (Exception ex)
            {
                return new Result<ICollection<T>>(ex);
            }
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new Result<T>(entity);
            }
            catch (Exception ex)
            {
                return new Result<T>(ex);
            }
        }
    }
}
