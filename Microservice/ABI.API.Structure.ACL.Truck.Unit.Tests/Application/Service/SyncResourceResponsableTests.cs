using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;

using FluentAssertions;

using HttpMock;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System.IO;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Service
{
    [TestClass()]
    public class SyncResourceResponsableTests
    {
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static IResourceResponsableRepository _resourceResponsableRepository;
        private static ISyncLogRepository _syncLogRepo;
        private static ILogger<SyncResourceResponsable> _logger;
        private static string _responseApi;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<SyncResourceResponsable>>();
            _logger = loggerMock.Object;
            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");
            _resourceResponsableRepository = new ResourceResponsableRepository(AddDataTruckACLContext._context);
            _syncLogRepo = new SyncLogRepository(AddDataTruckACLContext._context);
        }

        [TestMethod()]
        public void SyncResourceResponsableCreateTest()
        {
            var service = new SyncResourceResponsable(_dBUHResourceRepository, _resourceResponsableRepository, _syncLogRepo, _logger);
            service.Should().NotBeNull();
        }

        [TestMethod()]
        public void SyncResourceResponsableDoWorkTest()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceGetAll.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/GetAll")).Return(_responseApi).OK();

            var service = new SyncResourceResponsable(_dBUHResourceRepository, _resourceResponsableRepository, _syncLogRepo, _logger);

            service.Invoking(async i => await i.DoWork(default))
                .Should().ThrowAsync<TaskCanceledException>();
        }
    }
}
