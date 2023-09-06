using ABI.API.Structure.ACL.Truck.Application.DTO.Extension;
using ABI.API.Structure.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Extension.Tests
{
    [TestClass()]
    public class ToCountryDTOExtensionsTests
    {
        [TestMethod()]
        public void ToCountryDTOExtensionsTest()
        {

            var country = new Country
            {
                Id = 1,
                Name = "ARGENTINA",
                Code = "AR"
            };

            var result = CountryExtensions.ToCountryDTO(country);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("ARGENTINA");
            result.Code.Should().Be("AR");

        }


    }
}