using ConferenceRoomBooking.Bll.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Contracts.Repositories
{
    public interface IConferenceRoomRepository : IRepositoryBase<ConferenceRoom, ConferenceRoomFilterDto>
    {
        Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilterDto filter);
    }
}
