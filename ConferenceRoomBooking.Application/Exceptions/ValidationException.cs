﻿using FluentValidation.Results;

namespace ConferenceRoomBooking.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors) 
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
