using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureModelDTOV2Tests
    {
        [TestMethod()]
        public void StructureModelDTOV2Test()
        {
            var result = new StructureModelV2DTO();
            result.Id = null;
            result.Name = "TEST";
            result.ShortName = "TEST";
            result.Description = "TEST";
            result.Active = true;
            result.CanBeExportedToTruck = true;
            result.Erasable = null;
            result.Code = "TEST";
            result.CountryId = 1;
            result.Definitions = new System.Collections.Generic.List<StructureModelDefinitionV2DTO>();

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.Name.Should().Be("TEST");
            result.ShortName.Should().Be("TEST");
            result.Description.Should().Be("TEST");
            result.Active.Should().BeTrue();
            result.CanBeExportedToTruck.Should().BeTrue();
            result.Erasable.Should().BeNull();
            result.Code.Should().Be("TEST");
            result.CountryId.Should().Be(1);
            result.Definitions.Should().BeEmpty();
        }
    }
}