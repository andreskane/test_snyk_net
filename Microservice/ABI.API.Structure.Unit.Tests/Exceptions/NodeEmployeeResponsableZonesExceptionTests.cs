using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeEmployeeResponsableZonesExceptionTests
    {
        [TestMethod()]
        public void NodeEmployeeResponsableZonesExceptionTest()
        {
            var result = new NodeEmployeeResponsableZonesException();
            result.Should().NotBeNull();
        }
    }
}