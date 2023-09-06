using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeEmployeeResponsableTerritoriesExceptionTests
    {
        [TestMethod()]
        public void NodeEmployeeResponsableTerritoriesExceptionTest()
        {
            var result = new NodeEmployeeResponsableTerritoriesException();
            result.Should().NotBeNull();
        }
    }
}