using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class CreateNodeCommandValidatorTests
    {
        [TestMethod()]
        public void CreateNodeCommandValidatorTest()
        {
            var result = new CreateNodeCommandValidator(null, null);

            result.Should().NotBeNull();
        }
    }
}