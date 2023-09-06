using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public  class KindOfChangeFilterDtoTests
    {
        [TestMethod]
        public void CreationTest()
        {
            var dto = new KindOfChangeFilterDto();
            dto.structureIds = new List<string>();


            dto.Id.Should().Be(default);
            dto.Name.Should().Be(default);
            dto.structureIds.Should().BeEmpty();
         }
       
    }
}
