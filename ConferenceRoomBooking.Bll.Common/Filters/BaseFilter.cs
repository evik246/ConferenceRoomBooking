using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ConferenceRoomBooking.Bll.Common.Filters
{
    public abstract class BaseFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        [BindNever]
        public int Skip
        {
            get { return PageSize * (PageNumber - 1); }
        }
    }
}
