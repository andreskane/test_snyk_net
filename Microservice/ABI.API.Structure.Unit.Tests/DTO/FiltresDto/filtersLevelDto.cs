using ABI.API.Structure.Application.DTO.FiltresDto;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public  class filtersLevelDtoTests
    {
        [TestMethod]
        public void CreationTest()
        {
            var dto = new filtersLevelDto();
            dto.levelId = default;
           dto.parents = new List<int>();

            dto.Id.Should().Be(default);
            dto.Name.Should().Be(default);

            dto.levelId.Should().Be(default);
            dto.parents.Should().BeEmpty();
        }
      
    }
}
