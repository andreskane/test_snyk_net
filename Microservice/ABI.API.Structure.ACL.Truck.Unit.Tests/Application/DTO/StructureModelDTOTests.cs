using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class StructureModelDTOTests
    {
        [TestMethod()]
        public void StructureModelDTOTest()
        {
            var result = new StructureModelDTO
            {
                Id = 1,
                Name = "TEST",
                ShortName = "TEST",
                Description = "TEST",
                Active = null,
                CanBeExportedToTruck = true,
                Code = "TEST",
                CountryId = 1,
                Country = null
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.ShortName.Should().Be("TEST");
            result.Description.Should().Be("TEST");
            result.Active.Should().BeNull();
            result.CanBeExportedToTruck.Should().BeTrue();
            result.Code.Should().Be("TEST");
            result.CountryId.Should().Be(1);
            result.Country.Should().BeNull();
        }
    }
}