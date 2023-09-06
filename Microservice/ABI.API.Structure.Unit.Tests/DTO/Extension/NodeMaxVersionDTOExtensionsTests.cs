using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.Application.DTO.Extension.Tests
{
    [TestClass()]
    public class NodeMaxVersionDTOExtensionsTests
    {
        [TestMethod()]
        public void ToTypeVersionPendingTest()
        {
            var input = string.Empty;
            var node = new NodePendingDTO
            {
                NodeMotiveStateId = (int)MotiveStateNode.Draft
            };
            var arista = new StructureArista();
            arista.EditMotiveStateId((int)MotiveStateNode.Confirmed);

            var result = NodeMaxVersionDTOExtensions.ToTypeVersion(input, node, arista);

            result.Should().Be("B");
        }

        [TestMethod()]
        public void ToTypeVersionNewTest()
        {
            var input = string.Empty;
            var node = new NodePendingDTO
            {
                NodeMotiveStateId = (int)MotiveStateNode.Draft
            };
            var arista = new StructureArista();
            arista.EditMotiveStateId((int)MotiveStateNode.Draft);

            var result = NodeMaxVersionDTOExtensions.ToTypeVersion(input, node, arista);

            result.Should().Be("N");
        }


        [TestMethod()]
        public void ToTypeVersionNullTest()
        {
            var input = string.Empty;
            var node = new NodePendingDTO
            {
                NodeMotiveStateId = (int)MotiveStateNode.Confirmed
            };
            var arista = new StructureArista();
            arista.EditMotiveStateId((int)MotiveStateNode.Confirmed);

            var result = NodeMaxVersionDTOExtensions.ToTypeVersion(input, node, arista);

            result.Should().BeNull();
        }
    }
}