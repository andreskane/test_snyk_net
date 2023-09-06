using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Commands.Entities.Tests
{
    [TestClass()]
    public class ValidateErrorTests
    {
        [TestMethod()]
        public void ValidateErrorTest()
        {
            var result = new ValidateError();
            result.NodeId = 1;
            result.NodeCode = "TEST";
            result.NodeName = "TEST";
            result.NodeLevelId = 1;
            result.NodeLevelName = "TEST";
            result.Error = null;
            result.TypeError = null;
            result.Priority = 0;

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1);
            result.NodeCode.Should().Be("TEST");
            result.NodeName.Should().Be("TEST");
            result.NodeLevelId.Should().Be(1);
            result.NodeLevelName.Should().Be("TEST");
            result.Error.Should().BeNull();
            result.TypeError.Should().BeNull();
            result.Priority.Should().Be(0);
        }
    }
}