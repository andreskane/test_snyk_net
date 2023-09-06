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
    public class GetAllActiveForSelectQueryTests
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
        public void GetAllActiveForSelectQueryTest()
        {
            var result = new GetAllActiveForSelectQuery();
            result.Active = true;

            result.Should().NotBeNull();
            result.Active.Should().BeTrue();
        }

        [TestMethod()]
        public async Task GetAllActiveForSelectQueryHandlerTest()
        {
            var command = new GetAllActiveForSelectQuery();
            var handler = new GetAllActiveForSelectQueryHandler(_repo, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}