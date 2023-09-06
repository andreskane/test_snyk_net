using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class UpdateSaleChannelCommandValidatorTests
    {
        [TestMethod()]
        public void UpdateSaleChannelCommandValidatorTest()
        {
            var result = new UpdateSaleChannelCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
