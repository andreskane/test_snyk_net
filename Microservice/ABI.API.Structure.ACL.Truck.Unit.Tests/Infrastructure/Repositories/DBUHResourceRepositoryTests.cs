using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using HttpMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.TruckTests.Infrastructure.Repositories
{
    [TestClass()]
    public class DBUHResourceRepositoryTests
    {
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static string _responseApi;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {

            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");

        }

        [TestMethod()]
        public void DBUHResourceRepositoryTest()
        {
            var result = new DBUHResourceRepository(string.Empty);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DBUHResourceRepositoryArgumentNullExceptionTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await Task.Run(() => new DBUHResourceRepository(null))
            );
        }

        [TestMethod()]
        public async Task GetAllResourceTestAsync()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceGetAll.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/GetAll")).Return(_responseApi).OK();

            var result = await _dBUHResourceRepository.GetAllResource();

            result.Should().HaveCount(10);

        }

        [TestMethod()]
        public async Task AddVacantResourceTestAsync()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceAddVacantResource.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Post("/api/Resource/AddVacantResource")).Return(_responseApi).OK();

            var result = await _dBUHResourceRepository.AddVacantResource(1, "D");

            result.Should().NotBeNull();
        }
    }
}