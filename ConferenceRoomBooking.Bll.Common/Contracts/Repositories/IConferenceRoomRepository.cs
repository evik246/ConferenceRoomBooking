using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Common.Responces;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IConferenceRoomRepository : IRepositoryBase<ConferenceRoom, ConferenceRoomFilterDto>
    {
        Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilterDto filter);
    }
}
