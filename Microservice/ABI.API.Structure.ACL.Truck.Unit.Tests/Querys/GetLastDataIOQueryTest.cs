using ABI.API.Structure.ACL.Truck.Application.Queries;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.Framework.MS.Caching;

using AutoMapper;

using FluentAssertions;

using MediatR;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Querys
{
    [TestClass()]
  public class GetLastDataIOQueryTests
    {
        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private static IPortalService _portalService;
        private static IStructureClientRepository _structureClientRepository;
        private static ICacheStore _cacheStore;
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static IMediator _mediator;
        private static IMapper _mapper;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
            var mediator = new Mock<IMediator>();
            _mediator = mediator.Object;

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            _mapper = mappingConfig.CreateMapper();

            _mapeoTableTruckPortal = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            _portalService = new PortalService(_structureRepo, _nodeRepo, _mediator);
            _structureClientRepository = new StructureClientRepository(AddDataContext._context, _cacheStore);
        }

        [TestMethod()]
        public async Task GetLastDataIOQueryTest()
        {
            var command = new GetLastDataIOQuery { Country = "01AR" };
            var handler = new GetLastDataIOQueryHandler(_mapper, _mapeoTableTruckPortal, _portalService, _structureClientRepository);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}
