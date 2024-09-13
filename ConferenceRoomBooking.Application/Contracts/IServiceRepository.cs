using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts
{
    public interface IServiceRepository : IGenericRepository<Service, ServiceFilterDto>
    {
    }
}
