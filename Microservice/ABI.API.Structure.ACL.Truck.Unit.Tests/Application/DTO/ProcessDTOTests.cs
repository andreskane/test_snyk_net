using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class ProcessDTOTests
    {
        [TestMethod()]
        public void ProcessDTOTest()
        {
            var result = new ProcessDTO();
            result.StructureId = null;
            result.Start();
            result.Stop();
            result.AddLog("TEST");
            result.AddError("TEST");

            result.Should().NotBeNull();
            result.StructureId.Should().BeNull();
            result.ProcessStartDateTime.Should().NotBeNull();
            result.ProcessTime.Should().NotBeNull();
            result.ProcessLog.Should().HaveCount(1);
            result.ProcessError.Should().HaveCount(1);

            result.GetDateTime().Should().StartWith(DateTimeOffset.UtcNow.ToString("{dd/MM/yyyy HH:mm"));
        }
    }
}