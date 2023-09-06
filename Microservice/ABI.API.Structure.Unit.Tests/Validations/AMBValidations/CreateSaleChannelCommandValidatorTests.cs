using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class CreateSaleChannelCommandValidatorTests
    {
        [TestMethod()]
        public void CreateSaleChannelCommandValidatorTest()
        {
            var result = new CreateSaleChannelCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
