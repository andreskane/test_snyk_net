using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;

using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Commands.RequestsTray
{
    [TestClass()]
    public class DeleteChangeGroupCommandTests
    {
        private static IChangeTrackingRepository _changeTrackingRepository;
        private static IChangeTrackingStatusRepository _changeTrackingStatusRepository;
        private static ICacheStore _cacheStore;
        private static IStructureRepository _structureRepository;


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

            _changeTrackingRepository = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _changeTrackingStatusRepository = new ChangeTrackingStatusRepository(AddDataContext._context, _cacheStore, default);
            _structureRepository = new StructureRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void DeleteCommandTest()
        {
            var result = new DeleteChangeGroupCommand { structureId = 1, validity = Convert.ToDateTime("2021-06-30 00:00:00.0000000 -03:00") };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerTest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockCurrentUser = new Mock<ICurrentUserService>();
            mockCurrentUser
                .Setup(s => s.UserId)
                .Returns(Guid.NewGuid());

            var command = new DeleteChangeGroupCommand { structureId = 1, validity = Convert.ToDateTime("2021-07-01 00:00:00.0000000 -03:00") };
            var handler = new DeleteChangeGroupCommandHandler(mockMediator.Object, mockCurrentUser.Object, _changeTrackingRepository, _changeTrackingStatusRepository, _structureRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerNodeTest()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(s => s.Send(It.IsAny<VersionedCancelAllChangesCommand>(), default))
                .ReturnsAsync(true);

            var mockCurrentUser = new Mock<ICurrentUserService>();
            mockCurrentUser
                .Setup(s => s.UserId)
                .Returns(Guid.NewGuid());

            var mockChangeTrackingRepo = new Mock<IChangeTrackingRepository>();
            mockChangeTrackingRepo
                .Setup(s => s.GetByStructureId(It.IsAny<int>(), default))
                .ReturnsAsync(new List<Domain.Entities.ChangeTracking>
                {
                    new Domain.Entities.ChangeTracking
                    {
                        User = new User { Id = mockCurrentUser.Object.UserId },
                        ValidityFrom = DateTimeOffset.MaxValue,
                        IdObjectType = 2,
                        ChangedValueNode = new ChangeTrackingNode { Node = new ItemNode { Id = 1 }}
                    }
                });
            var mockChangeTrackingStatusRepo = new Mock<IChangeTrackingStatusRepository>();
            mockChangeTrackingStatusRepo
                .Setup(s => s.GetByChangeId(It.IsAny<int>(), default))
                .ReturnsAsync(new Domain.Entities.ChangeTrackingStatus());

            var mockStructureRepo = new Mock<IStructureRepository>();
            mockStructureRepo
                .Setup(s => s.GetStructureDataCompleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new StructureDomain
                {
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        CanBeExportedToTruck = true
                    }
                });

            var command = new DeleteChangeGroupCommand { structureId = 1, validity = DateTimeOffset.MaxValue };
            var handler = new DeleteChangeGroupCommandHandler(
                mockMediator.Object,
                mockCurrentUser.Object,
                mockChangeTrackingRepo.Object,
                mockChangeTrackingStatusRepo.Object,
                mockStructureRepo.Object
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerAristTest()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(s => s.Send(It.IsAny<VersionedCancelAllChangesCommand>(), default))
                .ReturnsAsync(true);

            var mockCurrentUser = new Mock<ICurrentUserService>();
            mockCurrentUser
                .Setup(s => s.UserId)
                .Returns(Guid.NewGuid());

            var mockChangeTrackingRepo = new Mock<IChangeTrackingRepository>();
            mockChangeTrackingRepo
                .Setup(s => s.GetByStructureId(It.IsAny<int>(), default))
                .ReturnsAsync(new List<Domain.Entities.ChangeTracking>
                {
                    new Domain.Entities.ChangeTracking
                    {
                        User = new User { Id = mockCurrentUser.Object.UserId },
                        ValidityFrom = DateTimeOffset.MaxValue,
                        IdObjectType = 3,
                        IdDestino = 1
                    }
                });
            var mockChangeTrackingStatusRepo = new Mock<IChangeTrackingStatusRepository>();
            mockChangeTrackingStatusRepo
                .Setup(s => s.GetByChangeId(It.IsAny<int>(), default))
                .ReturnsAsync(new Domain.Entities.ChangeTrackingStatus());

            var mockStructureRepo = new Mock<IStructureRepository>();
            mockStructureRepo
                .Setup(s => s.GetStructureDataCompleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new StructureDomain
                {
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        CanBeExportedToTruck = true
                    }
                });

            var command = new DeleteChangeGroupCommand { structureId = 1, validity = DateTimeOffset.MaxValue };
            var handler = new DeleteChangeGroupCommandHandler(
                mockMediator.Object,
                mockCurrentUser.Object,
                mockChangeTrackingRepo.Object,
                mockChangeTrackingStatusRepo.Object,
                mockStructureRepo.Object
            );
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }
    }
}
