using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts.Repositories
{
    public interface IConferenceRoomRepository : IRepositoryBase<ConferenceRoom, ConferenceRoomFilterDto>
    {
        Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilterDto filter);
    }
}
