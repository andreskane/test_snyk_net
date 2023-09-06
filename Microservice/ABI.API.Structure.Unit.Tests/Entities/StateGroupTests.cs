using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class StateGroupTests
    {
        [TestMethod()]
        public void StateGroupTest()
        {
            var result = new StateGroup();
            result.States = null;

            result.Should().NotBeNull();
            result.States.Should().BeNull();
        }
    }
}