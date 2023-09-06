using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Unit.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class ValidateStructureSaleChannelCommandValidatorTests
    {
        [TestMethod()]
        public void ValidateSaleChannelTest()
        {
            var structureModels = FactoryMock.GetMockJson<List<StructureModelDefinition>>(FactoryMock.GetMockPath("ValidationLevelStructureModelDefinition.json"));
            var nodes = FactoryMock.GetMockJson<IList<StructureNodeDTO>>(FactoryMock.GetMockPath("ValidationLevelNodes.json"));
            var validate = new ValidateStructureSaleChannelCommandValidator
            {
                StructureModels = structureModels
            };

            validate.ValidateSaleChannel(nodes);

            Assert.AreEqual(0, validate.NodesError.Count);
        }
    }
}