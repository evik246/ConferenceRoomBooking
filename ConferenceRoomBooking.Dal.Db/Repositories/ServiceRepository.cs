using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Responces;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Dal.Db.Entities;
using ConferenceRoomBooking.Dal.Db.Mappers;

namespace ConferenceRoomBooking.Dal.Db.Repositories
{
    public class ServiceRepository : RepositoryBase<Service, ServiceEntity, ServiceFilter>, IServiceRepository
    {
        public ServiceRepository(ConferenceRoomBookingDbContext dbContext, IEntityMapper<Service, ServiceEntity> mapper) : base(dbContext, mapper)
        {
        }

        public async override Task<Result<ICollection<Service>>> GetAsync(ServiceFilter filter)
        {
            try
            {
                var query = _dbContext.Set<ServiceEntity>().AsQueryable();

                if (filter.Guids != null && filter.Guids.Any())
                {
                    query = query.Where(s => filter.Guids.Contains(s.Id));
                }

                query = query.Skip(filter.Skip).Take(filter.PageSize);

                var serviceEntities = await query.ToListAsync();
                var serviceModels = serviceEntities.Select(_mapper.MapToModel).ToList();
                return new Result<ICollection<Service>>(serviceModels);
            }
            catch (Exception ex)
            {
                return new Result<ICollection<Service>>(ex);
            }
        }
    }
}
