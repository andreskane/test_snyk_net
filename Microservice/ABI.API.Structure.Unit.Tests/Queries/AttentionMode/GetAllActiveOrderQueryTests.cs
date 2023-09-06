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
    public class GetAllActiveOrderQueryTests
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
        public void GetAllActiveOrderQueryTest()
        {
            var result = new GetAllActiveOrderQuery();
            result.Active = true;

            result.Should().NotBeNull();
            result.Active.Should().BeTrue();
        }

        [TestMethod()]
        public async Task GetAllActiveOrderQueryHandlerTest()
        {
            var command = new GetAllActiveOrderQuery();
            var handler = new GetAllActiveOrderQueryHandler(_repo, _mapper);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}