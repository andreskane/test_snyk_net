using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class UpdateLevelCommandValidatorTests
    {
        [TestMethod()]
        public void UpdateLevelCommandValidatorTest()
        {
            var result = new UpdateLevelCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
