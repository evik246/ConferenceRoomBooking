using ConferenceRoomBooking.Bll.Common.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IServiceRepository : IRepositoryBase<Service, ServiceFilterDto>
    {
    }
}
