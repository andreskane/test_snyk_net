using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class CreateLevelCommandValidatorTests
    {
        [TestMethod()]
        public void CreateLevelCommandValidatorTest()
        {
            var result = new CreateLevelCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
