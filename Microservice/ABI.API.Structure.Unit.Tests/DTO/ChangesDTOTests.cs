using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class ChangesDTOTests
    {
        [TestMethod()]
        public void ChangesDTOTest()
        {
            var result = new ChangesDTO();
            result.Nodes = null;

            result.Should().NotBeNull();
            result.Nodes.Should().BeNull();
        }
    }
}