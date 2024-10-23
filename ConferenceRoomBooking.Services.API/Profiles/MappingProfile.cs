using AutoMapper;
using ConferenceRoomBooking.Bll.Common.DTOs.BookingRequest;
using ConferenceRoomBooking.Bll.Common.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Bll.Common.DTOs.ServiceRequest;
using ConferenceRoomBooking.Bll.Common.Entities;

namespace ConferenceRoomBooking.Bll.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ConferenceRoom, ConferenceRoomDto>().ReverseMap();
            CreateMap<ConferenceRoom, CreateConferenceRoomRequestDto>().ReverseMap();
            CreateMap<UpdateConferenceRoomRequestDto, ConferenceRoom>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Service, ServiceDto>().ReverseMap();

            CreateMap<Booking, BookingDto>();
            CreateMap<Booking, CreateBookingRequestDto>().ReverseMap();

            CreateMap<CreateConferenceRoomRequestDto, ServiceFilterDto>()
                .ForMember(dest => dest.Guids, opt => opt.MapFrom(src => src.ServiceIds));
            CreateMap<UpdateConferenceRoomRequestDto, ServiceFilterDto>()
                .ForMember(dest => dest.Guids, opt => opt.MapFrom(src => src.ServiceIds));
            CreateMap<CreateBookingRequestDto, ServiceFilterDto>()
                .ForMember(dest => dest.Guids, opt => opt.MapFrom(src => src.ServiceIds));
        }
    }
}
