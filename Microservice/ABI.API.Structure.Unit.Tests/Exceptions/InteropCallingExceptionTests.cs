using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class InteropCallingExceptionTests
    {
        [TestMethod()]
        public void InteropCallingExceptionTest()
        {
            var result = new InteropCallingException();
            result.Should().NotBeNull();
        }
    }
}