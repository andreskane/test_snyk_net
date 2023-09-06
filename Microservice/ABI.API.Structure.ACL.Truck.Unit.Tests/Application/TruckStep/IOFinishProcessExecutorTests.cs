using ABI.API.Structure.ACL.Truck.Application.BackgroundServices;
using ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.Framework.MS.Caching;

using Coravel.Events.Interfaces;

using FluentAssertions;

using MediatR;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.TruckStep
{
    [TestClass()]
    public class IOFinishProcessExecutorTests
    {
        private static IImportProcessRepository _importProcessRepository;
        private static IEstructuraClienteTerritorioIORepository _ImportProcessIORepository;
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

            _importProcessRepository = new ImportProcessRepository(AddDataTruckACLContext._context);
            _ImportProcessIORepository = new EstructuraClienteTerritorioIORepository(AddDataTruckACLContext._context, _cacheStore);
        }

        [TestMethod()]
        public void IOFinishProcessExecutorTest()
        {
            var result = new IOFinishProcessExecutor(null, null, null, null);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void InvokeTest()
        {
            var loggerMock = new Mock<ILogger<IOFinishProcessExecutor>>();
            var dispatcherMock = new Mock<IDispatcher>();
            var medietorMock = new Mock<IMediator>();

            var executor = new IOFinishProcessExecutor(_ImportProcessIORepository, _importProcessRepository, loggerMock.Object, medietorMock.Object);

            executor.Payload = new IOLoadFinishedCommand
            {
                ProccessId = 262,
                RowsCount = 0,
                State = Domain.Enums.ImportProcessState.Pending
            };

            executor.Invoking(async i => await i.Invoke())
                .Should().NotThrowAsync();
        }
    }
}
