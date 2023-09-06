using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeCodeLengthExceptionTests
    {
        [TestMethod()]
        public void NodeCodeLengthExceptionTest()
        {
            var result = new NodeCodeLengthException(1);
            result.Should().NotBeNull();
            result.MaxLength.Should().Be(1);
        }
    }
}