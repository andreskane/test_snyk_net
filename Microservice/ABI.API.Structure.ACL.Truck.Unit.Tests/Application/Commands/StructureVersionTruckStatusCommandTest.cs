using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Queries.Structure;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.ACL.TruckTests.Inits;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.Application.Infrastructure;
using ABI.Framework.MS.Caching;
using AutoMapper;
using FluentAssertions;
using HttpMock;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class StructureVersionTruckStatusCommandTest
    {

        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private static string _responseApi;
        private static IApiTruck _apiTruck;
        private static IApiTruckUrls _apiUrls;
        private static ITruckService _truckService;
        private static IMediator _mediator;
        private static ICacheStore _cacheStore;

        private static IStructureNodePortalRepository _structureNodePortalRepository;
        private static IVersionedAristaRepository _versionedAristaRepository;
        private static IVersionedNodeRepository _VersionedNodeRepository;
        private static IVersionedRepository _VersionedRepository;
        private static IVersionedLogRepository _VersionedLogRepository;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _mapeoTableTruckPortal = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            _apiUrls = new ApiTruckUrls("http://localhost:9192/", 300000);
            _apiTruck = new ApiTruck(_apiUrls);


            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            Dictionary<string, TimeSpan> cachingConfiguration = new Dictionary<string, TimeSpan>();
            cachingConfiguration.Add("default", new TimeSpan(0, 1, 0, 0));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);


            var mediator = new Mock<IMediator>();

            mediator
                .Setup(s => s.Send(It.Is<GetByIdQuery>(w => w.StructureId.Equals(1)), default))
                .ReturnsAsync(new StructureDTO
                {
                    Id = 1,
                    Name = "ARGENTINA",
                    StructureModelId = 1,
                    Validity = new DateTime(2021, 05, 02),
                    StructureModel = new StructureModelDTO
                    {
                        Active = true,
                        CanBeExportedToTruck = true,
                        Description = "MODELO TEST",
                        Id = 1,
                        Name = "MODELO ARG",
                        ShortName = "MARG"
                    }
                });

            _mediator = mediator.Object;

            _structureNodePortalRepository = new StructureNodePortalRepository(AddDataContext._context);
            _versionedAristaRepository = new VersionedAristaRepository(AddDataTruckACLContext._context);
            _VersionedNodeRepository = new VersionedNodeRepository(AddDataTruckACLContext._context);
            _VersionedRepository = new VersionedRepository(AddDataTruckACLContext._context, _cacheStore);
            _VersionedLogRepository = new VersionedLogRepository(AddDataTruckACLContext._context);

            _truckService = new TruckService(_structureNodePortalRepository, _versionedAristaRepository,
                                            _VersionedNodeRepository, _VersionedRepository,
                                            _VersionedLogRepository);

        }

        [TestMethod()]
        public void StructureVersionTruckStatusCommandsTest()
        {
            var command = new StructureVersionTruckStatusCommand { StructureId = 1, VersionTruck = 1 };

            command.StructureId.Should().Be(1);
            command.VersionTruck.Should().Be(1);
        }


        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerTestAsync()
        {
            _responseApi = File.ReadAllText(string.Format("MockFile{0}Truck-EstructuraVersion.json", Path.DirectorySeparatorChar));
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }

        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerNotExportTruckTestAsync()
        {

            var mediator = new Mock<IMediator>();
            mediator
            .Setup(s => s.Send(It.Is<GetByIdQuery>(w => w.StructureId.Equals(1)), default))
            .ReturnsAsync(new StructureDTO
            {
                Id = 1,
                Name = "ARGENTINA",
                StructureModelId = 1,
                Validity = new DateTime(2021, 05, 02),
                StructureModel = new StructureModelDTO
                {
                    Active = true,
                    CanBeExportedToTruck = false,
                    Description = "MODELO TEST",
                    Id = 1,
                    Name = "MODELO ARG",
                    ShortName = "MARG"
                }
            });

            _mediator = mediator.Object;


            _responseApi = File.ReadAllText(string.Format("MockFile{0}Truck-EstructuraVersion.json", Path.DirectorySeparatorChar));
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }


        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerCompanyUSATestAsync()
        {

            var mediator = new Mock<IMediator>();
            mediator
            .Setup(s => s.Send(It.Is<GetByIdQuery>(w => w.StructureId.Equals(1)), default))
            .ReturnsAsync(new StructureDTO
            {
                Id = 1,
                Name = "USA",
                StructureModelId = 1,
                Validity = new DateTime(2021, 05, 02),
                StructureModel = new StructureModelDTO
                {
                    Active = true,
                    CanBeExportedToTruck = false,
                    Description = "MODELO TEST",
                    Id = 1,
                    Name = "MODELO ARG",
                    ShortName = "MARG"
                }
            });

            _mediator = mediator.Object;


            _responseApi = File.ReadAllText(string.Format("MockFile{0}Truck-EstructuraVersion.json", Path.DirectorySeparatorChar));
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }

        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerTestTruckAsync()
        {
            _responseApi = "{'EstructuraVersiones':{'Level1':[{'ECFecAlt':'2021-05-07','ECFecApr':'2021-05-07','ECFecDes':'2021-05-31','ECFecHas':'2999-12-31','ECFecMod':'','ECFecTra':'2021-05-07','ECHorAlt':'17:23:04','ECHorApr':'17:25:26','ECHorMod':'17:24:03','ECHorTra':'17:25:37','ECIncMsg':'','ECIndTra':'S','ECStsCod':'APR','ECTipCre':'','ECUsuAlt':'Truck','ECUsuApr':'Truck','ECUsuMod':'Truck','ECUsuTra':'Truck','ECVerNro':1524,'EmpId':1}]}}";
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }

        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerTestTruck2Async()
        {
            _responseApi = "{'EstructuraVersiones':{'Level1':[{'ECFecAlt':'2021-05-07','ECFecApr':'2021-05-07','ECFecDes':'2021-05-31','ECFecHas':'2999-12-31','ECFecMod':'','ECFecTra':'2021-05-07','ECHorAlt':'17:23:04','ECHorApr':'17:25:26','ECHorMod':'17:24:03','ECHorTra':'17:25:37','ECIncMsg':'','ECIndTra':'S','ECStsCod':'APR','ECTipCre':'','ECUsuAlt':'Truck','ECUsuApr':'Truck','ECUsuMod':'Truck','ECUsuTra':'Truck','ECVerNro':1524,'EmpId':1},{'ECFecAlt':'2021-05-07','ECFecApr':'2021-05-07','ECFecDes':'2021-05-29','ECFecHas':'2999-12-31','ECFecMod':'','ECFecTra':'2021-05-07','ECHorAlt':'17:23:04','ECHorApr':'17:25:26','ECHorMod':'17:24:03','ECHorTra':'17:25:37','ECIncMsg':'','ECIndTra':'S','ECStsCod':'APR','ECTipCre':'','ECUsuAlt':'Truck','ECUsuApr':'Truck','ECUsuMod':'Truck','ECUsuTra':'Truck','ECVerNro':1523,'EmpId':1}]}}";
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }

        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerTestTruckEditPortalAsync()
        {
            _responseApi = "{'EstructuraVersiones':{'Level1':[{'ECFecAlt':'2021-05-1','ECFecApr':'2021-05-07','ECFecDes':'2021-05-1','ECFecHas':'2999-12-31','ECFecMod':'2021-05-02','ECFecTra':'2021-05-07','ECHorAlt':'17:23:04','ECHorApr':'17:25:26','ECHorMod':'17:24:03','ECHorTra':'17:25:37','ECIncMsg':'','ECIndTra':'S','ECStsCod':'APR','ECTipCre':'','ECUsuAlt':'AR3ROBOT','ECUsuApr':'AR3ROBOT','ECUsuMod':'Truck','ECUsuTra':'Truck','ECVerNro':1524,'EmpId':1}]}}";
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                 .Should().NotThrowAsync();

        }


        [TestMethod()]
        public async Task StructureVersionTruckStatusCommandsHandlerTestTruckNullApiAsync()
        {
            _responseApi = "{'EstructuraVersiones':{'Level1':[]}}";
            var _TruckHttp = HttpMockRepository.At("http://localhost:9192");
            _TruckHttp.Stub(x => x.Post("/EstructuraVersion")).Return(_responseApi).OK();

            var command = new StructureVersionTruckStatusCommand { StructureId = 1 };
            var handler = new StructureVersionTruckStatusCommandHandler(_mediator, _mapeoTableTruckPortal, _truckService, _apiTruck);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync();

        }

    }
}
