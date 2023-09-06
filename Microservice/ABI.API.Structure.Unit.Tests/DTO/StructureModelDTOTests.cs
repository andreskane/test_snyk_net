using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureModelDTOTests
    {
        [TestMethod()]
        public void StructureModelDTOTest()
        {
            var result = new StructureModelDTO
            {
                Id = null,
                Name = "TEST",
                ShortName = "TEST",
                Description = "TEST",
                Active = true,
                CanBeExportedToTruck = true,
                Erasable = null,
                InUse = null,
                StructureModelSourceId = null,
                CountryId = 1,
                Country = null
            };

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.ShortName.Should().Be("TEST");
            result.Description.Should().Be("TEST");
            result.Active.Should().BeTrue();
            result.CanBeExportedToTruck.Should().BeTrue();
            result.Erasable.Should().BeNull();
            result.InUse.Should().BeNull();
            result.StructureModelSourceId.Should().BeNull();
            result.CountryId.Should().Be(1);
            result.Country.Should().BeNull();

        }
    }
}