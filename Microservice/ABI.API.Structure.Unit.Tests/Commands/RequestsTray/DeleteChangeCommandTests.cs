using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;

using FluentAssertions;

using MediatR;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ABI.API.Structure.Unit.Tests.Commands.RequestsTray
{
    [TestClass()]
    public class DeleteChangeCommandTests
    {
        private static IChangeTrackingRepository _changeTrackingRepository;
        private static IChangeTrackingStatusRepository _changeTrackingStatusRepository;
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

            _changeTrackingRepository = new ChangeTrackingRepository(AddDataContext._context, _cacheStore);
            _changeTrackingStatusRepository = new ChangeTrackingStatusRepository(AddDataContext._context, _cacheStore, default);
        }

        [TestMethod()]
        public void DeleteCommandTest()
        {
            var result = new DeleteChangeCommand { Id = 13, DeleteConfirm = true };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerNoExceptionTest()
        {
            var mockMediator = new Mock<IMediator>();
            var command = new DeleteChangeCommand { Id = 13 };
            var handler = new DeleteChangeCommandHandler(mockMediator.Object, InitCurrentUserService.GetDefaultUserService(), _changeTrackingRepository, _changeTrackingStatusRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<ConfirmException>();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerExceptionAttentionModeTest()
        {
            var mockMediator = new Mock<IMediator>();
            var command = new DeleteChangeCommand { Id = 28 };
            var handler = new DeleteChangeCommandHandler(mockMediator.Object, InitCurrentUserService.GetDefaultUserService(), _changeTrackingRepository, _changeTrackingStatusRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<ConfirmException>();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerExceptionRoleTest()
        {
            var mockMediator = new Mock<IMediator>();
            var command = new DeleteChangeCommand { Id = 29 };
            var handler = new DeleteChangeCommandHandler(mockMediator.Object, InitCurrentUserService.GetDefaultUserService(), _changeTrackingRepository, _changeTrackingStatusRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<ConfirmException>();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerExceptionPersonaTest()
        {
            var mockMediator = new Mock<IMediator>();
            var command = new DeleteChangeCommand { Id = 30 };
            var handler = new DeleteChangeCommandHandler(mockMediator.Object, InitCurrentUserService.GetDefaultUserService(), _changeTrackingRepository, _changeTrackingStatusRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<ConfirmException>();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerTest()
        {
            var mockMediator = new Mock<IMediator>();
            var command = new DeleteChangeCommand { Id = 31, DeleteConfirm = true };
            var handler = new DeleteChangeCommandHandler(mockMediator.Object, InitCurrentUserService.GetDefaultUserService(), _changeTrackingRepository, _changeTrackingStatusRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
        [TestMethod()]
        public async Task DeleteCommandHandlerOneChangeTest()
        {
            var mockMediator = new Mock<IMediator>();
            var command = new DeleteChangeCommand { Id = 34 };
            var handler = new DeleteChangeCommandHandler(mockMediator.Object, InitCurrentUserService.GetDefaultUserService(), _changeTrackingRepository, _changeTrackingStatusRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerAristTest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockChangeTrackingRepo = new Mock<IChangeTrackingRepository>();
            mockChangeTrackingRepo
                .Setup(s => s.GetById(It.IsAny<int>(),true, default))
                .ReturnsAsync(new Domain.Entities.ChangeTracking
                {
                    IdObjectType = 3,
                    IdDestino = 1,
                    ValidityFrom = DateTimeOffset.UtcNow,
                    ChangedValueArista = new ChangeTrackingArista
                    {
                        AristaActual = new ItemAristaActual
                        {
                            AristaId = 1,
                            OldValue = new ItemAristaCompare
                            {
                                NodeIdFrom = 1
                            }
                        }
                    }
                });

            var mockChangeTrackingStatusRepo = new Mock<IChangeTrackingStatusRepository>();

            var command = new DeleteChangeCommand { Id = 13, DeleteConfirm = true };
            var handler = new DeleteChangeCommandHandler(
                mockMediator.Object,
                InitCurrentUserService.GetDefaultUserService(),
                mockChangeTrackingRepo.Object,
                mockChangeTrackingStatusRepo.Object
            );

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}
