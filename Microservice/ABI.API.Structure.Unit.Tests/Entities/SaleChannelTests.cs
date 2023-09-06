using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class SaleChannelTests
    {
        [TestMethod()]
        public void SaleChannelTest()
        {
            var result = new SaleChannel();
            result.Should().NotBeNull();
            result.Active.Should().BeFalse();
            result.Description.Should().BeNull();
            result.ShortName.Should().BeNull();
            result.StructureNodoDefinitions.Should().BeEmpty();
        }
    }
}