using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;
using ABI.Framework.MS.Repository.Exceptions;
using Coravel.Queuing.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class SaveChangesWithoutSavingCommandHandlerTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static IChangeTrackingRepository _changeTrackingRepository;
        private static IChangeTrackingStatusRepository _changeTrackingRepositoryStatus;
        private static ICurrentUserService _currentUserService;
        private static IMediator _mediator;
        private static ICacheStore _cacheStore;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {


            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);


            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<Queries.Structures.GetByIdQuery>(), default))
                .ReturnsAsync(new StructureDTO
                {
                    StructureModel = new StructureModelDTO
                    {
                        CanBeExportedToTruck = true
                    }
                });

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingWithoutSavingQuery>(), default))
                .ReturnsAsync(new List<StructureNodeDTO>
                {
                    new StructureNodeDTO
                    {
                        NodeId = 100013,
                        NodeMotiveStateId = 1,
                        NodeDefinitionId = 100014
                    },
                    new StructureNodeDTO
                    {
                        NodeId = 100023,
                        AristaMotiveStateId = 1,
                        NodeDefinitionId = 100025
                    },
                    new StructureNodeDTO
                    {
                        NodeId = 100022,
                        NodeMotiveStateId = 1,
                        AristaMotiveStateId = 1,
                        NodeDefinitionId = 100025
                    },
                    new StructureNodeDTO
                    {
                        NodeId = 100024,
                        NodeMotiveStateId = 4,
                        NodeDefinitionId = 100014
                    },
                    new StructureNodeDTO
                    {
                        NodeId = 100037,
                        NodeMotiveStateId = 1,
                        NodeDefinitionId = 100035
                    },
                    new StructureNodeDTO
                    {
                        NodeId = 100037,
                        NodeMotiveStateId = 4,
                        NodeDefinitionId = 100036
                    }
                });

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodeRootQuery>(), default))
                .ReturnsAsync(new StructureNodeDTO
                {
                    RootNodeId = 1,
                });

            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetNodeTreeByNodeIdQuery>(), default))
                .ReturnsAsync(new List<int>
                {
                    100013,1
                });

            _mediator = mediatorMock.Object;
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
            _changeTrackingRepository = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _currentUserService = InitCurrentUserService.GetDefaultUserService();
            _changeTrackingRepositoryStatus = new ChangeTrackingStatusRepository(AddDataContext._context, _cacheStore, default);
        }


        [TestMethod()]
        public void SaveChangesWithoutSavingCommandTest()
        {
            var result = new SaveChangesWithoutSavingCommand(1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTime.UtcNow.Date);
        }

        [TestMethod()]
        public async Task SaveChangesWithoutSavingCommandHandlerTest()
        {
            var queueMock = new Mock<IQueue>();
            var command = new SaveChangesWithoutSavingCommand(7, Convert.ToDateTime("2021-05-01"));
            var handler = new SaveChangesWithoutSavingCommandHandler(_nodeRepo, _changeTrackingRepository, _currentUserService, queueMock.Object, _mediator, _changeTrackingRepositoryStatus);
            var result = await handler.Handle(command, default);

            result.Should().BeTrue();
        }

        [TestMethod()]
        public async Task SaveChangesWithoutSavingCommandHandlerThrowsRepositoryExceptionTest()
        {
            var queueMock = new Mock<IQueue>();
            var handler = new SaveChangesWithoutSavingCommandHandler(_nodeRepo, _changeTrackingRepository, _currentUserService, queueMock.Object, _mediator, _changeTrackingRepositoryStatus);

            await handler
                .Invoking(async (i) => await i.Handle(null, default))
                .Should().ThrowAsync<RepositoryException>();
        }

        [TestMethod()]
        public void CreateRepoNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveChangesWithoutSavingCommandHandler(null, null, null, null, null, null)
            );
        }

        [TestMethod()]
        public void CreateChangeTrackingRepoNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveChangesWithoutSavingCommandHandler(_nodeRepo, null, null, null, null, null)
            );
        }

        [TestMethod()]
        public void CreateCurrentUserServiceNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveChangesWithoutSavingCommandHandler(_nodeRepo, _changeTrackingRepository, null, null, null, null)
            );
        }

        [TestMethod()]
        public void CreateQueueNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveChangesWithoutSavingCommandHandler(_nodeRepo, _changeTrackingRepository, _currentUserService, null, null, null)
            );
        }

        [TestMethod()]
        public void CreateMediatorNullException()
        {
            var queueMock = new Mock<IQueue>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveChangesWithoutSavingCommandHandler(_nodeRepo, _changeTrackingRepository, _currentUserService, queueMock.Object, null, null)
            );
        }

        [TestMethod()]
        public void CreateChangeTrackingStatusRepoNullException()
        {
            var queueMock = new Mock<IQueue>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveChangesWithoutSavingCommandHandler(_nodeRepo, _changeTrackingRepository, _currentUserService, queueMock.Object, _mediator, null)
            );
        }
    }
}