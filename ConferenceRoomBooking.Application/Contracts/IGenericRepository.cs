using ConferenceRoomBooking.Application.DTOs;
using ConferenceRoomBooking.Application.Responces;

namespace ConferenceRoomBooking.Application.Contracts
{
    public interface IGenericRepository<T, F>
        where T : class
        where F : BaseFilterDto
    {
        Task<Result<ICollection<T>>> GetAsync(F filter);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
    }
}
