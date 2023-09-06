using ABI.API.Structure.Application.Commands.Structures;
using ABI.Framework.MS.Domain.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;

namespace ABI.API.Structure.Application.Validations
{

    public class EditStructureCommandValidator : AbstractValidator<EditStructureCommand>
    {
        public EditStructureCommandValidator(ILogger<EditStructureCommandValidator> logger)
        {

            RuleFor(command => command.ValidityFrom).Must(ValidDate)
            .OnAnyFailure(x =>
            {
                throw new DateGreaterThanTodayException();
            });

        }

        private bool ValidDate(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset.HasValue)
            {
                var today = DateTimeOffset.UtcNow.Date;
                var utc = dateTimeOffset.Value.ToUniversalTime();

                return utc >= today.AddDays(1);
            }

            return true;
        }
    }

}
