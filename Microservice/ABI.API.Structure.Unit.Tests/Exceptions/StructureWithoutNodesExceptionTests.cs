using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class StructureWithoutNodesExceptionTests
    {
        [TestMethod()]
        public void StructureWithoutNodesExceptionTest()
        {
            var result = new StructureWithoutNodesException();
            result.Should().NotBeNull();
        }
    }
}