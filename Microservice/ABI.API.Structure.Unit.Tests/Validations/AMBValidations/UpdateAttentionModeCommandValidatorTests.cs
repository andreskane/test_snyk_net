using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class UpdateAttentionModeCommandValidatorTests
    {
        [TestMethod()]
        public void UpdateAttentionModeCommandValidatorTest()
        {
            var result = new UpdateAttentionModeCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
