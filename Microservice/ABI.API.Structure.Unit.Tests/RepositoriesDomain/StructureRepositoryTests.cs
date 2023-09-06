using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.RepositoriesDomain.Tests
{
    [TestClass()]
    public class StructureRepositoryTests
    {
        private static IStructureRepository _repo;
        private static StructureContext _context;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
            _repo = new StructureRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void CreateTest()
        {
            var result = new StructureDomain(1);
            result.Should().NotBeNull();
            result.AristaFrom.Should().BeEmpty();
            result.AristaTo.Should().BeEmpty();
            result.Node.Should().BeNull();
            result.RootNodeId.Should().BeNull();
            result.StructureModel.Should().BeNull();
            result.StructureModelId.Should().Be(0);
            result.ValidityFrom.Should().BeNull();
            result.Code.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateTwoTest()
        {
            var result = new StructureDomain(1, null, null);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateThreeTest()
        {
            var result = new StructureDomain(null, 1, null, null);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task AddTest()
        {
            var entity = new StructureDomain("TEST", 1, null, null, "ARG_VTA");
            entity.AddCode("ARG_VTA");

            entity.Node = null;
            entity.StructureModel = null;
            var arista = new StructureArista(1, 1, 1, 1, 1, DateTime.UtcNow.Date);
            arista.EditValidityFrom(new DateTime(2020, 1, 1));
            entity.AddStructureAristaItems(arista);
            entity.SetValidityFrom(DateTime.UtcNow.Date);

            _repo.Add(entity);
            await _context.SaveChangesAsync();

            entity.Should().NotBe(0);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            var entity = await _repo.GetAsync(2);

            _repo.Delete(entity);
            await _context.SaveChangesAsync();

            var result = await _repo.GetAsync(entity.Id);
            result.Should().BeNull();
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var results = await _repo.GetAllAsync();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAsyncTest()
        {
            var result = await _repo.GetAsync(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var entity = await _repo.GetAsync(1);

            entity.SetName("MODIFIED");
            await _context.SaveChangesAsync();

            var result = await _repo.GetAsync(1);
            result.Name.Should().Be("MODIFIED");
        }

        [TestMethod()]
        public async Task GetStructureNodeRootAsyncTest()
        {
            var result = await _repo.GetStructureNodeRootAsync(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureDataCompleteAsyncTest()
        {
            var result = await _repo.GetStructureDataCompleteAsync(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureDataCompleteByNameAsyncTest()
        {
            var result = await _repo.GetStructureDataCompleteByNameAsync("NAMETEST");
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodeRootAsyncTest()
        {
            var results = await _repo.GetAllStructureNodeRootAsync(null);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNodeRootAsyncCountryTest()
        {
            var results = await _repo.GetAllStructureNodeRootAsync("AR");
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructuresChangesTrackingTest()
        {
            var results = await _repo.GetAllStructuresChangesTracking();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public void AddMigrationTest()
        {
            var results = _repo.AddMigration(new StructureDomain("TEST", 1, null, null));
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureByCodeAsyncTest()
        {
            var results = await _repo.GetStructureByCodeAsync("ARG_VTA1");
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureByNameAsyncTest()
        {
            var results = await _repo.GetStructureByNameAsync("EXPORTABLETOTRUCK");
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public void SetValidityFromOneTest()
        {
            var result = new StructureDomain(1);
            result.Should().NotBeNull();
            result.SetValidityFrom(null);

            result.ValidityFrom.Should().BeNull();
        }
    }
}