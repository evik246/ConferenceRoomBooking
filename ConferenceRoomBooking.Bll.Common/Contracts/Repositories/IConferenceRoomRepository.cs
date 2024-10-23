using ConferenceRoomBooking.Bll.Common.Responces;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IConferenceRoomRepository : IRepositoryBase<ConferenceRoom, ConferenceRoomFilter>
    {
        Task<Result<ICollection<ConferenceRoom>>> GetAvailableRoomsAsync(ConferenceRoomFilter filter);
    }
}
