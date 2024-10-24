using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Dal.Db.Entities;

namespace ConferenceRoomBooking.Dal.Db.Mappers
{
    public class ConferenceRoomEntityMapper : IEntityMapper<ConferenceRoom, ConferenceRoomEntity>
    {
        private readonly IMapper _mapper;

        public ConferenceRoomEntityMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ConferenceRoomEntity MapToEntity(ConferenceRoom model)
        {
            return _mapper.Map<ConferenceRoomEntity>(model);
        }

        public ConferenceRoom MapToModel(ConferenceRoomEntity entity)
        {
            return _mapper.Map<ConferenceRoom>(entity);
        }
    }
}
