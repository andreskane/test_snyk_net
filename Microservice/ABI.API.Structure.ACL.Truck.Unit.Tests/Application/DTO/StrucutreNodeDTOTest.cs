using ABI.API.Structure.ACL.Truck.Application.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.DTO
{
    [TestClass()]
    public class StrucutreNodeDTOTest
    {
        [TestMethod()]
        public void StructureNodoTest()
        {
            var result = new StructureNodeDTO
            {
                Structure = null,
                ChangesWithoutSaving = true,
                Nodes = null
            };

            result.Should().NotBeNull();
            result.Structure.Should().BeNull();
            result.ChangesWithoutSaving.Should().BeTrue();
            result.Nodes.Should().BeNull();
        }
    }
}
