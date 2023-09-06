using ABI.API.Structure.Application.DTO;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO
{
    [TestClass()]
    public class StructureQuantityAndClientDtoTests
    {
        [TestMethod()]
        public void StructureQuantityAndClientDtoTest()
        {
            var result = new StructureQuantityAndClientDto();
            result.Quantity = 1;
            result.Clients.Add(new StructureClientDTO());

            result.Should().NotBeNull();
            result.Quantity.Should().Be(1);
            result.Clients.Should().NotBeEmpty();
        }
    }
}
