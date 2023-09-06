using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.StructureClient;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Caching;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures.Tests
{
    [TestClass()]
    public class GetAllClientQueryTests
    {
        private static IMapper _mapper;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
        }

        [TestMethod()]
        public void GetAllClientQueryTest()
        {
            var result = new GetAllClientQuery { StructureCode ="ARG", ValidityFrom = DateTimeOffset.MinValue };
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllOrderQueryHandlerTest()
        {
            var listClients = new List<StructureClientNode>
            {
                new StructureClientNode(1, "ClienteTest", "23", "1", DateTimeOffset.MinValue)
            };

            var mockCacheStore = new Mock<ICacheStore>();
            mockCacheStore.Setup(s => s.Add(listClients, new AllStructureClientNodeCacheKey("1"), "default"));

            var _structureClientRepository = new StructureClientRepository(AddDataContext._context, mockCacheStore.Object);
            var _structureRepository = new StructureRepository(AddDataContext._context);

            var command = new GetAllClientQuery { StructureCode = "ARG_VTA1", ValidityFrom = ToOffsetTest(DateTimeOffset.UtcNow, -3) };

            var handler = new GetAllClientQueryHandler(_structureClientRepository, _structureRepository, _mapper);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllStructureNullOrderQueryHandlerTest()
        {
            var listClients = new List<StructureClientNode>
            {
                new StructureClientNode(1, "ClienteTest", "23", "1", DateTimeOffset.MinValue)
            };

            var mockCacheStore = new Mock<ICacheStore>();
            mockCacheStore.Setup(s => s.Add(listClients, new AllStructureClientNodeCacheKey("1"), "default"));

            var _structureClientRepository = new StructureClientRepository(AddDataContext._context, mockCacheStore.Object);
            var _structureRepository = new StructureRepository(AddDataContext._context);

            var command = new GetAllClientQuery { StructureCode = "ARG_VTANULL", ValidityFrom = ToOffsetTest(DateTimeOffset.UtcNow, -3) };

            var handler = new GetAllClientQueryHandler(_structureClientRepository, _structureRepository, _mapper);
            var result = await handler.Handle(command, default);

            result.Should().BeEmpty();
        }

        public DateTimeOffset ToOffsetTest( DateTimeOffset input, int offset = -3)
        {
            var zone = TimeSpan.FromHours(offset);

            return input.ToOffset(zone);
        }
    }
}