using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass]
    public class AttentionModeRoleRepositoryTests
    {
        private static IAttentionModeRoleRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _repo = new AttentionModeRoleRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task GetAllTest()
        {
            var results = await _repo.GetAll();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllByRoleIdTest()
        {
            var results = await _repo.GetAllByRoleId(1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetRoleByIdTest()
        {
            var result = await _repo.GetRoleById(1);
            result.Should().NotBeNull();
        }
    }
}