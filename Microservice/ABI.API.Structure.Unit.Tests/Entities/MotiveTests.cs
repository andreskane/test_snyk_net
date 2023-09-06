using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class MotiveTests
    {
        [TestMethod()]
        public void MotiveTest()
        {
            var result = new Motive();
            result.MotiveStates = null;

            result.Should().NotBeNull();
            result.MotiveStates.Should().BeNull();
        }
    }
}