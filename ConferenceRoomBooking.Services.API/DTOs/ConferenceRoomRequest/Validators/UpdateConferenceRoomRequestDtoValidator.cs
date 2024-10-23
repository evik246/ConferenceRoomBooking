using FluentValidation;

namespace ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest.Validators
{
    public class UpdateConferenceRoomRequestDtoValidator : AbstractValidator<UpdateConferenceRoomRequestDto>
    {
        public UpdateConferenceRoomRequestDtoValidator() 
        {
            RuleFor(p => p.Name)
                .MaximumLength(100)
                    .WithMessage("{PropertyName} must be less than or equal 100 characters");

            RuleFor(p => p.Capacity)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(p => p.PricePerHour)
                .InclusiveBetween(0.01m, 999999.99m)
                    .WithMessage("{PropertyName} must be between 0.01 and 999999.99");
        }
    }
}
