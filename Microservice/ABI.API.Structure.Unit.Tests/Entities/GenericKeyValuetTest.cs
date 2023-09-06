using ABI.API.Structure.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.Entities
{
    [TestClass]
    public  class GenericKeyValueTest
    {
        [TestMethod]
        public void CreationTest()
        {
            var dto = new GenericKeyValue();
            dto.Id = default;
            dto.Name = default;
             

            dto.Id.Should().Be(default);
            dto.Name.Should().Be(default);
             
        }
    }
}
