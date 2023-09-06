using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class UserFilterDtoTests
    {
        [TestMethod]
        public void CreationTest()
        {
            var dto = new UserFilterDto();
            dto.structureIds = new List<string>();
            dto.kindOfChangeIds = new List<string>();


            dto.Id.Should().Be(default);
            dto.Name.Should().Be(default);

            dto.structureIds.Should().BeEmpty();
            dto.kindOfChangeIds.Should().BeEmpty();
         }
      
    }
}
