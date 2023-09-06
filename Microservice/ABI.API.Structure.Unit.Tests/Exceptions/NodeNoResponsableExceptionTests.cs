using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeNoResponsableExceptionTests
    {
        [TestMethod()]
        public void NodeNoResponsableExceptionTest()
        {
            var result = new NodeNoResponsableException();
            result.Should().NotBeNull();
        }
    }
}