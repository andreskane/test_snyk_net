using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using HttpMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class DBUHResourceRepositoryTests2
    {
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static string _responseApi;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {

            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");

        }

        //[TestMethod()]
        //public void DBUHResourceRepositoryCostructorBaseTest()
        //{
        //    var resource = new DBUHResourceRepository("http://localhost:9191");
        //    resource._baseUrl.Should().NotBeNull();
        //}

        //[TestMethod()]
        //public async Task GetAllResourceTest()
        //{
        //    _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceGetAll.json", Path.DirectorySeparatorChar)));

        //    var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
        //    _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/GetAll")).Return(_responseApi).OK();

        //    var results = await _dBUHResourceRepository.GetAllResource();

        //    results.Should().NotBeNull();
        //}

        [TestMethod()]
        public async Task CheckVacantCategoryTestAsync()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceCheckVacantCategory.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();

            var results = await _dBUHResourceRepository.CheckVacantCategory("001", "V");

            results.Should().Be(true);
        }

        //[TestMethod()]
        //public async Task CheckVacantCategoryVCTestAsync()
        //{
        //    _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceCheckVacantCategory.json", Path.DirectorySeparatorChar)));

        //    var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
        //    _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();

        //    var results = await _dBUHResourceRepository.CheckVacantCategory("001", "C,V");

        //    results.Should().Be(true);
        //}
        //[TestMethod()]
        //public async Task CheckVacantCategoryNullTestAsync()
        //{
        //    _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceCheckVacantCategory.json", Path.DirectorySeparatorChar)));

        //    var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
        //    _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();

        //    var results = await _dBUHResourceRepository.CheckVacantCategory("001", "C");

        //    results.Should().Be(false);
        //}
    }
}