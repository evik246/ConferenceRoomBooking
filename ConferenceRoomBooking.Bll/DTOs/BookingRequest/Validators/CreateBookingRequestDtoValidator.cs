using FluentValidation;

namespace ConferenceRoomBooking.Bll.DTOs.BookingRequest.Validators
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
                .Must(dto => IsWithinAllowedTimeRange(dto, new TimeSpan(6, 0, 0), new TimeSpan(23, 0, 0)))
                .WithMessage("Booking must be within the allowed time range from 06:00 to 23:00.");
        }

        private bool IsWithinAllowedTimeRange(CreateBookingRequestDto dto, TimeSpan allowedStartTime, TimeSpan allowedEndTime)
        {
            // Get the start and end times of the booking
            var startTime = dto.DateTime.TimeOfDay;
            var endTime = dto.DateTime.AddHours(dto.HourAmount).TimeOfDay;
            var bookingDate = dto.DateTime.Date;
            var endDate = dto.DateTime.AddHours(dto.HourAmount).Date;

            // Check for bookings that span multiple days
            if (bookingDate != endDate)
            {
                // Create DateTime objects for allowed start and end times on each relevant day
                var firstDayEnd = new DateTime(bookingDate.Year, bookingDate.Month, bookingDate.Day, allowedEndTime.Hours, allowedEndTime.Minutes, allowedEndTime.Seconds);
                var lastDayStart = new DateTime(endDate.Year, endDate.Month, endDate.Day, allowedStartTime.Hours, allowedStartTime.Minutes, allowedStartTime.Seconds);

                // Check if the booking starts before the allowed start time or ends after the allowed end time
                if (startTime < allowedStartTime || endTime > allowedEndTime)
                {
                    // If the booking ends before or exactly at the allowed end time on the first day
                    if (endTime <= allowedEndTime && firstDayEnd.TimeOfDay >= endTime)
                    {
                        return true;
                    }
                    // If the booking starts after or exactly at the allowed start time on the last day
                    if (startTime >= allowedStartTime && endTime > allowedEndTime)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }

            // Check that the booking is within the allowed time range on the same day
            return startTime >= allowedStartTime && endTime <= allowedEndTime;
        }

    }
}
