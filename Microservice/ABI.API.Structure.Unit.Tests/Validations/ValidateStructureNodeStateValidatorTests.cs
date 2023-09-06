using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Unit.Tests.Mock;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class ValidateStructureNodeStateValidatorTests
    {
        [TestMethod()]
        public void ValidateStructureNodeStateValidatorCreateTest()
        {
            var validator = new ValidateStructureNodeStateValidator();
            validator.MessageError = "TEST";

            validator.Should().NotBeNull();
            validator.MessageError.Should().Be("TEST");
        }

        [TestMethod()]
        public void ValidateStructureNodeStateValidatorTest()
        {
            var nodes = FactoryMock.GetMockJson<IList<StructureNodeDTO>>(FactoryMock.GetMockPath("ValidationLevelNodes.json"));
            var validate = new ValidateStructureNodeStateValidator();

            validate.Validate(nodes);

            validate.NodesError.Count.Should().Be(0);
        }

        [TestMethod()]
        public void ValidateStructureNodeStateValidatorFalseTest()
        {
            var nodes = new List<StructureNodeDTO>
            {
                new StructureNodeDTO { NodeId = 1, NodeParentId = 0, NodeActive = false },
                new StructureNodeDTO { NodeId = 2, NodeParentId = 1, NodeActive = true }
            };
            var validate = new ValidateStructureNodeStateValidator();

            validate.Validate(nodes);

            validate.NodesError.Count.Should().Be(1);
        }
    }
}