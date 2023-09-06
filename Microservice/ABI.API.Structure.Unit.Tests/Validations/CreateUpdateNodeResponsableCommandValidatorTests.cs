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
    public class CreateUpdateNodeResponsableCommandValidatorTests
    {
        private static Mock<IStructureNodeRepository> _repositoryStructureNodeMock;
        private static Mock<IAttentionModeRoleRepository> _attentionModeRoleRepoMock;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repositoryStructureNodeMock = new Mock<IStructureNodeRepository>();
            _repositoryStructureNodeMock
                .Setup(s => s.GetNodoChildAllByNodeIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNode>
                {
                    new StructureNode
                    {
                        StructureNodoDefinitions = new List<StructureNodeDefinition>
                        {
                            new StructureNodeDefinition(10, null, 1, 1, DateTimeOffset.MinValue, "N1", true),
                            new StructureNodeDefinition(10, null, 1, 2, DateTimeOffset.MinValue, "N2", true),
                        }
                    }
                });

            _attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            _attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });
        }


        [TestMethod()]
        public void CreateNodeResponsableCommandValidatorTrueTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 8, true, 10, null, 1, 1, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void CreateNodeResponsableCommandValidatorThrowsNodeResponsableExceptionTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 8, true, 13, 19, 1, 1, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeResponsableException>();
        }

        [TestMethod()]
        public void CreateNodeResponsableCommandValidatorThrowsNodeNoResponsableExceptionTest()
        {
            var repositoryStructureNodeMock = new Mock<IStructureNodeRepository>();
            repositoryStructureNodeMock
                .Setup(s => s.GetNodoChildAllByNodeIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNode>
                {
                    new StructureNode
                    {
                        StructureNodoDefinitions = new List<StructureNodeDefinition>
                        {
                            new StructureNodeDefinition(13, 19, 1, 1, DateTimeOffset.MinValue, "N1", true),
                            new StructureNodeDefinition(13, 19, 1, 2, DateTimeOffset.MinValue, "N2", true),
                        }
                    }
                });

            var model = new CreateNodeCommand(1, 1, "TEST", "1", 8, true, 10, null, 1, 1, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeNoResponsableException>();
        }

        [TestMethod()]
        public void CreateNodeResponsableCommandValidatorNoAttentionModeTest()
        {
            var model = new CreateNodeCommand(1, 1, "TEST", "1", 8, true, null, null, 1, 1, DateTimeOffset.MinValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(null, null);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void UpdateNodeResponsableCommandValidatorTrueTest()
        {
            var model = new EditNodeCommand(1, 1, 1, "TEST", "1", true, 10, null, 1, 1, 8, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void UpdateNodeResponsableCommandValidatorThrowsNodeResponsableExceptionTest()
        {
            var model = new EditNodeCommand(1, 1, 1, "TEST", "1", true, 13, 19, 1, 1, 8, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(_repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeResponsableException>();
        }

        [TestMethod()]
        public void UpdateNodeResponsableCommandValidatorThrowsNodeNoResponsableExceptionTest()
        {
            var repositoryStructureNodeMock = new Mock<IStructureNodeRepository>();
            repositoryStructureNodeMock
                .Setup(s => s.GetNodoChildAllByNodeIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNode>
                {
                    new StructureNode
                    {
                        StructureNodoDefinitions = new List<StructureNodeDefinition>
                        {
                            new StructureNodeDefinition(13, 19, 1, 1, DateTimeOffset.MinValue, "N1", true),
                            new StructureNodeDefinition(13, 19, 1, 2, DateTimeOffset.MinValue, "N2", true),
                        }
                    }
                });

            var model = new EditNodeCommand(1, 1, 1, "TEST", "1", true, 10, null, 1, 1, 8, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(repositoryStructureNodeMock.Object, _attentionModeRoleRepoMock.Object);

            validator
                .Invoking(i => validator.TestValidate(model))
                .Should().Throw<NodeNoResponsableException>();
        }


        [TestMethod()]
        public void UpdateNodeResponsableCommandValidatorNoAttentionModeTest()
        {
            var model = new EditNodeCommand(1, 1, 1, "TEST", "1", true, null, null, 1, 1, 8, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            var validator = new CreateUpdateNodeResponsableCommandValidator(null, null);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}