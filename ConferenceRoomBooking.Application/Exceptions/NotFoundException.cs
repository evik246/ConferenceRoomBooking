namespace ConferenceRoomBooking.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name) : base($"{name} was not found") 
        {
        }

        public NotFoundException(string name, string message) : base($"{name} was not found: {message}")
        {
        }
    }
}
