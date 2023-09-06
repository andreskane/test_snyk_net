using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class NodeEditSameDateExceptionTests
    {
        [TestMethod()]
        public void NodeEditSameDateExceptionTest()
        {
            var result = new NodeEditSameDateException();
            result.Should().NotBeNull();
        }
    }
}