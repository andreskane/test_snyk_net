using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Exceptions.Tests
{
    [TestClass()]
    public class StructureCodeExistsExceptionTests
    {
        [TestMethod()]
        public void StructureCodeExistsExceptionTest()
        {
            var result = new StructureCodeExistsException();

            result.Should().NotBeNull();
            result.Message.Should().Be("El código ya existe");
        }
    }
}