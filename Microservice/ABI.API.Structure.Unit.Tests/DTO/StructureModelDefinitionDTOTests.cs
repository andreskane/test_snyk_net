using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureModelDefinitionDTOTests
    {
        [TestMethod()]
        public void StructureModelDefinitionDTOTest()
        {
            var result = new StructureModelDefinitionDTO();
            result.Id = null;
            result.StructureModelId = 1;
            result.LevelId = 1;
            result.ParentLevelId = null;
            result.IsAttentionModeRequired = true;
            result.IsSaleChannelRequired = true;
            result.Erasable = null;
            result.Level = null;

            result.Should().NotBeNull();
            result.Id.Should().BeNull();
            result.StructureModelId.Should().Be(1);
            result.LevelId.Should().Be(1);
            result.ParentLevelId.Should().BeNull();
            result.IsAttentionModeRequired.Should().BeTrue();
            result.IsSaleChannelRequired.Should().BeTrue();
            result.Erasable.Should().BeNull();
            result.Level.Should().BeNull();
        }
    }
}