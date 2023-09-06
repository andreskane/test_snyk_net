using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class MotiveStateTests
    {
        [TestMethod()]
        public void MotiveStateTest()
        {
            var result = new MotiveState();
            result.StateId = 1;
            result.MotiveId = 1;
            result.State = null;
            result.Motive = null;
            result.StructureNodoDefinitions = null;
            result.StructureAristas = null;

            result.Should().NotBeNull();
            result.StateId.Should().Be(1);
            result.MotiveId.Should().Be(1);
            result.State.Should().BeNull();
            result.Motive.Should().BeNull();
            result.StructureNodoDefinitions.Should().BeNull();
            result.StructureAristas.Should().BeNull();
        }
    }
}