using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.AggregatesModel.ChangeTrackinAggregate
{
    [TestClass()]
    public class ItemAristaCompareTests
    {
        [TestMethod()]
        public void ItemAristaCompareTest()
        {
            var result = new ItemAristaCompare
            {
                NodeIdFrom =1,
                AristaValidityTo = Convert.ToDateTime("2021-04-01"),
                StructureIdFrom = 1
            };

            result.NodeIdFrom.Should().Be(1);
            result.AristaValidityTo.Should().Be(Convert.ToDateTime("2021-04-01"));
            result.StructureIdFrom.Should().Be(1);
        }
    }
}
