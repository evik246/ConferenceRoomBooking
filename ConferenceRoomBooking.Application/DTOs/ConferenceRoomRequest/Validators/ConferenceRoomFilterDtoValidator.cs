using FluentValidation;

namespace ConferenceRoomBooking.Application.DTOs.ConferenceRoomRequest.Validators
{
    public class ConferenceRoomFilterDtoValidator : AbstractValidator<ConferenceRoomFilterDto>
    {
        public ConferenceRoomFilterDtoValidator() 
        {
            RuleFor(p => p.PageNumber)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(p => p.PageSize)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(p => p.Capacity)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }
}
