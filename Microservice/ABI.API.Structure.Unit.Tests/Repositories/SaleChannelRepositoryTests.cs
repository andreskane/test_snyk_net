using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class SaleChannelRepositoryTests
    {
        private static ISaleChannelRepository _repo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repo = new SaleChannelRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task GetByNameTest()
        {
            var result = await _repo.GetByName("DIRECTA");
            result.Should().NotBeNull();
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
    }
}