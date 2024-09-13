namespace ConferenceRoomBooking.Domain.Entities.Common
{
    public abstract class BaseDomainEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
