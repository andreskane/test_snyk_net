using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class LevelRepositoryTests
    {
        private static ILevelRepository _repo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repo = new LevelRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task GetAllActiveTest()
        {
            var results = await _repo.GetAllActive(true);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllInactiveTest()
        {
            var results = await _repo.GetAllActive(false);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var result = await _repo.GetById(9999);

            await _repo.DeleteAsync(result);

            result = await _repo.GetById(9999);
            result.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetByNameTest()
        {
            var results = await _repo.GetByName("ZONA");
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var results = await _repo.GetAll();
            results.Should().NotBeNull();
        }
    }
}