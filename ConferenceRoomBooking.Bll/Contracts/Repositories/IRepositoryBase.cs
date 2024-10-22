using ConferenceRoomBooking.Bll.DTOs;
using ConferenceRoomBooking.Bll.Responces;

namespace ConferenceRoomBooking.Bll.Contracts.Repositories
{
    public interface IRepositoryBase<T, F>
        where T : class
        where F : BaseFilterDto
    {
        Task<Result<ICollection<T>>> GetAsync(F filter);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
    }
}
