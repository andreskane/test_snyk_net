using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class CountryDTOTests
    {
        [TestMethod()]
        public void CountryDTOTest()
        {
            var result = new CountryDTO
            {
                Id = 1,
                Name = "TEST",
                Code = "TEST"
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
        }
    }
}