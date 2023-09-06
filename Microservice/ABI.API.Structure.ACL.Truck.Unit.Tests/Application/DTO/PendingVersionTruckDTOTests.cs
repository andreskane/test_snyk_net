using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class PendingVersionTruckDTOTests
    {

        [TestMethod()]
        public void PendingVersionTruckDTOTest()
        {
            var result = new PendingVersionTruckDTO
            {
                LastVersionDate = null,
                StructureEdit = true,
                VersionTruck = null,
                Message = null
            };

            result.Should().NotBeNull();
            result.LastVersionDate.Should().BeNull();
            result.StructureEdit.Should().Be(true);
            result.VersionTruck.Should().BeNull();
            result.Message.Should().BeNull();
        }


        [TestMethod()]
        public void PendingVersionTruckDTOFalseTest()
        {

            var date = DateTime.Now;

            var result = new PendingVersionTruckDTO
            {
                LastVersionDate = date,
                StructureEdit = false,
                VersionTruck = "1350",
                Message = $"Existe en Truck una versión: 1350 programada para fecha {date.ToString("dd/MM/yyyy")} . La estructura no podrá ser editada en este momento. únicamente podrá visualizar a la fecha actual"
            };

            result.Should().NotBeNull();
            result.LastVersionDate.Should().Be(date);
            result.StructureEdit.Should().Be(false);
            result.VersionTruck.Should().Be("1350");
            result.Message.Should().NotBeNull();
        }

    }
}
