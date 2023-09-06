using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class ResourceRelationTests
    {
        [TestMethod()]
        public void ResourceRelationTest()
        {
            var result = new ResourceRelation();
            result.Code = "TEST";
            result.Name = "TEST";
            result.Type = 1;

            result.Should().NotBeNull();
            result.Code.Should().Be("TEST");
            result.Name.Should().Be("TEST");
            result.Type.Should().Be(1);
        }
    }
}