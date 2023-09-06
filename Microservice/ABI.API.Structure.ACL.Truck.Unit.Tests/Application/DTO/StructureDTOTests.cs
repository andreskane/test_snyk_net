using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class StructureDTOTests
    {
        [TestMethod()]
        public void StructureDTOTest()
        {
            var result = new StructureDTO
            {
                Id = 1,
                Name = "TEST",
                StructureModelId = 1,
                Validity = null,
                Erasable = null,
                FirstNodeName = "TEST",
                StructureModel = null,
                ThereAreChangesWithoutSaving = true,
                ThereAreScheduledChanges = true
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.StructureModelId.Should().Be(1);
            result.Validity.Should().BeNull();
            result.Erasable.Should().BeNull();
            result.FirstNodeName.Should().Be("TEST");
            result.StructureModel.Should().BeNull();
            result.ThereAreChangesWithoutSaving.Should().BeTrue();
            result.ThereAreScheduledChanges.Should().BeTrue();
        }
    }
}