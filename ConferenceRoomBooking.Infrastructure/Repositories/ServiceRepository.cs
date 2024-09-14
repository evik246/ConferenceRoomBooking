using ConferenceRoomBooking.Application.Contracts.Repositories;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Infrastructure.Repositories
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
