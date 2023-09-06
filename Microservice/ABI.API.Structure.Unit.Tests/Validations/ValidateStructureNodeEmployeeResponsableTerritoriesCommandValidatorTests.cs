using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidatorTests
    {
        private static Mock<IAttentionModeRoleRepository> _attentionModeRoleRepoMock;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            _attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });
        }


        [TestMethod()]
        public void ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidatorCreateTest()
        {
            var result = new ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidator(null);
            result.NodesError = new List<DTO.StructureNodeDTO>();
            result.MessageError = "TEST";

            result.Should().NotBeNull();
            result.NodesError.Should().BeEmpty();
            result.MessageError.Should().Be("TEST");
        }

        [TestMethod()]
        public async Task ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidatorTrueTest()
        {
            var nodes = new List<StructureNodeDTO>
            {
                new StructureNodeDTO { NodeId = 1, NodeLevelId = 8, NodeEmployeeId = 1, NodeAttentionModeId = 10, NodeRoleId = null } // Responsable
            };
            var validator = new ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidator(_attentionModeRoleRepoMock.Object);
            var result = await validator.Validate(nodes);

            result.Should().BeTrue();
        }

        [TestMethod()]
        public async Task ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidatorFalseTest()
        {
            var nodes = new List<StructureNodeDTO>
            {
                new StructureNodeDTO { NodeId = 1, NodeLevelId = 8, NodeEmployeeId = 1, NodeAttentionModeId = 10, NodeRoleId = null }, // Responsable
                new StructureNodeDTO { NodeId = 2, NodeLevelId = 8, NodeEmployeeId = 1, NodeAttentionModeId = 13, NodeRoleId = 19 } // No responsable
            };
            var validator = new ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidator(_attentionModeRoleRepoMock.Object);
            var result = await validator.Validate(nodes);

            result.Should().BeFalse();
            validator.NodesError.Should().HaveCount(2);
        }
    }
}