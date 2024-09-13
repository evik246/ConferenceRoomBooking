namespace ConferenceRoomBooking.Application.DTOs
{
    public abstract class BaseFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public int Skip
        {
            get { return PageSize * (PageNumber - 1); }
        }
    }
}
