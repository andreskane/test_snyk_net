using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.Entities;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures.Tests
{
    [TestClass()]
    public class ValidateStructureCommandTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static ILevelTruckPortalRepository _levelTruckPortalRepo;
        private static ILevelRepository _levelRepo;
        private static IDBUHResourceRepository _resourcesRepo;
        private static IMediator _mediator;
        private static IAttentionModeRoleRepository _attentionModeRoleRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            AddDataContext.PrepareFactoryData();

            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetEmployeeIdsNodesByStructureQuery>(), default))
                .ReturnsAsync(new List<DTO.EmployeeIdNodesDTO>
                {
                    new DTO.EmployeeIdNodesDTO
                    {
                        EmployeeId = 2280,
                        Nodes = new List<DTO.EmployeeIdNodesItemDTO>
                        {
                            new DTO.EmployeeIdNodesItemDTO
                            {
                                Code = "1302",
                                Id = 288,
                                Name = "NORES MARCOS"
                            },
                            new DTO.EmployeeIdNodesItemDTO
                            {
                                Code = "1303",
                                Id = 315,
                                Name = "GRION GABRIEL"
                            }
                        }
                    }
                });

            var dbuhResourceMock = new Mock<IDBUHResourceRepository>();
            dbuhResourceMock
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<Domain.Entities.Resource>());

            _mediator = mediatrMock.Object;
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
            _levelTruckPortalRepo = new LevelTruckPortalRepository(AddDataTruckACLContext._context);
            _levelRepo = new LevelRepository(AddDataContext._context);
            _resourcesRepo = dbuhResourceMock.Object;
            _attentionModeRoleRepo = new AttentionModeRoleRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void ValidateStructureCommandTest()
        {
            var result = new ValidateStructureCommand(3, DateTimeOffset.MinValue);
            result.Should().NotBeNull();

            result.SetNodes(new List<DTO.StructureNodeDTO>());

            result.StructureId.Should().Be(3);
            result.Nodes.Should().BeEmpty();
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerTest()
        {
            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(_mediator, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerNoAttentionModeTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = null,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerNoSaleChannelTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = null
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerCodeDuplicatedTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        NodeMotiveStateId = (int)MotiveStateNode.Confirmed
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100014,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        NodeMotiveStateId = (int)MotiveStateNode.Confirmed
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(2);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerWrongLevelsTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        ContainsNodeId = 100015
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100015,
                        NodeDefinitionId = 100015,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = false,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 2,
                        NodeLevelId = 2,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(8, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerNodeCodeNullTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = null,
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerNodeNameNullTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = null,
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerWrongResourceLevelTest()
        {
            var dbuhResourceMock = new Mock<IDBUHResourceRepository>();
            dbuhResourceMock
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<Domain.Entities.Resource>
                {
                    new Domain.Entities.Resource
                    {
                        Id = 2,
                        Relations = new List<Domain.Entities.ResourceRelation>
                        {
                            new Domain.Entities.ResourceRelation
                            {
                                Type = 1,
                                Attributes = new Domain.Entities.ResourceAttributes
                                {
                                    VdrTpoCat = "X"
                                }
                            }
                        }
                    }
                });

            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "TEST",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 2,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, dbuhResourceMock.Object, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerParentInactiveChildActiveTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "PARENT",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = false,

                        NodeParentId = 0,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "2",
                        NodeName = "CHILD",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100013,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 1,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerResponsablesTrueTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "1",
                        NodeName = "TESTZONE",
                        NodeValidityFrom = new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 7,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        ContainsNodeId = 100015
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100015,
                        NodeDefinitionId = 100015,
                        NodeCode = "2",
                        NodeName = "TESTTERR1",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 13,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100016,
                        NodeDefinitionId = 100016,
                        NodeCode = "3",
                        NodeName = "TESTTERR2",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 10,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(8, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().BeNullOrEmpty();
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerResponsablesFalseTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "1",
                        NodeName = "TESTZONE",
                        NodeValidityFrom = new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 7,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        NodeMotiveStateId = (int)MotiveStateNode.Confirmed,

                        ContainsNodeId = 100015
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100015,
                        NodeDefinitionId = 100015,
                        NodeCode = "2",
                        NodeName = "TESTTERR1",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 13,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        NodeMotiveStateId = (int)MotiveStateNode.Confirmed
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100016,
                        NodeDefinitionId = 100016,
                        NodeCode = "3",
                        NodeName = "TESTTERR2",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 10,
                        NodeEmployeeId = 3,
                        NodeLevelId = 8,
                        NodeRoleId = null,
                        NodeSaleChannelId = 1,

                        NodeMotiveStateId = (int)MotiveStateNode.Confirmed
                    }
                });

            var attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });

            var command = new ValidateStructureCommand(8, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, attentionModeRoleRepoMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerEmployeeResponsableZonesFalseTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "Z1",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        ContainsNodeId = 100014,

                        NodeAttentionModeId = null,
                        NodeEmployeeId = 1,
                        NodeLevelId = 7,
                        NodeRoleId = 17,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "2",
                        NodeName = "T1",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100013,

                        NodeAttentionModeId = 10,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = null,
                        NodeSaleChannelId = 1
                    },                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100015,
                        NodeDefinitionId = 100015,
                        NodeCode = "3",
                        NodeName = "Z2",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        ContainsNodeId = 100016,

                        NodeAttentionModeId = null,
                        NodeEmployeeId = 1,
                        NodeLevelId = 7,
                        NodeRoleId = 17,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100016,
                        NodeDefinitionId = 100016,
                        NodeCode = "4",
                        NodeName = "T2",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100015,

                        NodeAttentionModeId = 13,
                        NodeEmployeeId = 3,
                        NodeLevelId = 8,
                        NodeRoleId = 19,
                        NodeSaleChannelId = 1
                    },

                });

            var attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, attentionModeRoleRepoMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(2);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerEmployeeResponsableTerritoriesFalseTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100013,
                        NodeDefinitionId = 100013,
                        NodeCode = "1",
                        NodeName = "N1",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 10,
                        NodeEmployeeId = 1,
                        NodeLevelId = 8,
                        NodeRoleId = null,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 7,
                        StructureModelID = 10000,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "2",
                        NodeName = "N2",
                        NodeValidityFrom = null,
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 13,
                        NodeEmployeeId = 1,
                        NodeLevelId = 8,
                        NodeRoleId = 19,
                        NodeSaleChannelId = 1
                    }

                });

            var attentionModeRoleRepoMock = new Mock<IAttentionModeRoleRepository>();
            attentionModeRoleRepoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(new List<AttentionModeRole>
                {
                    new AttentionModeRole { AttentionModeId = 10, RoleId = null, EsResponsable = true }
                });

            var command = new ValidateStructureCommand(7, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, attentionModeRoleRepoMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(2);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerRepeatedEmployeeTrueTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "1",
                        NodeName = "TESTZONE",
                        NodeValidityFrom = new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 7,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        ContainsNodeId = 100015
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100015,
                        NodeDefinitionId = 100015,
                        NodeCode = "2",
                        NodeName = "TESTTERR1",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 13,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100016,
                        NodeDefinitionId = 100016,
                        NodeCode = "3",
                        NodeName = "TESTTERR2",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 10,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetRepeatedEmployeeIdsNodesByStructureQuery>(), default))
                .ReturnsAsync(new List<DTO.EmployeeIdNodesDTO>
                {
                    new DTO.EmployeeIdNodesDTO
                    {
                        EmployeeId=2280,
                        Nodes = new List<DTO.EmployeeIdNodesItemDTO>
                        {
                            new DTO.EmployeeIdNodesItemDTO
                            {
                                Code="1",
                                Id=1,
                                Name ="TEST"
                            },
                            new DTO.EmployeeIdNodesItemDTO
                            {
                                Code="2",
                                Id=2,
                                Name ="TEST"
                            }
                        }
                    }
                });
                

            var command = new ValidateStructureCommand(8, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().HaveCount(1);
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerRepeatedEmployeeFalseTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100014,
                        NodeDefinitionId = 100014,
                        NodeCode = "1",
                        NodeName = "TESTZONE",
                        NodeValidityFrom = new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeAttentionModeId = 1,
                        NodeEmployeeId = 1,
                        NodeLevelId = 7,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1,

                        ContainsNodeId = 100015
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100015,
                        NodeDefinitionId = 100015,
                        NodeCode = "2",
                        NodeName = "TESTTERR1",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 13,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    },
                    new DTO.StructureNodeDTO
                    {
                        StructureId = 8,
                        StructureModelID = 10005,

                        NodeId = 100016,
                        NodeDefinitionId = 100016,
                        NodeCode = "3",
                        NodeName = "TESTTERR2",
                        NodeValidityFrom =  new DateTime(2020, 1, 1),
                        NodeValidityTo = DateTimeOffset.MaxValue,
                        NodeActive = true,

                        NodeParentId = 100014,
                        NodeAttentionModeId = 10,
                        NodeEmployeeId = 2,
                        NodeLevelId = 8,
                        NodeRoleId = 1,
                        NodeSaleChannelId = 1
                    }
                });

            var command = new ValidateStructureCommand(8, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().BeNullOrEmpty();
        }

        [TestMethod()]
        public async Task ValidateStructureCommandHandlerGetNodesAsyncTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllStructureNodesQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        NodeId = 100014,
                        NodeCode = "TEST1",
                        NodeName = "TEST1"
                    }
                });
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO
                    {
                        NodeId = 100015,
                        NodeCode = "TEST2",
                        NodeName = "TEST2"
                    }
                });

            var command = new ValidateStructureCommand(8, DateTimeOffset.MinValue);
            var handler = new ValidateStructureCommandHandler(mediatrMock.Object, _levelTruckPortalRepo, _nodeRepo, _resourcesRepo, _levelRepo, _structureRepo, _attentionModeRoleRepo);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ValidateStructure));
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}