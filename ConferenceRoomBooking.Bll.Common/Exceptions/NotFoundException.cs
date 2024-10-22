namespace ConferenceRoomBooking.Bll.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name) : base($"{name} was not found") 
        {
        }

        public NotFoundException(string name, string key) : base($"{name} ({key}) was not found")
        {
        }
    }
}
