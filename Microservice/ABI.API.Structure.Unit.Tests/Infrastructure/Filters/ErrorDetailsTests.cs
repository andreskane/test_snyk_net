using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Infrastructure.Filters.Tests
{
    [TestClass()]
    public class ErrorDetailsTests
    {
        [TestMethod()]
        public void ErrorDetailsTest()
        {
            var result = new ErrorDetails(null, null);

            result.Source.Should().BeNull();
            result.Message.Should().BeNull();

            result.Should().NotBeNull();
            result.ToString().Should().NotBeNull();
        }
    }
}