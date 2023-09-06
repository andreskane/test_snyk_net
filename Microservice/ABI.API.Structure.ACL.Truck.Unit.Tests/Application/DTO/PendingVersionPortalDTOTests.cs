using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class PendingVersionPortalDTOTests
    {
        [TestMethod()]
        public void PendingVersionPortalDTOTest()
        {
            var result = new PendingVersionPortalDTO();
            result.StructureId = 1;
            result.StructureName = "TEST";
            result.ValidityFrom = DateTimeOffset.UtcNow.Date;
            result.Message = "TEST";

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.StructureName.Should().Be("TEST");
            result.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
            result.Message.Should().Be("TEST");
        }
    }
}