﻿using ConferenceRoomBooking.Bll.Common.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Common.Contracts.Services;
using ConferenceRoomBooking.Bll.Common.Models.BookingModels;
using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using ConferenceRoomBooking.Bll.Common.Models.ServiceModels;
using ConferenceRoomBooking.Dal.Db.Entities;
using ConferenceRoomBooking.Dal.Db.Mappers;
using ConferenceRoomBooking.Dal.Db.Repositories;
using ConferenceRoomBooking.Dal.Db.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConferenceRoomBooking.Dal.Db
{
    public static class DalDbServicesRegistration
    {
        public static IServiceCollection ConfigureDalDbServices(
            this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<ConferenceRoomBookingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConferenceRoomBookingConnectionString")
            ));

            //services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,,>));
            
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<IEntityMapper<Booking, BookingEntity>, BookingEntityMapper>();
            services.AddScoped<IEntityMapper<ConferenceRoom, ConferenceRoomEntity>, ConferenceRoomEntityMapper>();
            services.AddScoped<IEntityMapper<Service, ServiceEntity>, ServiceEntityMapper>();

            return services;
        }
    }
}
