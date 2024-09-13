using FluentValidation;

namespace ConferenceRoomBooking.Application.DTOs.BookingRequest.Validators
{
    public class CreateBookingRequestDtoValidator : AbstractValidator<CreateBookingRequestDto>
    {
        public CreateBookingRequestDtoValidator()
        {
            RuleFor(p => p.ConferenceRoomId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();
            
            RuleFor(p => p.DateTime)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .Must(p => p >= DateTime.Now)
                    .WithMessage("{PropertyName} must not be in the past");
            
            RuleFor(p => p.HourAmount)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
