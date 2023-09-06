using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class EditNodeCommandValidatorTests
    {
        [TestMethod()]
        public void EditNodeCommandValidatorTest()
        {
            var result = new EditNodeCommandValidator(null, null);

            result.Should().NotBeNull();
        }
    }
}