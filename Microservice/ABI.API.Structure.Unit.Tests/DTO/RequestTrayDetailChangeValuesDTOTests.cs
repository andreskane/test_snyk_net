using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RequestTrayDetailChangeValuesDTOTests
    {
        [TestMethod()]
        public void RequestTrayDetailChangeValuesDTOTest()
        {
            var result = new RequestTrayDetailChangeValuesDTO();
            result.Id = 1;
            result.Old = "TEST";
            result.New = "TEST";
            result.Field = "TEST";
            result.FieldName = "TEST";

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Old.Should().Be("TEST");
            result.New.Should().Be("TEST");
            result.Field.Should().Be("TEST");
            result.FieldName.Should().Be("TEST");
        }
    }
}