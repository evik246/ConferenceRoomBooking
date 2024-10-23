using ConferenceRoomBooking.Bll.Common.Models.ConferenceRoomModels;
using FluentValidation;

namespace ConferenceRoomBooking.Services.API.DTOs.ConferenceRoomRequest.Validators
{
    public class ConferenceRoomFilterValidator : AbstractValidator<ConferenceRoomFilter>
    {
        public ConferenceRoomFilterValidator() 
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

            RuleFor(p => p.Date)
                .Must(date => !date.HasValue || date.Value.ToDateTime(TimeOnly.MinValue) >= DateTime.Today)
                    .WithMessage("{PropertyName} must not be in the past");
        }
    }
}
