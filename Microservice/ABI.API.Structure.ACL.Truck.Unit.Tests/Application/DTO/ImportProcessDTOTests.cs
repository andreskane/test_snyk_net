using ABI.API.Structure.ACL.Truck.Domain.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Tests
{
    [TestClass()]
    public class ImportProcessDTOTests
    {
        [TestMethod()]
        public void ImportProcessDTOT()
        {
            var result = new ImportProcessDTO
            {
                Id = 1,
                ProcessDate = DateTime.MinValue,
                Condition = "TEST",
                Periodicity = Periodicity.Daily,
                ProcessState = ImportProcessState.DoneOk,
                StartDate = null,
                EndDate = null,
                ProcessedRows = null
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.ProcessDate.Should().Be(DateTime.MinValue);
            result.Condition.Should().Be("TEST");
            result.Periodicity.Should().NotBeNull();
            result.ProcessState.Should().NotBeNull();
            result.StartDate.Should().BeNull();
            result.EndDate.Should().BeNull();
            result.ProcessedRows.Should().BeNull();
        }
    }
}