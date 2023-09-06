using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Tests
{
    [TestClass()]
    public class StructureChangesWithoutSavingDTOTests
    {
        [TestMethod()]
        public void StructureChangesWithoutSavingDTOTest()
        {
            var result = new StructureChangesWithoutSavingDTO();
            result.ChangesWithoutSaving = true;

            result.Should().NotBeNull();
            result.ChangesWithoutSaving.Should().BeTrue();
        }
    }
}