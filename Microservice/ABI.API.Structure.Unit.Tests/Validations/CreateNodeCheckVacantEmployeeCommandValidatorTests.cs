using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class CreateNodeCheckVacantEmployeeCommandValidatorTests
    {
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private static IMediator _mediator;
        private static IStructureNodeRepository _repositoryStructureNode;

        private static string _responseApi;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain("ARGENTINA", 1, null, DateTimeOffset.MinValue));

            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");
            _mapeoTableTruckPortal = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            _mediator = mediator.Object;
            _repositoryStructureNode = new StructureNodeRepository(AddDataContext._context);
        }

        [TestMethod()]
        public void CreateNodeCheckVacantEmployeeCommandValidatorTest()
        {
            //_responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryTrue.json", Path.DirectorySeparatorChar)));

            //var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            //_dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();


            //var mockLogger = new Mock<ILogger<CreateNodeCheckVacantEmployeeCommandValidator>>();
            //var model = new CreateNodeCommand(1, null, "TEST", null, 1, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            //var validator = new CreateNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator);
            //var result = validator.TestValidate(model);

            //result.ShouldNotHaveAnyValidationErrors();
            Assert.Inconclusive();
        }


        [TestMethod()]
        public async Task EditNodeCheckVacantEmployeeNullCommandValidatorTest()
        {
            //_responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryFalse.json", Path.DirectorySeparatorChar)));

            //var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            //_dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();


            //var mockLogger = new Mock<ILogger<CreateNodeCheckVacantEmployeeCommandValidator>>();
            //var model = new CreateNodeCommand(1, null, "TEST", null, 1, true, null, 1, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            //var validator = new CreateNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator);

            //await Assert.ThrowsExceptionAsync<CheckVacantEmployeeException>(async () =>
            //    await validator.TestValidateAsync(model));
            Assert.Inconclusive();
        }

        [TestMethod()]
        public async Task EditNodeCheckVacantRolNullEmployeeNullCommandValidatorTest()
        {
            //_responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryFalse.json", Path.DirectorySeparatorChar)));

            //var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            //_dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();


            //var mockLogger = new Mock<ILogger<CreateNodeCheckVacantEmployeeCommandValidator>>();
            //var model = new CreateNodeCommand(1, null, "TEST", null, 1, true, null, null, null, null, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            //var validator = new CreateNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator);

            //var result = validator.TestValidate(model);
            //result.ShouldNotHaveAnyValidationErrors();
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void EditNodeCheckVacantEmployeeNotNullCommandValidatorTest()
        {
            //_responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryTrue.json", Path.DirectorySeparatorChar)));

            //var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            //_dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();

            //var mockLogger = new Mock<ILogger<CreateNodeCheckVacantEmployeeCommandValidator>>();
            //var model = new CreateNodeCommand(1, null, "TEST", null, 1, true, 1, 1, null, 1, DateTimeOffset.MinValue, false, DateTimeOffset.MaxValue.Date);
            //var validator = new CreateNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator);
            //var result = validator.TestValidate(model);
            //result.ShouldNotHaveAnyValidationErrors();
            Assert.Inconclusive();
        }
    }
}