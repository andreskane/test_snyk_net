using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class TruckProcessStatusStructureDTOTests
    {
        [TestMethod()]
        public void TruckProcessStatusStructureDTOTest()
        {
            var result = new TruckProcessStatusStructureDTO();
            result.Id = 1;
            result.Name = "TEST";

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
        }
    }
}