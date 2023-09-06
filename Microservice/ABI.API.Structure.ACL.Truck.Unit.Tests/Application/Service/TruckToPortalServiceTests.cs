using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using ABI.API.Structure.ACL.Truck.Application.DTO.ImportProcess;
using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureModel;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.Framework.MS.Domain.Common;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Service
{
    [TestClass()]
    public class TruckToPortalServiceTests
    {

        private static  IStructureRepository _repository;
        private static  IStructureNodeRepository _structureNodeRepository;
        private static  IMediator _mediator;
  

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repository = new StructureRepository(AddDataContext._context);
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);

            var country = new CountryDTO { Id = 1, Name = "ARGENTINA", Code = "AR" };

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetStructureModelByIdQuery>(), default))
                .ReturnsAsync(new StructureModelDTO
                {
                    Id = 1,
                    Name = "Test",
                    ShortName = "Test",
                    Description = "Test",
                    Code = "VTA",
                    Active = true,
                    CanBeExportedToTruck = false,
                    CountryId = 1,
                    Country = country
                }) ;


            _mediator = mediator.Object;
        }

        [TestMethod()]
        public void TruckToPortalServiceTest()
        {
            var service = new TruckToPortalService(_repository, _structureNodeRepository, _mediator);

            service.Should().NotBeNull();
            service._Structure.Should().BeNull();
            service._StructureAristaItems.Should().BeNull();
        }

        [TestMethod()]
        public void TruckToPortalServiceThrowsTest()
        {
            using var scope = new AssertionScope();

            scope
                .Invoking(i => new TruckToPortalService(null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new TruckToPortalService(_repository, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new TruckToPortalService(_repository, _structureNodeRepository, null))
                .Should().Throw<ArgumentNullException>();
        }

        [TestMethod()]
        public async Task GetNodesByLevelIdAsyncTestAsync()
        {
            var mockStructureNodeRepo = new Mock<IStructureNodeRepository>();
            mockStructureNodeRepo
                .Setup(s => s.GetNodesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() =>
                {
                    var node = new StructureNode(1);
                    node.EditLevel(new Structure.Domain.Entities.Level { Id = 1 });

                    return new List<StructureNode> { node };
                });

            var service = new TruckToPortalService(_repository, mockStructureNodeRepo.Object, _mediator);
            var results = await service.GetNodesByLevelIdAsync(1, 1);

            results.Should().NotBeNull();
        }


        [TestMethod()]
        public void MigrateEstructureAsyncTest()
        {
            var service = new TruckToPortalService(_repository, _structureNodeRepository, _mediator);

            var portal = new StructurePortalDTO
            {
                StructureModelId = 1,
                ValidityFrom = DateTime.UtcNow.Date,
                Nodes = new List<NodePortalDTO>()
            };

            var node = new NodePortalDTO {
                Name = "TEST",
                Code = "12",
                LevelId = 1,
                AttentionModeId = 1,
                EmployeeId = 1,
                IsRootNode = true,
                Active = true,
                ValidityFrom = DateTime.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date,
                VacantPerson = true,
                Nodes = new List<NodePortalDTO>()

            };

            portal.Nodes.Add(node);

            var result = service.MigrateEstructureAsync(portal, "Test");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task MigrateEstructureAsyncTestAsync()
        {
            var mockMediatr = new Mock<IMediator>();
            mockMediatr
                .Setup(s => s.Send(It.IsAny<GetStructureModelByIdQuery>(), default))
                .ReturnsAsync(new StructureModelDTO
                {
                    Name = "TEST",
                    Country = new CountryDTO { Code = "1", Name = "AR" },
                    Code = "VTA"
                });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow
                .Setup(s => s.SaveEntitiesAsync(default))
                .Returns(Task.FromResult(true));

            var mockStructureRepo = new Mock<IStructureRepository>();
            mockStructureRepo
                .Setup(s => s.Add(It.IsAny<StructureDomain>()))
                .Returns(new StructureDomain(1));
            mockStructureRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUow.Object);

            var service = new TruckToPortalService(mockStructureRepo.Object, _structureNodeRepository, mockMediatr.Object);
            var model = new StructurePortalDTO
            {
                Nodes = new List<NodePortalDTO>
                {
                    new NodePortalDTO
                    {
                        Code = "1",
                        Name = "TEST",
                        ValidityFrom = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                        Nodes = new List<NodePortalDTO>
                        {
                            new NodePortalDTO { Code = "2", Name = "TEST", ValidityFrom = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)) }
                        }
                    }
                }
            };
            var results = await service.MigrateEstructureAsync(model, "TEST");

            results.Should().BeGreaterThan(0);
        }


        [TestMethod()]
        public void MigrateEstructureAsyncLongCodeTest()
        {
            var service = new TruckToPortalService(_repository, _structureNodeRepository, _mediator);

            var portal = new StructurePortalDTO
            {
                StructureModelId = 1,
                ValidityFrom = DateTime.UtcNow.Date,
                Nodes = new List<NodePortalDTO>()
            };

            var node = new NodePortalDTO
            {
                Name = "TEST",
                Code = "12",
                LevelId = 1,
                AttentionModeId = 1,
                EmployeeId = 1,
                IsRootNode = true,
                Active = true,
                ValidityFrom = DateTime.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date,
                VacantPerson = true,
                Nodes = new List<NodePortalDTO>()

            };

            portal.Nodes.Add(node);

            var result = service.MigrateEstructureAsync(portal, "ARGENINA_TEST_NUEVO_BUENOS");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void MigrateEstructureAsyncNullTest()
        {

            var mediatorNull = new Mock<IMediator>();
            mediatorNull
                .Setup(s => s.Send(It.IsAny<GetStructureModelByIdQuery>(), default))
                         .ReturnsAsync(new StructureModelDTO
                         {
                             Id = 1,
                             Name = "Test",
                             ShortName = "Test",
                             Description = "Test",
                             Code = "VTA",
                             Active = true,
                             CanBeExportedToTruck = false,
                             CountryId = 1,
                             Country = null
                         });


            var service = new TruckToPortalService(_repository, _structureNodeRepository, mediatorNull.Object);

            var portal = new StructurePortalDTO
            {
                StructureModelId = 1,
                ValidityFrom = DateTime.UtcNow.Date,
                Nodes = new List<NodePortalDTO>()
            };

            var node = new NodePortalDTO
            {
                Name = "TEST",
                Code = "12",
                LevelId = 1,
                AttentionModeId = 1,
                EmployeeId = 1,
                IsRootNode = true,
                Active = true,
                ValidityFrom = DateTime.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date,
                VacantPerson = true,
                Nodes = new List<NodePortalDTO>()

            };

            portal.Nodes.Add(node);

            var result = service.MigrateEstructureAsync(portal, "ARGENINA_TEST_NUEVO_BUENOS");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void MigrateEstructureClientsAsyncTest()
        {
            var mockUow = new Mock<IUnitOfWork>();
            mockUow
                .Setup(s => s.SaveEntitiesAsync(default))
                .Returns(Task.FromResult(true));

            var mockStructureNodeRepo = new Mock<IStructureNodeRepository>();
            mockStructureNodeRepo
                .Setup(s => s.GetNodesTerritoryAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNode>
                {
                    new StructureNode("1", 1)
                });
            mockStructureNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUow.Object);

            var service = new TruckToPortalService(_repository, mockStructureNodeRepo.Object, _mediator);


            var listNodeTerritory = new List<StructureNode>();
            var node = new StructureNode(1);
            node.EditCode("1");
            listNodeTerritory.Add(node);

            var listInformatica = new List<DataIODto>();

            listInformatica.Add(new DataIODto { CliId = "1", CliNom = "TEST", CliTrrId = "1", CliSts = "1" });


            var results = service.MigrateEstructureClientsAsync(listInformatica, DateTimeOffset.MaxValue, listNodeTerritory);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task SaveCompareTestAsync()
        {
            var mockMediatr = new Mock<IMediator>();
            mockMediatr
                .Setup(s => s.Send(It.IsAny<GetAllNodeByStructureIdQuery>(), default))
                .ReturnsAsync(new List<PortalAristalDTO>
                {
                    new PortalAristalDTO
                    {
                        NodeId = 1,
                        NodeCode = "1",
                        NodeLevelId = 1
                    },
                    new PortalAristalDTO
                    {
                        NodeId = 2,
                        NodeCode = "2",
                        NodeLevelId = 2
                    }
                });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow
                .Setup(s => s.SaveEntitiesAsync(default))
                .Returns(Task.FromResult(true));
            mockUow
                .Setup(s => s.BeginTransactionAsync())
                .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

            var mockStructureNodeRepo = new Mock<IStructureNodeRepository>();
            mockStructureNodeRepo
                .Setup(s => s.UnitOfWork)
                .Returns(mockUow.Object);
            mockStructureNodeRepo
                .Setup(s => s.GetNodoOneByCodeLevelAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(() =>
                {
                    var node = new StructureNode(1);
                    node.EditCode("3");
                    node.EditLevel(new Structure.Domain.Entities.Level { Id = 3 });
                    return node;
                });
            mockStructureNodeRepo
                .Setup(s => s.GetNodoDefinitionValidityByNodeIdAsync(It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(new StructureNodeDefinition());

            var service = new TruckToPortalService(_repository, mockStructureNodeRepo.Object, mockMediatr.Object);
            var model = new List<NodePortalCompareDTO>
            {
                new NodePortalCompareDTO
                {
                    Code = "1",
                    LevelId = 1,
                    ParentNodeCode = "P1",
                    ParentNodeLevelId = 0,
                    TypeActionNode = TypeActionNode.New
                },
                new NodePortalCompareDTO
                {
                    Code = "2",
                    LevelId = 2,
                    ParentNodeCode = "1",
                    ParentNodeLevelId = 1,
                    TypeActionNode =  TypeActionNode.Draft
                }
            };
            var results = await service.SaveCompare(1, model);

            results.Should().BeGreaterThan(0);
        }

        [TestMethod()]
        public void GetStructureTest()
        {
            var mockStructureRepo = new Mock<IStructureRepository>();
            mockStructureRepo
                .Setup(s => s.GetStructureDataCompleteByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new StructureDomain());

            var service = new TruckToPortalService(mockStructureRepo.Object, _structureNodeRepository, _mediator);

            service
                .Invoking(i => i.GetStructure("TEST"))
                .Should().NotThrow();
        }
    }
}