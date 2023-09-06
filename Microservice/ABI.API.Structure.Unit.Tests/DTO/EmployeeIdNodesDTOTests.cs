using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class EmployeeIdNodesDTOTests
    {
        [TestMethod()]
        public void CreationTest()
        {
            var dto = new EmployeeIdNodesDTO
            {
                EmployeeId = default,
                Nodes = default
            };

            dto.EmployeeId.Should().BeNull();
            dto.Nodes.Should().BeNull();
        }
    }
}