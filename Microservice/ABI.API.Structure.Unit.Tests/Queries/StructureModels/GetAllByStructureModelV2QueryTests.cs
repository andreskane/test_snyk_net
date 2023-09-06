using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.StructureModelDefinition;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.StructureModels
{
    [TestClass()]
    public class GetAllByStructureModelV2QueryTests
    {
        private static IMapper _mapper;
        private static IStructureModelDefinitionRepository _repo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureModelDefinitionRepository(AddDataContext._context);
        }

        [TestMethod()]
        public void GetAllByStructureModelV2Test()
        {
            var result = new GetAllByStructureModelV2Query { Id = 1 };
            result.Id.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetAllByStructureModelV2TestAsync()
        {
            var command = new GetAllByStructureModelV2Query { Id = 1};
            var handler = new GetAllByStructureModelV2QueryHandler(_mapper, _repo);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}
