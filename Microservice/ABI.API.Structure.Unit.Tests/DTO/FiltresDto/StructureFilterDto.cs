using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureListFilterDtoTests
    {
        [TestMethod]
        public void CreationTest()
        {
            var dto = new StructureListFilterDto();
            dto.StructureListFilter = default;


        }

    }
    public class StructureFilterDtoTest
    {
        [TestMethod]
        public void CreationTest()
        {
            var dto = new StructureFilterDto();

            dto.Id = default;
            dto.Name = default;


            dto.Id.Should().Be(default);
            dto.Name.Should().Be(default);
        
        }
    }
}
