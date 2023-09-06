using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class ChangesWithoutSavingDTOTests
    {
        [TestMethod()]
        public void ChangesWithoutSavingDTOTest()
        {
            var result = new ChangesWithoutSavingDTO();
            result.Field = "TEST";
            result.Value = "TEST";
            result.ValueNew = "TEST";
            result.FieldName = "TEST";

            result.Should().NotBeNull();
            result.Field.Should().Be("TEST");
            result.Value.Should().Be("TEST");
            result.ValueNew.Should().Be("TEST");
            result.FieldName.Should().Be("TEST");
        }
    }
}