using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.RepositoriesDomain.Tests
{
    [TestClass()]
    public class StructureNodeRepositoryTests
    {
        private static IStructureNodeRepository _repo;
        private static StructureContext _context;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            AddDataContext.PrepareFactoryData();

            _context = AddDataContext._context;
            _repo = new StructureNodeRepository(AddDataContext._context);
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
        public async Task ContainsChildNodes_True_Test()
        {
            var results = await _repo.ContainsChildNodes(1, 1);
            results.Should().BeTrue();
        }

        [TestMethod()]
        public async Task ContainsChildNodes_False_Test()
        {
            var results = await _repo.ContainsChildNodes(1, 2);
            results.Should().BeFalse();
        }

        [TestMethod()]
        public async Task GetNodoChildAllByNodeIdAsyncTest()
        {
            var results = await _repo.GetNodoChildAllByNodeIdAsync(1, 1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoOneByNodeCodeAsyncTest()
        {
            var results = await _repo.GetNodoOneByNodeCodeAsync(1, "1", 1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoOneByCodeLevelAsyncTest()
        {
            var results = await _repo.GetNodoOneByCodeLevelAsync(1, "1", 2);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = new StructureNode(1);
            result.Should().NotBeNull();
            result.AristasFrom.Should().BeNull();
            result.AristasTo.Should().BeNull();
            result.Code.Should().BeNull();
            result.Level.Should().BeNull();
            result.LevelId.Should().Be(0);
            result.StructureArista.Should().BeNull();
            result.StructureNodoDefinition.Should().BeNull();
            result.StructureNodoDefinitions.Should().BeNull();
            result.Structures.Should().BeNull();
            result.StructureClientNodes.Should().BeNull();
        }


        [TestMethod()]
        public void CreateTwoTest()
        {
            var result = new StructureNode(null, 1);
            result.EditCode("1");
            result.EditLevel(new Domain.Entities.Level { Id = 1 });
            result.EditStructureArista(new StructureArista(1, 1, 1, 1, 1, DateTime.UtcNow.Date));
            result.EditStructureNodeDefinition(new StructureNodeDefinition(1));

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task AddTest()
        {
            var entity = new StructureNode("C9999", 1);

            var entityDefinition = new StructureNodeDefinition(1, 1, 1, 1, DateTime.UtcNow.Date, "TEST", true);
            entityDefinition.EditValidityFrom(DateTime.UtcNow.Date);
            entityDefinition.EditMotiveStateId(1);

            entity.AddDefinition(entityDefinition);

            _repo.Add(entity);
            await _context.SaveChangesAsync();

            entityDefinition.MotiveStateId.Should().Be(1);
            entity.Id.Should().NotBe(0);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            var entity = await _repo.GetAsync(100006);

            _repo.Delete(entity);
            await _context.SaveChangesAsync();

            var result = await _repo.GetAsync(entity.Id);
            result.Should().BeNull();
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var entity = await _repo.GetAsync(100036);
            entity.EditCode("MODIFIED");

            _repo.Update(entity);
            await _context.SaveChangesAsync();

            var result = await _repo.GetAsync(100036);
            result.Should().NotBeNull();
            result.Code.Should().Be("MODIFIED");
        }

        [TestMethod()]
        public async Task GetNodoDefinitionAsyncTest()
        {
            var result = await _repo.GetNodoDefinitionAsync(1, new DateTime(2020, 10, 8), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionPendingAsyncTest()
        {
            var result = await _repo.GetNodoDefinitionPendingAsync(99998);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionByIdAsyncTest()
        {
            var result = await _repo.GetNodoDefinitionByIdAsync(1, true);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionByIdAsyncNoTrackingTest()
        {
            var result = await _repo.GetNodoDefinitionByIdAsync(1, false);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionValidityByNodeIdNoTrackingAsyncTest()
        {
            var result = await _repo.GetNodoDefinitionValidityByNodeIdNoTrackingAsync(1, new DateTime(2021,08,01));
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionValidityByNodeIdAsyncTest()
        {
            var result = await _repo.GetNodoDefinitionValidityByNodeIdAsync(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionValidityByNodeIdAsyncDroppedTest()
        {
            var result = await _repo.GetNodoDefinitionValidityByNodeIdDroppedAsync(100021, new DateTimeOffset(2021, 5, 5, 0, 0, 0, TimeSpan.FromHours(-3)));
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodoDefinitionValidityByNodeIdAsyncValidityTest()
        {
            var result = await _repo.GetNodoDefinitionValidityByNodeIdAsync(1, DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)));
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllNodoDefinitionByIdAsyncTest()
        {
            var results = await _repo.GetAllNodoDefinitionByIdAsync(1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllNodeDefinitionTest()
        {
            var results = await _repo.GetAllNodeDefinition();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task AddNodoDefinitionTest()
        {
            var entity = new StructureNodeDefinition(99998, null, null, null, null, DateTime.UtcNow.Date, "ADDED", true);
            entity.EditMotiveStateId(1);

            _repo.AddNodoDefinition(entity);
            await _context.SaveChangesAsync();

            var result = await _repo.GetAllNodoDefinitionByIdAsync(entity.Id);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task UpdateNodoDefinitionTest()
        {
            var entity = await _repo.GetNodoDefinitionByIdAsync(99999);
            entity.EditName("MODIFIED");

            _repo.UpdateNodoDefinition(entity);
            await _context.SaveChangesAsync();

            var result = await _repo.GetNodoDefinitionByIdAsync(99999);
            result.Should().NotBeNull();
            result.Name.Should().Be("MODIFIED");
        }

        [TestMethod()]
        public async Task DeleteNodoDefinitionsTest()
        {
            var entity = await _repo.GetNodoDefinitionByIdAsync(100000);

            _repo.DeleteNodoDefinitions(entity);
            await _context.SaveChangesAsync();

            var result = await _repo.GetNodoDefinitionByIdAsync(100000);
            result.Should().BeNull();
        }

        [TestMethod()]
        public void CreateAristaTest()
        {
            var result = new StructureArista(1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateAristaTestTwo()
        {
            var result = new StructureArista(1, 1, 1, 1, 1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateAristaTestThree()
        {
            var result = new StructureArista(1, 1, 1, null, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateAristaTestFour()
        {
            var result = new StructureArista(1, null, 1, null, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void CreateAristaTestFive()
        {
            var result = new StructureArista(null, null, null, null, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task AddAristaTest()
        {
            var entity = new StructureArista(1, 1, 1, 1, 1, DateTime.UtcNow, DateTime.UtcNow.Date.AddDays(1));

            entity.NodeFrom = null;
            entity.NodeTo = null;
            entity.StructureFrom = null;
            entity.StructureTo = null;
            entity.TypeRelationship = null;
            entity.EditValidityFrom(new DateTime(2020, 1, 1));
            entity.EditMotiveStateId(1);
            entity.SetNodeParent(1);

            _repo.AddArista(entity);
            await _context.SaveChangesAsync();

            entity.Id.Should().NotBe(0);
            entity.NodeFrom.Should().NotBeNull();
            entity.NodeIdFrom.Should().Be(1);
            entity.NodeTo.Should().NotBeNull();
            entity.NodeIdTo.Should().Be(1);
            entity.StructureFrom.Should().NotBeNull();
            entity.StructureIdFrom.Should().Be(1);
            entity.StructureTo.Should().NotBeNull();
            entity.StructureIdTo.Should().Be(1);
            entity.TypeRelationship.Should().NotBeNull();
            entity.TypeRelationshipId.Should().Be(1);
            entity.ValidityFrom.Should().Be(new DateTime(2020, 1, 1));
            entity.ValidityTo.Should().Be(DateTime.UtcNow.Date.AddDays(1));
            entity.MotiveStateId.Should().Be(1);
        }

        [TestMethod()]
        public async Task UpdateAristaTest()
        {
            var entity = await _repo.GetArista(1, 1, 372, new DateTime(2020, 10, 8), DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)));

            entity.EditValidityTo(entity.ValidityTo.AddDays(-1));
            _repo.UpdateArista(entity);
            await _context.SaveChangesAsync();

            var result = await _context.StructureAristas.FindAsync(entity.Id);
            result.ValidityTo.Should().Be(DateTimeOffset.MaxValue.ToOffset(TimeSpan.FromHours(-3)).AddDays(-1));
        }

        [TestMethod()]
        public async Task GetAristaPendientTest()
        {
            var result = await _repo.GetAristaPendient(8, 100014, new DateTime(2020, 1, 1));
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllAristaTest()
        {
            var results = await _repo.GetAllArista();
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllAristaByStructureTest()
        {
            var results = await _repo.GetAllAristaByStructure(1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DeleteAristaTest()
        {
            var entity = await _context.StructureAristas.FindAsync(99999);

            _repo.DeleteArista(entity);
            await _context.SaveChangesAsync();

            var result = await _context.StructureAristas.FindAsync(99999);
            result.Should().BeNull();
        }

        [TestMethod()]
        public async Task ExistsInStructuresTest()
        {
            var result = await _repo.ExistsInStructures(1);
            result.Should().BeTrue();
        }

        [TestMethod()]
        public async Task GetAristaPreviousTest()
        {
            var results = await _repo.GetAristaPrevious(1, 372, new DateTime(2020, 10, 8));
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodesAsyncAsyncTest()
        {
            var results = await _repo.GetNodesAsync(1, 5);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetNodesTerritoryAsyncTest()
        {
            var results = await _repo.GetNodesTerritoryAsync(1);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAristaByNodeToTest()
        {
            var results = await _repo.GetAristaByNodeTo(13, 100040, new DateTime(2021, 05, 29), true);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAristaByNodeToNoTrackingTest()
        {
            var results = await _repo.GetAristaByNodeTo(13, 100040, new DateTime(2021, 05, 29), false);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetTerritoriesByZonesEmployeeIdTest()
        {
            var results = await _repo.GetTerritoriesByZonesEmployeeId(1, 105, new DateTime(2021, 1, 1));
            results.Should().NotBeNull();
            results.Should().HaveCount(4);
        }

        [TestMethod()]
        public async Task GetTerritoriesByEmployeeIdTest()
        {
            var results = await _repo.GetTerritoriesByEmployeeId(1, 87, new DateTime(2021, 1, 1));
            results.Should().NotBeNull();
            results.Should().HaveCount(5);
        }

        [TestMethod()]
        public async Task GetAllNodoDefinitionByNodeIdAsyncTest()
        {
            var results = await _repo.GetAllNodoDefinitionByNodeIdAsync(100031);
            results.Should().NotBeNull();
            results.Should().HaveCount(2);
        }
        [TestMethod()]
        public async Task ExistsActiveTerritoryClientByNodeTest()
        {
            var results = await _repo.ExistsActiveTerritoryClientByNode(100035, new DateTime(2021, 6, 4));
            results.Should().BeTrue();
        }
        [TestMethod()]
        public async Task NoExistsActiveTerritoryClientByNodeTest()
        {
            var results = await _repo.ExistsActiveTerritoryClientByNode(100036, new DateTime(2021, 6, 4));
            results.Should().BeFalse();
        }
        [TestMethod()]
        public async Task GetNodosDefinitionByIdsNodoAsyncTest()
        {
            var results = await _repo.GetNodosDefinitionByIdsNodoAsync(new System.Collections.Generic.List<int> { 100034, 100037 }, new DateTime(2021, 5, 1), new DateTime(2021, 7, 1));
            results.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetAllAristaByNodeToTestAsync()
        {
            var results = await _repo.GetAllAristaByNodeTo(2);
            results.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetNodeParentByNodeIdTest()
        {
            var results = await _repo.GetNodeParentByNodeId(9999, 99999, new DateTime(2021,5,1));
            results.Should().BeNull();
        }
    }
}