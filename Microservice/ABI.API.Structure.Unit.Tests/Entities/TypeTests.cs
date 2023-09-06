using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class TypeTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new Type();
            result.Code = "TEST";
            result.TypeGroup = null;

            result.Should().NotBeNull();
            result.Code.Should().Be("TEST");
            result.StructureArista.Should().BeEmpty();
            result.ChangeTracking.Should().BeEmpty();
            result.TypeGroup.Should().BeNull();
            result.TypeGroupId.Should().Be(0);
        }
    }
}