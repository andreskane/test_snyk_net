using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class RequestTrayFiltersDTOTests
    {
        [TestMethod()]
        public void RequestTrayFiltersDTOTest()
        {
            var result = new RequestTrayFiltersDTO();
            //result.Structure = null;
            //result.User = null;
            //result.KindOfChange = null;

            result.Structures = null;
            result.KindOfChanges = null;
            result.Users = null;
            result.portalStates = null;
            result.externalSystems = null;
            result.filters = null;
            result.levels = null;


            result.Should().NotBeNull();


            result.Structures.Should().BeNull();
            result.KindOfChanges.Should().BeNull();
            result.Users.Should().BeNull();
            result.portalStates.Should().BeNull();
            result.externalSystems.Should().BeNull();
            result.filters.Should().BeNull();
            result.levels.Should().BeNull();

        }
    }
}