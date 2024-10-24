using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Dal.Db.Entities;

namespace ConferenceRoomBooking.Dal.Db.Mappers
{
    public class ServiceEntityMapper : IEntityMapper<Service, ServiceEntity>
    {
        private readonly IMapper _mapper;

        public ServiceEntityMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServiceEntity MapToEntity(Service model)
        {
            return _mapper.Map<ServiceEntity>(model);
        }

        public Service MapToModel(ServiceEntity entity)
        {
            return _mapper.Map<Service>(entity);
        }
    }
}
