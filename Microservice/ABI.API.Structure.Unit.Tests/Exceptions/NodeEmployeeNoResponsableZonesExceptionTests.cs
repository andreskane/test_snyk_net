using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeEmployeeNoResponsableZonesExceptionTests
    {
        [TestMethod()]
        public void NodeEmployeeNoResponsableZonesExceptionTest()
        {
            var result = new NodeEmployeeNoResponsableZonesException();
            result.Should().NotBeNull();
        }
    }
}