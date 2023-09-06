using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class LevelTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new Level();
            result.Should().NotBeNull();
            result.Active.Should().BeFalse();
            result.ParentStructureModelsDefinitions.Should().BeEmpty();
            result.StructureModelsDefinitions.Should().BeEmpty();
            result.StructureNodos.Should().BeEmpty();
            result.ShortName.Should().BeNull();
            result.Description.Should().BeNull();
        }
    }
}