using ABI.API.Structure.Application.Validations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations
{
    [TestClass()]
    public class DeleteChangeTrackingCommandValidatorTests
    {
        [TestMethod()]
        public void DeleteChangeTrackingCommandValidatorTest()
        {
            var result = new DeleteChangeTrackingCommandValidator(null, null);

            result.Should().NotBeNull();
        }
    }
}
