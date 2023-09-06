using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Domain.Entities.Tests
{
    [TestClass()]
    public class ObjectTypeTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var result = new ObjectType();
            //result.ChangeTracking = null;

            result.Should().NotBeNull();
            //result.ChangeTracking.Should().BeNull();
        }
    }
}