namespace ConferenceRoomBooking.Bll.Common.Entities.Common
{
    public abstract class BaseDomainEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
