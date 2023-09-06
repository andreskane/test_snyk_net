using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class StructureModelDefinitionTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new StructureModelDefinition();
            result.Level = null;
            result.ParentLevel = null;
            result.StructureModel = null;

            result.Should().NotBeNull();
            result.Level.Should().BeNull();
            result.ParentLevel.Should().BeNull();
            result.ParentLevelId.Should().BeNull();
            result.StructureModelId.Should().Be(0);
            result.StructureModel.Should().BeNull();
        }
    }
}