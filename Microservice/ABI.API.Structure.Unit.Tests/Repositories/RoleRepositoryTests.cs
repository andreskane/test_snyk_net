using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class RoleRepositoryTests
    {
        private static IRoleRepository _repo;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _repo = new RoleRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task GetByIdTest()
        {
            var result = await _repo.GetById(1);
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

        [TestMethod()]
        public async Task UpdateTest()
        {
            var result = await _repo.GetById(1);
            result.Name = "MODIFIED";

            await _repo.Update(result);

            result = await _repo.GetById(1);
            result.Name.Should().Be("MODIFIED");
        }

        [TestMethod()]
        public async Task UpdateWithAttentionModeTest()
        {
            var result = await _repo.GetById(1);
            result.Name = "MODIFIED";
            result.AttentionModeRoles = new List<AttentionModeRole>
            {
                new AttentionModeRole
                {
                    Id = 9999,
                    RoleId = 1,
                    Name = "MODIFIED"
                }
            };

            await _repo.Update(result);

            result = await _repo.GetById(1);
            result.Name.Should().Be("MODIFIED");
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
            var results = await _repo.GetByName("GERENTE");
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllTagTest()
        {
            var results = await _repo.GetAllTag();
            results.Should().NotBeNull();
        }
    }
}