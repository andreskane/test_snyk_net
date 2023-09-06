using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class StructureModelDefinitionRepositoryTests
    {
        private static IStructureModelDefinitionRepository _repo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repo = new StructureModelDefinitionRepository(AddDataContext._context);
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
            var result = await _repo.GetById(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetLevelByLevelIdTest()
        {
            var result = await _repo.GetLevelByLevelId(10003, 1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllByStructureModelTest()
        {
            var results = await _repo.GetAllByStructureModel(1);
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
    }
}