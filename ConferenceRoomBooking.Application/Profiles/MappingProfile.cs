﻿using AutoMapper;
using ConferenceRoomBooking.Application.DTOs.BookingRequest;
using ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest;
using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.Profiles
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
