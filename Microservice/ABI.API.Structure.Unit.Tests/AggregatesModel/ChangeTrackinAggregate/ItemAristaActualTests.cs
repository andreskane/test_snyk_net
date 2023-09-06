using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.AggregatesModel.ChangeTrackinAggregate
{
    [TestClass()]
    public class ItemAristaActualTests
    {
        [TestMethod()]
        public void ItemAristaActualTest()
        {
            var result = new ItemAristaActual
            {
                AristaId = 1,
                NewValue = new ItemAristaCompare { 
                    NodeIdFrom=1
                },
                OldValue = new ItemAristaCompare
                {
                    NodeIdFrom =1
                }
            };

            result.AristaId.Should().Be(1);
            result.NewValue.NodeIdFrom.Should().Be(1);
            result.OldValue.NodeIdFrom.Should().Be(1);
        }
    }
}
