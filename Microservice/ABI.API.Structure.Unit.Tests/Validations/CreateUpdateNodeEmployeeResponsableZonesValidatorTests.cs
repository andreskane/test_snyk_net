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
    public class CreateUpdateNodeEmployeeResponsableZonesValidatorTests
    {
        private static Mock<IStructureNodeRepository> _repositoryStructureNodeMock;
        private static Mock<IAttentionModeRoleRepository> _attentionModeRoleRepoMock;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            var territories = new List<StructureNodeDefinition>
            {
                new StructureNodeDefinition(1, 10, null, 1, 1, DateTimeOffset.MinValue, "N1", true),
                new StructureNodeDefinition(2, 13, 19, 1, 2, DateTimeOffset.MinValue, "N2", true)
            };

            territories.ForEach(f => f.EditVacantPerson(false));

            _repositoryStructureNodeMock = new Mock<IStructureNodeRepository>();
            _repositoryStructureNodeMock
                .Setup(s => s.GetTerritoriesByZonesEmployeeId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(territories);

            _repositoryStructureNodeMock
                .Setup(s => s.GetNodoChildAllByNodeIdAsync(It.IsAny<int>(), It.IsNotIn(99)))
                .ReturnsAsync(new List<StructureNode>());


            var nd = new StructureNodeDefinition();
            nd.EditValidityFrom(DateTimeOffset.MinValue);
            nd.EditValidityTo(DateTimeOffset.MaxValue);
            nd.EditMotiveStateId(1);

            _repositoryStructureNodeMock
                .Setup(s => s.GetNodoChildAllByNodeIdAsync(It.IsAny<int>(), It.Is<int>(i => i.Equals(99))))
                .ReturnsAsync(new List<StructureNode>
                {
                    new StructureNode()
                    {
                        StructureNodoDefinitions = new List<StructureNodeDefinition> { nd }
                    }
                });

            _repositoryStructureNodeMock
                .Setup(s => s.GetNodoDefinitionAsync(It.IsNotIn(99), It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(new StructureNodeDefinition(5, 10, null, null, 1, DateTime.Now, "TEST", true));

            _repositoryStructureNodeMock
                .Setup(s => s.GetNodoDefinitionAsync(It.Is<int>(i => i.Equals(99)), It.IsAny<DateTimeOffset?>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(new StructureNodeDefinition(5, 10, null, null, null, DateTime.Now, "TEST", true));

            _attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            _attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });
        }


        [TestMethod()]
        public void CreateUpdateNodeEmployeeResponsableZonesValidatorPreValidateFalse()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "TEST", 7, true, null, null, null, null, DateTime.Now, false, null);
            var validator = new CreateUpdateNodeEmployeeResponsableZonesValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateUpdateNodeEmployeeResponsableZonesValidatorTerritoriesDefinitionsTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 7, true, null, 17, 1, 3, DateTime.Now) { Id = 99 };
            var validator = new CreateUpdateNodeEmployeeResponsableZonesValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeEmployeeNoResponsableZonesException>();
        }

        [TestMethod()]
        public void CreateUpdateNodeEmployeeResponsableZonesValidatorThrowsNoResponsableExceptionTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 7, true, null, 17, 1, 3, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeEmployeeResponsableZonesValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeEmployeeNoResponsableZonesException>();
        }
    }
}