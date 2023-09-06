using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RequestTrayDTOTests
    {
        [TestMethod()]
        public void RequestTrayDTOTest()
        {
            var result = new RequestTrayDTO();
            result.Structure = new ItemDTO
            {
                Name = "TEST"
            };
            result.User = new UserDTO()
            {
                Name = "TEST"
            };
            result.Validity = DateTimeOffset.MinValue;
            result.ChangeType = null;
            result.PortalStatus = 1;
            result.TruckStatus = null;
            result.StructureCode = "TESTCODE";
            result.NodeDefinitionID = 1;

            result.Should().NotBeNull();
            result.Structure.Name.Should().Be("TEST");
            result.User.Name.Should().Be("TEST");
            result.Validity.Should().Be(DateTimeOffset.MinValue);
            result.ChangeType.Should().BeNull();
            result.PortalStatus.Should().Be(1);
            result.TruckStatus.Should().BeNull();
            result.StructureCode.Should().Be("TESTCODE");
            result.NodeDefinitionID.Should().Be(1);
        }
    }
}