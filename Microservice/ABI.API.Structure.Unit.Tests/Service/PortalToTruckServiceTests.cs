using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Application.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ABI.API.Structure.ACL.Truck.Service.Tests
{
    [TestClass()]
    public class PortalToTruckServiceTests
    {
        [TestMethod()]
        public void GetOpiniNewVersionStructureTruckTest()
        {
            var service = new TruckService();

            var date = DateTime.UtcNow.AddDays(5).Date;
            var opini = service.GetTypeProcessTruck(TypeProcessTruck.NEW, date, "001", null);

            Assert.IsNotNull(opini);
        }
    }
}