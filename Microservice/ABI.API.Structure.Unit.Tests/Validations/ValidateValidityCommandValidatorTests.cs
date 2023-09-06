using ABI.API.Structure.Application.Commands.Structures;
using ABI.Framework.MS.Domain.Exceptions;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class ValidateValidityCommandValidatorTests
    {
        [TestMethod()]
        public async Task ValidateValidityCommandValidatorTestAsync()
        {
            var mockLogger = new Mock<ILogger<EditStructureCommandValidator>>();
            var command = new ValidateValidityCommand(1, DateTimeOffset.UtcNow.AddDays(1).ToOffset(TimeSpan.FromHours(-3)));
            var validator = new ValidateValidityCommandValidator(mockLogger.Object);
            var result = await validator.TestValidateAsync(command, default);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public async Task ValidateValidityCommandValidatorThrowsTestAsync()
        {
            var mockLogger = new Mock<ILogger<EditStructureCommandValidator>>();
            var command = new ValidateValidityCommand(1, DateTimeOffset.UtcNow.AddDays(-1).Date);
            var validator = new ValidateValidityCommandValidator(mockLogger.Object);

            await validator
                .Invoking(i => i.TestValidateAsync(command, default))
                .Should().ThrowAsync<DateGreaterThanTodayException>();
        }
    }
}