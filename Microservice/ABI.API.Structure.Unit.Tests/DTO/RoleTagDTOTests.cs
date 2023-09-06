using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RoleTagDTOTests
    {
        [TestMethod()]
        public void RoleTagDTOTest()
        {
            var result = new RoleTagDTO();
            result.Tags = null;

            result.Should().NotBeNull();
            result.Tags.Should().BeNull();
        }
    }
}