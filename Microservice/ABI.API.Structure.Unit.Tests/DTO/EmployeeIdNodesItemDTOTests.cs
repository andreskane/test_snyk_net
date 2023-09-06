using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class EmployeeIdNodesItemDTOTests
    {
        [TestMethod()]
        public void EmployeeIdNodesItemDTOTest()
        {
            var result = new EmployeeIdNodesItemDTO();
            result.Id = 1;
            result.Code = "TEST";
            result.Name = "TEST";
            result.Level = "TEST";

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Code.Should().Be("TEST");
            result.Name.Should().Be("TEST");
            result.Level.Should().Be("TEST");
        }
    }
}