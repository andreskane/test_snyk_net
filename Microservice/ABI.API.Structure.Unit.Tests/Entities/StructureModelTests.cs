using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class StructureModelTests
    {
        [TestMethod()]
        public void StructureModelTest()
        {
            var result = new StructureModel();
            result.Should().NotBeNull();
            result.Active.Should().BeNull();
            result.CanBeExportedToTruck.Should().BeTrue();
            result.Description.Should().BeNull();
            result.ShortName.Should().BeNull();
            result.StructureModelsDefinitions.Should().BeEmpty();
            result.Structures.Should().BeEmpty();
            result.Code.Should().BeNull();
            result.CountryId.Should().Be(0);
            result.Country.Should().BeNull();
        }
    }
}