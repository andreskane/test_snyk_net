using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class CountryTests
    {
        [TestMethod()]
        public void CountryTest()
        {
            var result = new Country();
            result.Should().NotBeNull();
            result.Id.Should().Be(0);
            result.Name.Should().BeNull();
            result.StructureModels.Should().BeEmpty();
            result.Code.Should().BeNull();
        }
    }
}