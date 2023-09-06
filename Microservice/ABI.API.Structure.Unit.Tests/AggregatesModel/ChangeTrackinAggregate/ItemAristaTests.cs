using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Unit.Tests.AggregatesModel.ChangeTrackinAggregate
{
    [TestClass()]
    public class ItemAristaTests
    {
        [TestMethod()]
        public void ItemAristaTest()
        {
            var result = new ItemArista
            {
                AristaId = 1,
                AristaValidityFrom = Convert.ToDateTime("2021-01-01"),
                AristaValidityTo = Convert.ToDateTime("2021-01-01"),
                NodeIdFrom=1,
                StructureIdFrom =1
            };

            result.AristaId.Should().Be(1);
            result.AristaValidityFrom.Should().Be(Convert.ToDateTime("2021-01-01"));
            result.AristaValidityTo.Should().Be(Convert.ToDateTime("2021-01-01"));
            result.NodeIdFrom.Should().Be(1);
            result.StructureIdFrom.Should().Be(1);
        }
    }
}
