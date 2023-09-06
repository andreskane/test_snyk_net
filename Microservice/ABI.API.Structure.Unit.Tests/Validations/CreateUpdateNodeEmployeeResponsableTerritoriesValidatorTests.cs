using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class CreateUpdateNodeEmployeeResponsableTerritoriesValidatorTests
    {
        private static Mock<IStructureNodeRepository> _repositoryStructureNodeMock;
        private static Mock<IAttentionModeRoleRepository> _attentionModeRoleRepoMock;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            var territories = new List<StructureNodeDefinition>
            {
                new StructureNodeDefinition(1, 10, null, 1, 1, DateTimeOffset.MinValue, "N1", true),
                new StructureNodeDefinition(2, 10, null, 1, 2, DateTimeOffset.MinValue, "N2", true)
            };

            territories.ForEach(f => f.EditVacantPerson(false));

            _repositoryStructureNodeMock = new Mock<IStructureNodeRepository>();
            _repositoryStructureNodeMock
                .Setup(s => s.GetTerritoriesByEmployeeId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(territories);

            _attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            _attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });
        }


        [TestMethod()]
        public void CreateUpdateNodeEmployeeResponsableTerritoriesValidatorThrowsNoResponsableExceptionTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 8, true, 13, 19, 1, 1, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeEmployeeResponsableTerritoriesValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeEmployeeNoResponsableTerritoriesException>();
        }

        [TestMethod()]
        public void CreateUpdateNodeEmployeeResponsableTerritoriesValidatorThrowsResponsableExceptionTest()
        {
            var territories = new List<StructureNodeDefinition>
            {
                new StructureNodeDefinition(1, 13, 19, 1, 1, DateTimeOffset.MinValue, "N1", true),
                new StructureNodeDefinition(2, 13, 19, 1, 2, DateTimeOffset.MinValue, "N2", true)
            };

            territories.ForEach(f => f.EditVacantPerson(false));

            var repositoryStructureNodeMock = new Mock<IStructureNodeRepository>();
            repositoryStructureNodeMock
                .Setup(s => s.GetTerritoriesByEmployeeId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(territories);

            var model = new CreateNodeCommand(1, 1, "TEST", "1", 8, true, 10, null, 1, 1, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeEmployeeResponsableTerritoriesValidator(repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeEmployeeResponsableTerritoriesException>();
        }

        [TestMethod()]
        public void CreateUpdateNodeEmployeeResponsableTerritoriesValidatorPrevalidateFalseTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 1, true, null, null, 1, null, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeEmployeeResponsableTerritoriesValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().NotThrow();
        }
    }
}