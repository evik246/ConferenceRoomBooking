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

            RuleFor(p => p)
                .Must(IsWithinAllowedTimeRange)
                .WithMessage("Booking must be within the allowed time range from 06:00 to 23:00.");
        }

        private bool IsWithinAllowedTimeRange(CreateBookingRequestDto dto)
        {
            var startTime = dto.DateTime.TimeOfDay;
            var endTime = dto.DateTime.AddHours(dto.HourAmount).TimeOfDay;

            var morningTime = new TimeSpan(6, 0, 0);
            var eveningTime = new TimeSpan(23, 0, 0);

            return startTime >= morningTime && endTime <= eveningTime;
        }
    }
}
