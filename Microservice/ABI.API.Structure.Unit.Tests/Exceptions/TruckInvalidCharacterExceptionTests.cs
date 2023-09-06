using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class TruckInvalidCharacterExceptionTests
    {
        [TestMethod()]
        public void TruckInvalidCharacterExceptionTest()
        {
            var result = new TruckInvalidCharacterException("TEST", 'T');
            result.Should().NotBeNull();
            result.Property.Should().Be("TEST");
            result.Character.Should().Be("T");
        }
    }
}