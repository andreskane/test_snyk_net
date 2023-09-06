using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass]
    public class AttentionModeRepositoryTests
    {
        private static IAttentionModeRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _repo = new AttentionModeRepository(AddDataContext._context);
        }

        [TestMethod]
        public async Task GetAllActive()
        {
            var results = await _repo.GetAllActive(true);
            results.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetAllInactive()
        {
            var results = await _repo.GetAllActive(false);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByNameTest()
        {
            var results = await _repo.GetByName("BDR");
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var results = await _repo.GetAll();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var results = await _repo.GetById(1);
            results.Should().NotBeNull();
        }
    }
}
