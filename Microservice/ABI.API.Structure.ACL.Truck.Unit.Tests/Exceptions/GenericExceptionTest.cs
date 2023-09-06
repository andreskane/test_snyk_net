
using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Exceptions
{
    [TestClass()]
    public class GenericExceptionTest
    {
        [TestMethod()]
        public void CreateApiTruckExceptionTest()
        {
            var exT = new GenericException();

            exT.Should().NotBeNull();
        }


        [TestMethod()]
        public void CreateApiTruckExceptionMessageTest()
        {
            var exT = new GenericException("error Test");

            exT.Should().NotBeNull();
            exT.Message.Should().Be("error Test");
        }

        [TestMethod()]
        public void CreateApiTruckExceptionInnerExceptionTest()
        {
            var exT = new GenericException("error Test", new Exception().InnerException);

            exT.Should().NotBeNull();
            exT.Message.Should().Be("error Test");
            exT.InnerException.Should().BeNull();
        }
    }
}
