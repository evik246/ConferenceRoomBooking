using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.Responces;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Contracts
{
    public interface IConferenceRoomRepository : IGenericRepository<ConferenceRoom, ConferenceRoomFilterDto>
    {
        Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilterDto filter);
    }
}
