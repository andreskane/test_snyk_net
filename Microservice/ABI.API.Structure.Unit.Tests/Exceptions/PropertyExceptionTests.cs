using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class PropertyExceptionTests
    {
        [TestMethod()]
        public void PropertyExceptionTest()
        {
            var result = new PropertyException();

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void PropertyExceptionOverloadTest()
        {
            var result = new PropertyException("TEST", new Exception());

            result.Should().NotBeNull();
            result.Message.Should().Be("TEST");
            result.InnerException.Should().NotBeNull();
        }
    }
}