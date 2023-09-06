using ABI.API.Structure.Application.Exceptions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Exceptions
{
    [TestClass()]
    public class ConfirmExceptionTests
    {
        [TestMethod()]
        public void ConfirmExceptionTest()
        {
            var result = new ConfirmException();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ConfirmExceptionMessageTest()
        {
            var result = new ConfirmException("TEST");
            result.Should().NotBeNull();
        }
    }
}
