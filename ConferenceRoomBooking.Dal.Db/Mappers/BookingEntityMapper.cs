using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Dal.Db.Entities;

namespace ConferenceRoomBooking.Dal.Db.Mappers
{
    public class BookingEntityMapper : IEntityMapper<Booking, BookingEntity>
    {
        private readonly IMapper _mapper;

        public BookingEntityMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BookingEntity MapToEntity(Booking model)
        {
            return _mapper.Map<BookingEntity>(model);
        }

        public Booking MapToModel(BookingEntity entity)
        {
            return _mapper.Map<Booking>(entity);
        }
    }
}
