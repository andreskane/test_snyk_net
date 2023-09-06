using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class CreateAttentionModeCommandValidatorTests
    {
        [TestMethod()]
        public void CreateAttentionModeCommandValidatorTest()
        {
            var result = new CreateAttentionModeCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
