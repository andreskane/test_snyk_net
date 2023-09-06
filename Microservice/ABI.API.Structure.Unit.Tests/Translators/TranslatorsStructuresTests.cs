using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.API.Structure.Unit.Tests.Mock;
using HttpMock;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class TranslatorsStructuresTests
    {
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static string _responseApi;
        private static IStructureNodeRepository _repositoryStructureNode;
        private static  IStructureNodePortalRepository _repositoryStructureNodePortal;
        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;

        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");
            _repositoryStructureNode = new StructureNodeRepository(AddDataContext._context);
            _mapeoTableTruckPortal = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            _repositoryStructureNodePortal = new StructureNodePortalRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async System.Threading.Tasks.Task NodeTruckToPortalTestAsync()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetOneNodeDefinitionByIdQuery>(), default))
                .ReturnsAsync(new StructureNodeDefinition());

            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceGetAll.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/GetAll")).Return(_responseApi).OK();

            var truck = FactoryMock.GetMockJson<TruckStructure>(FactoryMock.GetMockPath("JsonTruckStructure.json"));
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("JsonResourse.json"));

            AddDataContext.PrepareFactoryData();
            AddDataTruckACLContext.PrepareFactoryData();

            var translator = new TranslatorsStructuresTruckToPortal( _mapeoTableTruckPortal);
            var structurePortal = await translator.TruckToPortal(truck, "ARGENTINA", resource);

            Assert.IsNotNull(structurePortal);
        }

        [TestMethod()]
        public async System.Threading.Tasks.Task PortalToTruckTestAsync()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                  .Setup(s => s.Send(It.Is<GetOneNodeDefinitionByIdQuery>(w => w.NodeDefinitionId.Equals(300000)), default))
                .ReturnsAsync(new StructureNodeDefinition());

            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceGetAll.json", Path.DirectorySeparatorChar)));
            var _responseApiAdd = File.ReadAllText((string.Format("MockFile{0}ResouceAddVacantResource.json", Path.DirectorySeparatorChar)));


            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/GetAll")).Return(_responseApi).OK();
            _dBUHResourceRepositoryHttp.Stub(x => x.Post("/api/Resource/AddVacantResource")).Return(_responseApiAdd).OK();

            var portal = FactoryMock.GetMockJson<List<NodePortalTruckDTO>>(FactoryMock.GetMockPath("JsonNodePortalTruck-RestoNodos.json"));
            var opecpiniOut = FactoryMock.GetMockJson<OpecpiniOut>(FactoryMock.GetMockPath("Truck-OpecpiniNew.json"));
            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("JsonResourse.json"));

            var structureId = 1;

            AddDataContext.PrepareFactoryData();
            AddDataTruckACLContext.PrepareFactoryData();

            var translator = new TranslatorsStructuresPortalToTruck(_dBUHResourceRepository, _repositoryStructureNode, mediatrMock.Object, _repositoryStructureNodePortal, _mapeoTableTruckPortal);
            var structureTruck = await translator.PortalToTruckAsync(opecpiniOut, structureId, portal, resource);

            Assert.IsNotNull(structureTruck);
            Assert.AreEqual(2, structureTruck.Ptecarea.Tecarea.Count);
            Assert.AreEqual(0, structureTruck.Ptecdire.Tecdire.Count);
            Assert.AreEqual(5, structureTruck.Ptecgere.Tecgere.Count);
            Assert.AreEqual(5, structureTruck.Ptecregi.Tecregi.Count);
            Assert.AreEqual(5, structureTruck.Ptecterr.Tecterr.Count);
            Assert.AreEqual(0, structureTruck.Pteczoco.Teczoco.Count);
            Assert.AreEqual(0, structureTruck.Pteczona.Teczona.Count);
        }
    }
}