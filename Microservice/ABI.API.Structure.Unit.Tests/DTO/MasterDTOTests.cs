using ABI.API.Structure.Application.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Unit.Tests.DTO
{
    [TestClass()]
    public class MasterDTOTests
    {
        [TestMethod()]
        public void MasterDTOTest()
        {
            var result = new MasterDTO();
            result.Should().NotBeNull();
            result.attentionModes.Should().BeEmpty();
            result.levels.Should().BeEmpty();
            result.roles.Should().BeEmpty();
            result.saleChannels.Should().BeEmpty();
            result.structureModels.Should().BeEmpty();
        }
    }
}
