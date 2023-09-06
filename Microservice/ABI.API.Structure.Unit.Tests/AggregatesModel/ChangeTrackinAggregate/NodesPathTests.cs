using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.AggregatesModel.ChangeTrackinAggregate
{
    [TestClass()]
    public class NodesPathTests
    {
        [TestMethod()]
        public void NodesPathTest()
        {
            var result = new NodesPath
            {
                Ids = new System.Collections.Generic.List<int>()
            };

            result.Ids.Add(1);

            result.Should().NotBeNull();
            result.Ids.Should().NotBeEmpty();
        }
    }
}
