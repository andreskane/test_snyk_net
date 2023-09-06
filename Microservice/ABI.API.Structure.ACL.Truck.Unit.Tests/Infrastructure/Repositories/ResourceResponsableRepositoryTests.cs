using System.Threading.Tasks;

using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Infrastructure.Repositories
{
    [TestClass()]
    public class ResourceResponsableRepositoryTests
    {
        private static TruckACLContext _context;
        private static IResourceResponsableRepository _resourceResponsableRepository;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataTruckACLContext._context;
            _resourceResponsableRepository = new ResourceResponsableRepository(_context);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var result = await _resourceResponsableRepository.GetAll();
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var result = await _resourceResponsableRepository.GetById(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            var entity = new ResourceResponsable
            {
                ResourceId = 10,
                TruckId = "1",
                IsVacant = false,
                CountryId = 1,
                VendorCategory = "S"
            };

            await _resourceResponsableRepository.Create(entity);
            entity.ResourceId.Should().Be(10);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var entity = await _resourceResponsableRepository.GetById(1);
            entity.CountryId = 2;
            await _resourceResponsableRepository.Update(entity);
            var result = await _resourceResponsableRepository.GetById(1);
            result.Should().NotBeNull();
            result.CountryId.Should().Be(2);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            var entity = await _resourceResponsableRepository.GetById(2);
            await _resourceResponsableRepository.Delete(entity);
            var result = await _resourceResponsableRepository.GetById(2);
            result.Should().BeNull();
        }
    }
}
