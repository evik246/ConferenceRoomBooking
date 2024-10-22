namespace ConferenceRoomBooking.Bll.DTOs.Errors
{
    public class ValidationErrorDto
    {
        public string PropertyName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
