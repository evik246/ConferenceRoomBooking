﻿using ConferenceRoomBooking.Application.DTOs.ServiceRequest;
using ConferenceRoomBooking.Domain.Entities;

namespace ConferenceRoomBooking.Application.DTOs.BookingRequest
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; } = new();
        public DateTime DateTime { get; set; }
        public int HourAmount { get; set; }
        public ICollection<ServiceDto> Services { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}