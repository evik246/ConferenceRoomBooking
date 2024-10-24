using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Models;
using ConferenceRoomBooking.Bll.Common.Responces;
using ConferenceRoomBooking.Dal.Db.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Dal.Db.Repositories
{
    public class RepositoryBase<TModel, TEntity, F> : IRepositoryBase<TModel, F>
        where TModel : class
        where TEntity : class
        where F : BaseFilter
    {
        protected readonly ConferenceRoomBookingDbContext _dbContext;
        protected readonly IEntityMapper<TModel, TEntity> _mapper;

        public RepositoryBase(ConferenceRoomBookingDbContext dbContext, IEntityMapper<TModel, TEntity> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<Result<TModel>> AddAsync(TModel model)
        {
            try
            {
                var entity = _mapper.MapToEntity(model);
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return new Result<TModel>(model);
            }
            catch (Exception ex) 
            {
                return new Result<TModel>(ex);
            }
        }

        public virtual async Task<Result> DeleteAsync(TModel model)
        {
            try
            {
                var entity = _mapper.MapToEntity(model);
                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public virtual async Task<Result<ICollection<TModel>>> GetAsync(F filter)
        {
            try
            {
                var entities = await _dbContext.Set<TEntity>()
                    .Skip(filter.Skip)
                    .Take(filter.PageSize)
                    .ToListAsync();

                var models = entities.Select(_mapper.MapToModel).ToList();

                return new Result<ICollection<TModel>>(models);
            }
            catch (Exception ex)
            {
                return new Result<ICollection<TModel>>(ex);
            }
        }

        public virtual async Task<Result<TModel>> UpdateAsync(TModel model)
        {
            try
            {
                var entity = _mapper.MapToEntity(model);
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new Result<TModel>(model);
            }
            catch (Exception ex)
            {
                return new Result<TModel>(ex);
            }
        }
    }
}
