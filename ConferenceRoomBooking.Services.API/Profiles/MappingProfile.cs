using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Services.API.DTOs.BookingRequest;
using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Services.API.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Bll.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BookingFilterDto, BookingFilter>().ReverseMap();
            CreateMap<CreateBookingRequestDto, Booking>().ReverseMap();

            CreateMap<ConferenceRoomFilterDto, ConferenceRoomFilter>().ReverseMap();
            CreateMap<CreateConferenceRoomRequestDto, ConferenceRoom>().ReverseMap();
            CreateMap<UpdateConferenceRoomRequestDto, ConferenceRoom>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ServiceFilterDto, ServiceFilter>().ReverseMap();


            /*CreateMap<ConferenceRoom, ConferenceRoomDto>().ReverseMap();
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
                .ForMember(dest => dest.Guids, opt => opt.MapFrom(src => src.ServiceIds));*/
        }
    }
}
