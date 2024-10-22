﻿using ConferenceRoomBooking.Bll.Contracts.Repositories;
using ConferenceRoomBooking.Bll.Contracts.Services;
using ConferenceRoomBooking.Dal.Db.Repositories;
using ConferenceRoomBooking.Dal.Db.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceRoomBooking.Dal.Db
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ConferenceRoomBookingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConferenceRoomBookingConnectionString")
            ));

            services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBookingService, BookingService>();

            return services;
        }
    }
}