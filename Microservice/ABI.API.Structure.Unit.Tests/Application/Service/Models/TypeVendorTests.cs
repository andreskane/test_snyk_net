using ABI.API.Structure.ACL.Truck.Application.Service.ACL.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Models.Tests
{
    [TestClass()]
    public class TypeVendorTests
    {
        [TestMethod()]
        public void TypeVendorTest()
        {
            var result = new TypeVendor
            {
                Id = 1,
                Name = "TEST",
                Code = "TEST",
                Description = "TEST"
            };

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("TEST");
            result.Code.Should().Be("TEST");
            result.Description.Should().Be("TEST");
        }



        [TestMethod()]
        public void TypeVendorConstructorTest()
        {
            var result = new TypeVendor();

            result.Should().NotBeNull();

        }
    }
}