using ABI.API.Structure.ACL.Truck.Application.BackgroundServices;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.BackgroundService
{
    [TestClass()]
    public class ResourceResponsableBackgroundServiceTests
    {
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static IResourceResponsableRepository _resourceResponsableRepository;
        private static ISyncLogRepository _syncLogRepo;
        private static ISyncResourceResponsable _syncResourceResponsable;
        private static ILogger<SyncResourceResponsable> _logger;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<SyncResourceResponsable>>();
            _logger = loggerMock.Object;
            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");
            _resourceResponsableRepository = new ResourceResponsableRepository(AddDataTruckACLContext._context);
            _syncLogRepo = new SyncLogRepository(AddDataTruckACLContext._context);
            _syncResourceResponsable = new SyncResourceResponsable(_dBUHResourceRepository, _resourceResponsableRepository, _syncLogRepo, _logger);
        }

        [TestMethod()]
        public void ResourceResponsableBackgroundServiceCreateTest()
        {
            var service = new ResourceResponsableBackgroundService(_syncResourceResponsable, _syncLogRepo, _logger);
            service.Should().NotBeNull();
        }

        [TestMethod()]
        public void ResourceResponsableBackgroundServiceTest()
        {
            var service = new ResourceResponsableBackgroundService(_syncResourceResponsable, _syncLogRepo, _logger);
            service.Invoking(async i => await i.StartAsync(default))
                .Should().ThrowAsync<TaskCanceledException>();
        }
    }
}
