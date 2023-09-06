using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class AuditableEntityTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var guid = System.Guid.NewGuid();

            var result = new DemoClass
            {
                CreatedBy = guid,
                CreatedByName = "TEST",
                CreatedDate = DateTime.MinValue,
                LastModifiedBy = null,
                LastModifiedByName = "TEST",
                LastModifiedDate = null,
                CompanyId = "TEST"
            };

            result.Should().NotBeNull();
            result.CreatedBy.Should().Be(guid);
            result.CreatedByName.Should().Be("TEST");
            result.CreatedDate.Should().Be(DateTime.MinValue);
            result.LastModifiedBy.Should().BeNull();
            result.LastModifiedByName.Should().Be("TEST");
            result.LastModifiedDate.Should().BeNull();
            result.CompanyId.Should().Be("TEST");
        }
    }
}