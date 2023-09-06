using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.Application.Commands.Structures;
using ABI.Framework.MS.Domain.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateValidityCommandValidator : AbstractValidator<ValidateValidityCommand>
    {
        public ValidateValidityCommandValidator(ILogger<EditStructureCommandValidator> logger)
        {
            RuleFor(command => command.Validity).Must(ValidDate)
            .OnAnyFailure(x =>
            {
                throw new DateGreaterThanTodayException();
            });
        }

        private bool ValidDate(DateTimeOffset dateTimeOffset)
        {
            var validityOffset = dateTimeOffset.Offset;
            var today = DateTimeOffset.UtcNow.ToOffset(validityOffset);
            var tomorrow = today.Date.AddDays(1).ToDateOffset(-3);

            var result = dateTimeOffset >= tomorrow;

            return result;
        }
    }

}
