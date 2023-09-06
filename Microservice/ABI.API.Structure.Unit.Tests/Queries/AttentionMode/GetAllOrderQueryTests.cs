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
    public class GetAllOrderQueryTests
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
        public void GetAllOrderQueryTest()
        {
            var result = new GetAllOrderQuery();
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllOrderQueryHandlerTest()
        {
            var command = new GetAllOrderQuery();
            var handler = new GetAllOrderQueryHandler(_repo, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}