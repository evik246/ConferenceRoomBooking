using ConferenceRoomBooking.Bll.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Contracts.Repositories
{
    public interface IServiceRepository : IRepositoryBase<Service, ServiceFilterDto>
    {
    }
}
