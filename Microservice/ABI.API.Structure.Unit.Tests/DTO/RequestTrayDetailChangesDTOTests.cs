using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RequestTrayDetailChangesDTOTests
    {
        [TestMethod()]
        public void RequestTrayDetailChangesDTOTest()
        {
            var result = new RequestTrayDetailChangesDTO();
            result.Node = new ItemNodeDTO()
            {
                Name = "TEST"
            };
            result.Changes = null;

            result.Should().NotBeNull();
            result.Node.Name.Should().Be("TEST");
            result.Changes.Should().BeNull();
        }
    }
}