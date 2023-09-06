using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Repositories
{
    [TestClass()]
    public class CountryRepositoryTests
    {
        private static ICountryRepository _repo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repo = new CountryRepository(AddDataContext._context);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var results = await _repo.GetAll();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetByNameTest()
        {
            var results = await _repo.GetByName("ARGENTINA");
            results.Should().NotBeNull();
        }
        [TestMethod()]
        public async Task GetByNameNullTest()
        {
            var results = await _repo.GetByName("TEST");
            results.Should().BeNull();
        }
    }
}
