using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.Domain.AggregatesModel.StructureAggregate.Tests
{
    [TestClass()]
    public class StructureClientNodeTests
    {
        [TestMethod()]
        public void StructureClientNodeTest()
        {
            var result = new StructureClientNode(1, "TEST", "1", "1", DateTimeOffset.MinValue.Date)
            {
                Node = new StructureNode()
            };

            result.Should().NotBeNull();
            result.NodeId.Should().Be(1); 
            result.Name.Should().Be("TEST"); 
            result.ClientId.Should().Be("1"); 
            result.ClientState.Should().Be("1"); 
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue.Date); 
            result.ValidityTo.Should().Be(DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3))); 
            result.Node.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureClientNodeEditsTest()
        {
            var date = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3));

            var result = new StructureClientNode(1, "TEST", "1", "1", DateTimeOffset.MinValue.Date)
            {
                Node = new StructureNode()
            };

            result.EditValidityTo(date);
            result.EditCientState("0");
            result.EditName("TEST2");
            result.EditNodeId(2);
            result.EditValidityFrom(date);

            result.Should().NotBeNull();
            result.NodeId.Should().Be(2);
            result.Name.Should().Be("TEST2");
            result.ClientId.Should().Be("1");
            result.ClientState.Should().Be("0");
            result.ValidityFrom.Should().Be(date);
            result.ValidityTo.Should().Be(date);
            result.Node.Should().NotBeNull();

        }

        [TestMethod()]
        public void StructureClientNodeEditNameTest()
        {
            var date = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3));

            var result = new StructureClientNode(1, "TEST", "1", "1", DateTimeOffset.MinValue.Date)
            {
                Node = new StructureNode()
            };
            result.EditName("TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2");
            result.Name.Should().Be("TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2TEST2");

        }

        [TestMethod()]
        public void StructureClientNodeEditShortNameTest()
        {
            var date = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3));

            var result = new StructureClientNode(1, "TEST", "1", "1", DateTimeOffset.MinValue.Date)
            {
                Node = new StructureNode()
            };
            result.EditName("TESTTEST");
            result.Name.Should().Be("TESTTEST");

        }
    }
}