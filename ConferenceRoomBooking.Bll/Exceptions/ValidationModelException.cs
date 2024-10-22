using ConferenceRoomBooking.Bll.DTOs.Errors;
using FluentValidation.Results;

namespace ConferenceRoomBooking.Bll.Exceptions
{
    public class ValidationModelException : ApplicationException
    {
        public List<ValidationErrorDto> Errors { get; set; } = new();

        public ValidationModelException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors) 
            {
                Errors.Add(new ValidationErrorDto { 
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                });
            }
        }
    }
}
