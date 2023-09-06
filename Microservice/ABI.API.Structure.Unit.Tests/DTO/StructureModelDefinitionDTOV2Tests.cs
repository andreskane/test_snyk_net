using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureModelDefinitionDTOV2Tests
    {
        [TestMethod()]
        public void StructureModelDefinitionDTOV2Test()
        {
            var result = new StructureModelDefinitionV2DTO();
            result.LevelId = 1;
            result.ParentLevelId = null;
            result.IsAttentionModeRequired = true;
            result.IsSaleChannelRequired = true;

            result.Should().NotBeNull();
            result.LevelId.Should().Be(1);
            result.ParentLevelId.Should().BeNull();
            result.IsAttentionModeRequired.Should().BeTrue();
            result.IsSaleChannelRequired.Should().BeTrue();
        }
    }
}