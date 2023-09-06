using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.DTO.ACLTruck.Compare
{
    [TestClass()]
    public class StructurePortalCompareDTOTests
    {
        [TestMethod()]
        public void StructurePortalCompareDTOTest()
        {
            var result = new StructurePortalCompareDTO
            {
                Date = DateTimeOffset.MinValue,
                StructureId = 1,
                StructureModelId = 1,
                TruckNodes = null,
                PortalNodes = null,
                UpdateNodes = null
            };

            result.Should().NotBeNull();
            result.Date.Should().Be(DateTimeOffset.MinValue);
            result.StructureId.Should().Be(1);
            result.StructureModelId.Should().Be(1);
            result.TruckNodes.Should().BeNull();
            result.PortalNodes.Should().BeNull();
            result.UpdateNodes.Should().BeNull();
        }
    }
}