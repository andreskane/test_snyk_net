using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.APIClient.Truck.Models.Tests
{
    [TestClass()]
    public class BusinessTests
    {
        [TestMethod()]
        public void BusinessTest()
        {
            var result = new Business("001");

            result.Should().NotBeNull();
            result.EmpId.Should().Be("001");
        }
    }
}