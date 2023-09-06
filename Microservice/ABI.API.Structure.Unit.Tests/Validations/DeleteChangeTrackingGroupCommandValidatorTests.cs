using ABI.API.Structure.Application.Validations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Validations
{
    [TestClass()]
    public class DeleteChangeTrackingGroupCommandValidatorTests
    {
        [TestMethod()]
        public void DeleteChangeTrackingGroupCommandValidatorTest()
        {
            var result = new DeleteChangeTrackingGroupCommandValidator(null, null);

            result.Should().NotBeNull();
        }
    }
}
