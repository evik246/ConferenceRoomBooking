using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Dal.Db.Entities;

namespace ConferenceRoomBooking.Dal.Db.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Booking, BookingEntity>().ReverseMap();
            CreateMap<ConferenceRoom, ConferenceRoomEntity>().ReverseMap();
            CreateMap<Service, ServiceEntity>().ReverseMap();
        }
    }
}
