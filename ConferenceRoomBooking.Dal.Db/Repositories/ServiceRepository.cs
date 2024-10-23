using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Responces;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;

namespace ConferenceRoomBooking.Dal.Db.Repositories
{
    public class ServiceRepository : RepositoryBase<Service, ServiceFilter>, IServiceRepository
    {
        public ServiceRepository(ConferenceRoomBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Result<ICollection<Service>>> GetAsync(ServiceFilter filter)
        {
            try
            {
                var query = _dbContext.Set<Service>().AsQueryable();

                if (filter.Guids != null && filter.Guids.Any())
                {
                    query = query.Where(s => filter.Guids.Contains(s.Id));
                }

                query = query.Skip(filter.Skip).Take(filter.PageSize);

                var services = await query.ToListAsync();
                return new Result<ICollection<Service>>(services);
            }
            catch (Exception ex)
            {
                return new Result<ICollection<Service>>(ex);
            }
        }
    }
}
