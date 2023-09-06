using ABI.API.Structure.Application.Commands.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures.Tests
{
    [TestClass()]
    public class ValidateValidityCommandTests
    {
        [TestMethod()]
        public void ValidateValidityCommandTest()
        {
            var result = new ValidateValidityCommand(1, DateTimeOffset.UtcNow.Date);

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.Validity.Should().Be(DateTimeOffset.UtcNow.Date);
        }

        [TestMethod()]
        public async Task ValidateValidityCommandHandlerTest()
        {
            var command = new ValidateValidityCommand(1, DateTimeOffset.UtcNow.Date);
            var handler = new ValidateValidityCommandHandler();
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType<ValidateDateStructure>();
        }

        [TestMethod()]
        public void ValidateValidityCommandParameterlessTest()
        {
            var result = new ValidateValidityCommand();

            result.Should().NotBeNull();
        }
    }
}