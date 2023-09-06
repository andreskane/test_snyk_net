using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureDTOV2Tests
    {
        [TestMethod()]
        public void StructureDTOV2Test()
        {
            var result = new StructureV2DTO();
            result.Id = 1;
            result.Name = "TEST";
            result.StructureModelId = 1;
            result.ValidityFrom = null;
            result.Erasable = null;
            result.FirstNodeName = "TEST";
            result.ThereAreChangesWithoutSaving = true;
            result.ThereAreScheduledChanges = true;
            result.Processing = true;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.StructureModelId.Should().Be(1);
            result.ValidityFrom.Should().BeNull();
            result.Erasable.Should().BeNull();
            result.FirstNodeName.Should().Be("TEST");
            result.ThereAreChangesWithoutSaving.Should().BeTrue();
            result.ThereAreScheduledChanges.Should().BeTrue();
            result.Processing.Should().BeTrue();
        }
    }
}