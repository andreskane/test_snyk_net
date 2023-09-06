using ABI.API.Structure.APIClient.Truck.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Exceptions.Api
{
    [TestClass()]
    public class ApiTruckExceptionTest
    {
        [TestMethod()]
        public void CreateApiTruckExceptionTest()
        {
            var exT = new ApiTruckException();

            exT.Should().NotBeNull();
        }


        [TestMethod()]
        public void CreateApiTruckExceptionMessageTest()
        {
            var exT = new ApiTruckException("error Test");

            exT.Should().NotBeNull();
            exT.Message.Should().Be("error Test");
        }

        [TestMethod()]
        public void CreateApiTruckExceptionInnerExceptionTest()
        {
            var exT = new ApiTruckException("error Test", new Exception().InnerException);

            exT.Should().NotBeNull();
            exT.Message.Should().Be("error Test");
            exT.InnerException.Should().BeNull();
        }
    }
}
