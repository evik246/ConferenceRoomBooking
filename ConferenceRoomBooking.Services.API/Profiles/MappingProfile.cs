using AutoMapper;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Services.API.DTOs.BookingRequest;
using ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Services.API.DTOs.ServiceRequest;

namespace ConferenceRoomBooking.Services.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BookingFilterDto, BookingFilter>().ReverseMap();
            CreateMap<BookingDto, Booking>().ReverseMap();
            CreateMap<CreateBookingRequestDto, Booking>().ReverseMap();

            CreateMap<ConferenceRoomFilterDto, ConferenceRoomFilter>().ReverseMap();
            CreateMap<ConferenceRoomDto, ConferenceRoom>().ReverseMap();
            CreateMap<CreateConferenceRoomRequestDto, ConferenceRoom>().ReverseMap();
            CreateMap<UpdateConferenceRoomRequestDto, ConferenceRoom>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ServiceFilterDto, ServiceFilter>().ReverseMap();
            CreateMap<ServiceDto, Service>().ReverseMap();
        }
    }
}
