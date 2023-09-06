using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class ResourceTests
    {
        [TestMethod()]
        public void ResourceTest()
        {
            var result = new Resource();
            result.Id = 1;
            result.Company = "TEST";
            result.Code = "TEST";
            result.Name = "TEST";
            result.Relations = null;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Company.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.Name.Should().Be("TEST");
            result.Relations.Should().BeNull();
        }
    }
}