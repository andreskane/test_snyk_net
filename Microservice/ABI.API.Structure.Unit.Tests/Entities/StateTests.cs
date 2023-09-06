using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class StateTests
    {
        [TestMethod()]
        public void StateTest()
        {
            var result = new State();
            result.StateGroupId = 1;
            result.StateGroup = null;
            result.MotiveStates = null;

            result.Should().NotBeNull();
            result.StateGroupId.Should().Be(1);
            result.StateGroup.Should().BeNull();
            result.MotiveStates.Should().BeNull();
            result.ChangeStatus.Should().BeEmpty();
        }
    }
}