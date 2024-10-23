using ConferenceRoomBooking.Bll.Common.Models;
using ConferenceRoomBooking.Bll.Common.Responces;

namespace ConferenceRoomBooking.Bll.Common.Contracts.Repositories
{
    public interface IRepositoryBase<T, F>
        where T : class
        where F : BaseFilter
    {
        Task<Result<ICollection<T>>> GetAsync(F filter);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
    }
}
