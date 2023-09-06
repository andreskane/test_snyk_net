using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class TypeGroupTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new TypeGroup();
            result.Should().NotBeNull();
            result.Types.Should().BeEmpty();
        }
    }
}