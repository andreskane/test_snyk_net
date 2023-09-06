using ABI.API.Structure.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.Entities
{
    [TestClass()]
    public class ChangeTrackingStatusTests
    {
        [TestMethod()]
        public void ChangeTrackingStatusTest()
        {
            var result = new ChangeTrackingStatus
            {
                CreatedDate = Convert.ToDateTime("2021-04-01"),
                Id = 1,
                IdChangeTracking = 1,
                IdStatus = 1,
                ChangeTracking = new ChangeTracking(),
                Status = new State()
            };
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.CreatedDate.Should().Be(Convert.ToDateTime("2021-04-01"));
            result.IdChangeTracking.Should().Be(1);
            result.IdStatus.Should().Be(1);
            result.Status.Should().NotBeNull();
            result.ChangeTracking.Should().NotBeNull();
        }
    }
}
