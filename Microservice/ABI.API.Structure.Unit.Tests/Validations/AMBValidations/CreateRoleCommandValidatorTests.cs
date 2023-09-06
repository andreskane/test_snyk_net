using ABI.API.Structure.Application.Validations.ABMValidation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations.AMBValidations
{
    [TestClass()]
    public class CreateRoleCommandValidatorTests
    {
        [TestMethod()]
        public void CreateRoleCommandValidatorTest()
        {
            var result = new CreateRoleCommandValidator(null);

            result.Should().NotBeNull();
        }
    }
}
