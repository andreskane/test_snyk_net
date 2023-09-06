using ABI.API.Structure.Application.Commands.Structures;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class EditStructureCommandValidatorTests
    {
        [TestMethod()]
        public async Task EditStructureCommandValidatorTestAsync()
        {
            var mockLogger = new Mock<ILogger<EditStructureCommandValidator>>();
            var model = new EditStructureCommand(1, "TEST", DateTimeOffset.UtcNow.AddDays(1).Date, "TEST");
            var validator = new EditStructureCommandValidator(mockLogger.Object);
            var result = await validator.TestValidateAsync(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}