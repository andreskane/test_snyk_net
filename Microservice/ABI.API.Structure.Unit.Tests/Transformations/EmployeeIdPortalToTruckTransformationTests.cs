using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes;
using ABI.API.Structure.ACL.Truck.Application.Transformations;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.API.Structure.Unit.Tests.Mock;
using HttpMock;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace ABI.API.Structure.ACL.Truck.Transformations.Tests
{
    [TestClass()]
    public class EmployeeIdPortalToTruckTransformationTests
    {

        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static string _responseApi;
        private static IStructureNodeRepository _repositoryStructureNode;
        private static IStructureNodePortalRepository _repositoryStructureNodePortal;



        [ClassInitialize()]
        public static void Setup(TestContext context )
        {
            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");
            _repositoryStructureNodePortal = new StructureNodePortalRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformTestAsync()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetOneNodeDefinitionByIdQuery>(), default))
                .ReturnsAsync(new StructureNodeDefinition());
      

            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("JsonResourse.json"));

            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceAddVacantResource.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Post("/api/Resource/AddVacantResource")).Return(_responseApi).OK();

            var nameTransformation = new EmployeeIdPortalToTruckTransformation(_dBUHResourceRepository, _repositoryStructureNodePortal, mediatrMock.Object);

            nameTransformation.Items = resource;

            var node = new Application.DTO.NodePortalTruckDTO();
            node.RoleId = 1;
            node.EmployeeId = 6188;

            dynamic dyItem = new ExpandoObject();

            dyItem.LevelPortalId = 8;
            dyItem.TypeEmployeeTruck = "V,C";
            dyItem.CompanyId = 1;
            dyItem.Node = node;

            var employeeId = (int?) await nameTransformation.DoTransform(dyItem);

            Assert.AreEqual(200629, employeeId.Value);
        }

        [TestMethod()]
        public async System.Threading.Tasks.Task DoTransformVacanteTestAsync()
        {

            AddDataContext.PrepareFactoryData();

            _repositoryStructureNode = new StructureNodeRepository(AddDataContext._context);

            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.Is<GetOneNodeDefinitionByIdQuery>(w => w.NodeDefinitionId.Equals(300000)), default))
                .ReturnsAsync(new StructureNodeDefinition());


            var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("JsonResourse.json"));

            _responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceAddVacantResource.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Post("/api/Resource/AddVacantResource")).Return(_responseApi).OK();

            var nameTransformation = new EmployeeIdPortalToTruckTransformation(_dBUHResourceRepository, _repositoryStructureNodePortal, mediatrMock.Object);

            nameTransformation.Items = resource;

            var node = new Application.DTO.NodePortalTruckDTO
            {
                RoleId = 1,
                EmployeeId = null, 
                NodeDefinitionId = 300000
            };

            dynamic dyItem = new ExpandoObject();

            dyItem.LevelPortalId = 8;
            dyItem.TypeEmployeeTruck = "V,C";
            dyItem.CompanyId = 1;
            dyItem.Node = node;


            var employeeId = (int?) await nameTransformation.DoTransform(dyItem);

            Assert.AreEqual(999, employeeId.Value);
        }


        [TestMethod()]
        public void DoTransformRolNullVacanteTest()
        {

            //var mediatrMock = new Mock<IMediator>();
            //mediatrMock
            //    .Setup(s => s.Send(It.IsAny<GetOneNodeDefinitionByIdQuery>(), default))
            //    .ReturnsAsync(new StructureNodeDefinition());

            //var resource = FactoryMock.GetMockJson<List<ResourceDTO>>(FactoryMock.GetMockPath("JsonResourse.json"));

            //_responseApi = File.ReadAllText((string.Format("MockFile{0}ResouceAddVacantResource.json", Path.DirectorySeparatorChar)));

            //var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            //_dBUHResourceRepositoryHttp.Stub(x => x.Post("/api/Resource/AddVacantResource")).Return(_responseApi).OK();

            //var nameTransformation = new EmployeeIdPortalToTruckTransformation(_dBUHResourceRepository, _repositoryStructureNode, mediatrMock.Object);

            //nameTransformation.Items = resource;

            //var node = new Application.DTO.NodePortalTruckDTO
            //{
            //    RoleId = null,
            //    EmployeeId = null,
            //    NodeDefinitionId = 2
            //};

            //dynamic dyItem = new ExpandoObject();

            //dyItem.LevelPortalId = 8;
            //dyItem.TypeEmployeeTruck = "V,C";
            //dyItem.CompanyId = 1;
            //dyItem.Node = node;


            //var employeeId = (int?)nameTransformation.DoTransform(dyItem);

            //Assert.IsNotNull(employeeId);
            Assert.Inconclusive();
        }

    }
}