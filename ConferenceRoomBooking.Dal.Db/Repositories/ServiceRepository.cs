using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Dal.Db.Repositories
{
    public class ServiceRepository : RepositoryBase<Service, ServiceFilterDto>, IServiceRepository
    {
        public ServiceRepository(ConferenceRoomBookingDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<Result<ICollection<Service>>> GetAsync(ServiceFilterDto filter)
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
