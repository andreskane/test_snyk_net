using ABI.API.Structure.Application.DTO.RequestTray;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class FiltersOptionsQueryDTOTests
    {

        [TestMethod]
        public void CreationTest()
        {
            var dto = new FiltersOptionsQueryDTO();
 

            dto.PeriodFrom = default;


            dto.PeriodTo = default;

            dto.sId.Should().BeEmpty();
            dto.PeriodFrom.Should().Be(default);
            dto.PeriodTo.Should().Be(default);
        }


        


       
}
}
