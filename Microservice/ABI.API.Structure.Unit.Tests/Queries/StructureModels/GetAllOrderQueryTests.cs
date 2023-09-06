using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.StructureModels;
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
    public class GetAllOrderQueryTests
    {
        private static IMapper _mapper;
        private static IStructureModelRepository _repo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureModelRepository(AddDataContext._context);
        }

        [TestMethod()]
        public async Task GetAllOrderQueryTestAsync()
        {
            var command = new GetAllOrderQuery();
            var handler = new GetAllOrderQueryHandler(_mapper, _repo);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

    }
}
