using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeEmployeeNoResponsableTerritoriesExceptionTests
    {
        [TestMethod()]
        public void NodeEmployeeNoResponsableTerritoriesExceptionTest()
        {
            var result = new NodeEmployeeNoResponsableTerritoriesException();
            result.Should().NotBeNull();
        }
    }
}