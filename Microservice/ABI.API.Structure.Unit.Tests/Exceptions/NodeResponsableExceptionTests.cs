using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeResponsableExceptionTests
    {
        [TestMethod()]
        public void NodeResponsableExceptionTest()
        {
            var result = new NodeResponsableException();
            result.Should().NotBeNull();
        }
    }
}