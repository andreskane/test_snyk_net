using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionMode.Tests
{
    [TestClass()]
    public class GetAllForSelectQueryTests
    {
        private static IMapper _mapper;
        private static IAttentionModeRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
            _repo = new AttentionModeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void GetAllForSelectQueryTest()
        {
            var result = new GetAllForSelectQuery();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllActiveForSelectQueryHandlerTest()
        {
            var command = new GetAllForSelectQuery();
            var handler = new GetAllForSelectQueryHandler(_repo, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}