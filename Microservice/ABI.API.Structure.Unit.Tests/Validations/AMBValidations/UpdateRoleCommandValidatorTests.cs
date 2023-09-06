using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class UpdateRoleCommandValidatorTests
    {
        [TestMethod()]
        public void UpdateRoleCommandValidatorTest()
        {
            var result = new UpdateRoleCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
