
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Extension;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.DTO.Extension
{
    [TestClass()]
    public class StructureNodeDTOExtensionsTests
    {
        [TestMethod()]
        public void StructureNodeDTOTest()
        {
            var result = new StructureNodeDTO();

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToStructureNodeDTOTest()
        {
            var result = StructureNodeExtensions.ToStructureNodeDTO(new List<PortalStructureNodeDTO>(), new List<PortalStructureNodeDTO>(), new StructureDomain(),true,false);

            result.Should().NotBeNull();
        }


    }
}