using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class LogImpactStructureStateDTOTests
    {
        [TestMethod()]
        public void LogImpactStructureStateDTOTest()
        {
            var result = new VersionedStructureStateDTO();
            result.StructureId = 1;
            result.StateId = 1;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StateId.Should().Be(1);
        }
    }
}