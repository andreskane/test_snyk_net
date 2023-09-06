using System;

using ABI.API.Structure.Domain.Entities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Entities
{
    [TestClass()]
    public class MostVisitedFilterTests
    {
        [TestMethod()]
        public void MostVisitedFilterEmptyTest()
        {
            var result = new MostVisitedFilter();
            result.Should().NotBeNull();
            result.Id.Should().Be(0);
            result.Description.Should().BeNull();
            result.FilterType.Should().Be(0);
            result.IdValue.Should().Be(0);
            result.Quantity.Should().Be(0);
            result.StructureId.Should().Be(0);
            result.UserId.Should().Be(new Guid("00000000-0000-0000-0000-000000000000"));
        }

        [TestMethod()]
        public void MostVisitedFilterTest()
        {
            var result = new MostVisitedFilter {
                Description = "TEST",
                FilterType = 1,
                IdValue = 1,
                Quantity = 1
            };
            result.Should().NotBeNull();
            result.Description.Should().Be("TEST");
            result.FilterType.Should().Be(1);
            result.IdValue.Should().Be(1);
            result.Quantity.Should().Be(1);
        }

        [TestMethod()]
        public void MostVisitedFilterAddQuantityTest()
        {
            var result = new MostVisitedFilter();
            result.AddQuantity();
            result.Should().NotBeNull();
            result.Quantity.Should().Be(1);
        }
        [TestMethod()]
        public void MostVisitedFilterEditStructureIdTest()
        {
            var result = new MostVisitedFilter();
            result.EditStructureId(1);
            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
        }
        [TestMethod()]
        public void MostVisitedFilterEditUserIdTest()
        {
            var result = new MostVisitedFilter();
            result.EditUser(new Guid("3E8FA66E-3619-48F0-9F0B-60500005D7EF"));
            result.Should().NotBeNull();
            result.UserId.Should().Be("3E8FA66E-3619-48F0-9F0B-60500005D7EF");
        }
    }
}
