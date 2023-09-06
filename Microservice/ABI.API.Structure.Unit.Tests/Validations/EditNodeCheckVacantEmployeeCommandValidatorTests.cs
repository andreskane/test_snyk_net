using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;

using FluentAssertions;

using FluentValidation.TestHelper;
using HttpMock;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations.Tests
{
    [TestClass()]
    public class EditNodeCheckVacantEmployeeCommandValidatorTests
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
                .Setup(s => s.Send(It.IsAny<GetOneStructureIdByNodeIdQuery>(), default))
                .ReturnsAsync(new StructureDomain("ARGENTINA", 1, null, DateTimeOffset.MinValue) { 
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        CanBeExportedToTruck = true
                    }
                });
            mediator
                .Setup(s => s.Send(It.Is<GetOneStructureIdByNodeIdQuery>(p => p.NodeId.Equals(100046)), default))
                .ReturnsAsync(new StructureDomain("ARGENTINA", 1, null, DateTimeOffset.MinValue)
                {
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        CanBeExportedToTruck = false
                    }
                });

            _dBUHResourceRepository = new DBUHResourceRepository("http://localhost:9191/");
            _mapeoTableTruckPortal = new MapeoTableTruckPortal(AddDataTruckACLContext._context);
            _mediator = mediator.Object;
            _repositoryStructureNode = new StructureNodeRepository(AddDataContext._context);

        }

        [TestMethod()]
        public async Task EditNodeCheckVacantEmployeeCommandValidatorExceptionDBHRTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, null, _mapeoTableTruckPortal, _mediator, _repositoryStructureNode))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task EditNodeCheckVacantEmployeeCommandValidatorExceptionMappeoTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, null, _mediator, _repositoryStructureNode))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task EditNodeCheckVacantEmployeeCommandValidatorExceptionMediatorTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, null, _repositoryStructureNode))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task EditNodeCheckVacantEmployeeCommandValidatorExceptionStructureNodeTest()
        {
            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public void EditNodeCheckVacantEmployeeCommandValidatorTest()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryTrue.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();


            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var model = new EditNodeCommand(100016, null, 10, "TEST", null, true, null, null, null, null, 1, DateTimeOffset.MinValue, DateTimeOffset.MaxValue.Date);
            var validator = new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator, _repositoryStructureNode);
            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public async Task EditNodeCheckVacantEmployeeNullCommandValidatorTest()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryFalse.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();


            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var model = new EditNodeCommand(100016, null, 10, "TEST", null, true, null, 1, null, null, 1, DateTimeOffset.MinValue, DateTimeOffset.MaxValue.Date);
            var validator = new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator, _repositoryStructureNode);

            await Assert.ThrowsExceptionAsync<CheckVacantEmployeeException>(async () =>
                await validator.TestValidateAsync(model));
        }

        [TestMethod()]
        public void EditNodeCheckVacantRolNullEmployeeNullCommandValidatorTest()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryFalse.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();


            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var model = new EditNodeCommand(100016, null, 10, "TEST", null, true, null, null, null, null, 1, DateTimeOffset.MinValue, DateTimeOffset.MaxValue.Date);
            var validator = new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator, _repositoryStructureNode);
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void EditNodeCheckVacantEmployeeNotNullCommandValidatorTest()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryTrue.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();

            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var model = new EditNodeCommand(100016, null, 10, "TEST", null, true, null, 1, null, 1, 1, DateTimeOffset.MinValue, DateTimeOffset.MaxValue.Date);
            var validator = new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator, _repositoryStructureNode);
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod()]
        public void EditNodeCheckVacantEmployeeNotNullCommandValidatorNoTruckTest()
        {
            _responseApi = File.ReadAllText((string.Format("MockFile{0}CheckVacantCategoryTrue.json", Path.DirectorySeparatorChar)));

            var _dBUHResourceRepositoryHttp = HttpMockRepository.At("http://localhost:9191");
            _dBUHResourceRepositoryHttp.Stub(x => x.Get("/api/Resource/CheckVacantCategory")).Return(_responseApi).OK();

            var mockLogger = new Mock<ILogger<EditNodeCheckVacantEmployeeCommandValidator>>();
            var model = new EditNodeCommand(100046, null, 14, "TEST", null, true, null, 1, null, null, 1, DateTimeOffset.MinValue, DateTimeOffset.MaxValue.Date);
            var validator = new EditNodeCheckVacantEmployeeCommandValidator(mockLogger.Object, _dBUHResourceRepository, _mapeoTableTruckPortal, _mediator, _repositoryStructureNode);
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}