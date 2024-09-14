using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts.Repositories
{
    public interface IServiceRepository : IRepositoryBase<Service, ServiceFilterDto>
    {
    }
}
