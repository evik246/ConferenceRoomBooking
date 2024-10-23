﻿namespace ConferenceRoomBooking.Bll.Common.Filters.ConferenceRoomRequest
{
    public class ConferenceRoomFilter : BaseFilter
    {
        public List<Guid>? Guids { get; set; }
        public List<string>? Names { get; set; }
        public List<Guid>? ServiceIds { get; set; }
        public int? Capacity { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }
}
