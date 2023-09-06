using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.APIClient.Truck.Models.Tests
{
    [TestClass()]
    public class TerritoryTests
    {
        [TestMethod()]
        public void TerritoryTest()
        {
            var result = new Territory("1","1","5555");

            result.Should().NotBeNull();
            result.EmpId.Should().Be("1"); 
            result.GerenciaID.Should().Be("1"); 
            result.TrrId.Should().Be("5555"); 
        }
    }
}