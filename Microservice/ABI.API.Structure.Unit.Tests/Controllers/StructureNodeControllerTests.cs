using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using C = ABI.API.Structure.Application.Commands.StructureNodes;
using Q = ABI.API.Structure.Application.Queries.StructureNodes;
using S = ABI.API.Structure.Application.Queries.Structure;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class StructureNodeControllerTests
    {
        private static StructureNodeController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var structure = new StructureDomain("TEST",1,null, DateTime.UtcNow.Date, "ARG_VTA1");
            structure.AddId(2);


        var loggerMock = new Mock<ILogger<StructureNodeController>>();
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetOneNodeByIdQuery>(p => p.NodeId.Equals(1)), default))
                .ReturnsAsync(new Application.DTO.NodeDTO());

            mediatrMock
              .Setup(s => s.Send(It.Is<S.GetAllByCodeQuery>(s=>s.Code.Equals("ARG_VTA1")), default))
              .ReturnsAsync(structure);

            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetOneNodeByIdQuery>(p => p.StructureId.Equals(1) && p.NodeId.Equals(2)), default))
                .ThrowsAsync(new NotFoundException());
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetAllStructureQuery>(p => p.StructureId.Equals(2)), default))
                .ThrowsAsync(new NotFoundException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.ValidateNodeCodeCommand>(p => p.LevelId.Equals(1) && p.Code.Equals("2")), default))
                .ThrowsAsync(new NodeCodeExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.ValidateTruckIllegalCharacterCommand>(p => p.Name.Equals("ÁÉÍÓ")), default))
                .ThrowsAsync(new TruckInvalidCharacterException("Name", 'Á'));
            mediatrMock
                .Setup(s => s.Send(It.Is<C.ValidateNodeCodeLengthCommand>(p => p.Code.Equals("NOTNUMERIC")), default))
                .ThrowsAsync(new NodeCodeNotNumericException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.ValidateNodeCodeLengthCommand>(p => p.Code.Equals("EXCEEDLENGTH")), default))
                .ThrowsAsync(new NodeCodeLengthException(1));
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateNodeCommand>(p => p.Code.Equals("VACANT")), default))
                .ThrowsAsync(new CheckVacantEmployeeException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateNodeCommand>(p => p.Code.Equals("RESPONSABLE")), default))
                .ThrowsAsync(new NodeResponsableException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateNodeCommand>(p => p.Code.Equals("NORESPONSABLE")), default))
                .ThrowsAsync(new NodeNoResponsableException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.DeleteNodeCommand>(p => p.Id.Equals(2)), default))
                .ThrowsAsync(new ContainsChildNodesException());
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetOneNodeVersionByIdQuery>(p => p.StructureId.Equals(2)), default))
                .ThrowsAsync(new NotFoundException());
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetAllScheduledChangesQuery>(p => p.Id.Equals(2)), default))
                .ThrowsAsync(new NotFoundException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Id.Equals(2)), default))
                .ThrowsAsync(new ChildNodesActiveException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Id.Equals(3)), default))
                .ThrowsAsync(new ParentNodesActiveException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Id.Equals(4)), default))
                .ThrowsAsync(new TruckInvalidCharacterException("TEST", 'Á'));
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("VACANT")), default))
                .ThrowsAsync(new CheckVacantEmployeeException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("RESPONSABLE")), default))
                .ThrowsAsync(new NodeResponsableException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("NORESPONSABLE")), default))
                .ThrowsAsync(new NodeNoResponsableException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("RESPONSABLEZONE")), default))
                .ThrowsAsync(new NodeEmployeeResponsableZonesException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("NORESPONSABLEZONE")), default))
                .ThrowsAsync(new NodeEmployeeNoResponsableZonesException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("RESPONSABLETERRITORY")), default))
                .ThrowsAsync(new NodeEmployeeResponsableTerritoriesException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditNodeCommand>(p => p.Code.Equals("NORESPONSABLETERRITORY")), default))
                .ThrowsAsync(new NodeEmployeeNoResponsableTerritoriesException());

            _controller = new StructureNodeController(mediatrMock.Object);
        }


        [TestMethod()]
        public async Task CreatTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new StructureNodeController(null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }


        [TestMethod()]
        public async Task GetAllByStructureIdAsyncTest()
        {
            var result = await _controller.GetAllByStructureIdAsync(1, null);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByStructureIdAsyncNotFoundTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                Task.Run(() => _controller.GetAllByStructureIdAsync(2, null))
            );

            throws.Should().BeOfType(typeof(NotFoundException));
        }

        [TestMethod()]
        public async Task GetOneByStructureAsyncTest()
        {
            var result = await _controller.GetOneByStructureAsync(1, 1, DateTimeOffset.MinValue);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetOneByStructureAsyncNotFoundTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                Task.Run(() => _controller.GetOneByStructureAsync(2, 1, DateTimeOffset.MinValue))
            );

            throws.Should().BeOfType(typeof(NotFoundException));
        }

        [TestMethod()]
        public async Task CreateDataAsyncTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "1", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNameInvalidCharacterTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "ÁÉÍÓ", "2", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNodeCodeExistsTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "2", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncCodeNotNumericTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "NOTNUMERIC", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncCodeLengthExceededTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "EXCEEDLENGTH", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncCheckVacantEmployeeTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "VACANT", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncResponsableTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "RESPONSABLE", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNoResponsableTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "NORESPONSABLE", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncResponsableZoneTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "RESPONSABLEZONE", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNoResponsableZoneTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "NORESPONSABLEZONE", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncResponsableTerritoryTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "RESPONSABLETERRITORY", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNoResponsableTerritoryTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "NORESPONSABLETERRITORY", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureNodeController(mediatrMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.CreateDataAsync(new C.CreateNodeCommand(1, 1, "TEST", "1", 1, true, 1, 1, 1, 1, DateTime.UtcNow.Date, false, null));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(1, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncChildNodesActiveTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(2, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncParentNodesActiveExceptionTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(3, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncTruckInvalidCharacterExceptionTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncCheckVacantExceptionTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "VACANT", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncResponsableExceptionTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "RESPONSABLE", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncNoResponsableExceptionTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "NORESPONSABLE", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }


        [TestMethod()]
        public async Task UpdateDataAsyncResponsableZoneTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "RESPONSABLEZONE", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncNoResponsableZoneTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "NORESPONSABLEZONE", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncResponsableTerritoryTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "RESPONSABLETERRITORY", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncNoResponsableTerritoryTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditNodeCommand(4, 1, 1, "NORESPONSABLETERRITORY", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureNodeController(mediatrMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.UpdateDataAsync(new C.EditNodeCommand(1, 1, 1, "TEST", "1", true, 1, 1, 1, 1, 1, DateTime.UtcNow.Date, DateTime.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task DeleteDataAsyncTest()
        {
            var result = await _controller.DeleteDataAsync(new C.DeleteNodeCommand(1, 1, 1, DateTime.UtcNow.Date, DateTimeOffset.MaxValue));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task DeleteDataAsyncContainsChildNodesExceptionTest()
        {
            var result = await _controller.DeleteDataAsync(new C.DeleteNodeCommand(2, 1, 1, DateTime.UtcNow.Date, DateTimeOffset.MaxValue));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task DeleteChangesWithoutSavingAsyncTest()
        {
            var result = await _controller.DeleteChangesWithoutSavingAsync(1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task DeleteNodeDraftDataAsyncTest()
        {
            var result = await _controller.DeleteNodeDraftDataAsync(new C.DeleteNodeDraftCommand(1, 1,DateTimeOffset.MinValue.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<BaseGenericResponse>>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureAsyncTest()
        {
            var result = await _controller.GetAllByScheduledStructureAsync(2,null, DateTime.UtcNow.Date, null);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureAsyncCodeTest()
        {
            var result = await _controller.GetAllByScheduledStructureAsync(null, "ARG_VTA1", DateTime.UtcNow.Date, null);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureAsyncCodeNullTest()
        {
            var result = await _controller.GetAllByScheduledStructureAsync(null, "", DateTime.UtcNow.Date, null);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetChangesWithoutSavingAsyncTest()
        {
            var result = await _controller.GetChangesWithoutSavingAsync(1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetChangesWithoutSavingByStructureIdAsyncTest()
        {
            var result = await _controller.GetChangesWithoutSavingByStructureIdAsync(1);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetCodeHintTest()
        {
            var result = await _controller.GetCodeHint(1, 1);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetEmployeeIdsNodesByStructureTest()
        {
            var result = await _controller.GetEmployeeIdsNodesByStructure(1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetOneVersionByStructureAsyncTest()
        {
            var result = await _controller.GetOneVersionByStructureAsync(1, 1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetOneVersionByStructureAsyncNotFoundTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                Task.Run(() => _controller.GetOneVersionByStructureAsync(1, 2, DateTime.UtcNow.Date))
            );

            throws.Should().BeOfType(typeof(NotFoundException));
        }

        [TestMethod()]
        public async Task GetScheduledDatesByStructureAsyncTest()
        {
            var result = await _controller.GetScheduledDatesByStructureAsync(1);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetScheduledDatesByStructureAsyncNotFoundTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                Task.Run(() => _controller.GetScheduledDatesByStructureAsync(2))
            );

            throws.Should().BeOfType(typeof(NotFoundException));
        }

        [TestMethod()]
        public async Task SaveChangesWithoutSavingAsyncTest()
        {
            var result = await _controller.SaveChangesWithoutSavingAsync(new C.SaveChangesWithoutSavingCommand(1, DateTime.UtcNow));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task SaveChangesWithoutSavingAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureNodeController(mediatrMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.SaveChangesWithoutSavingAsync(new C.SaveChangesWithoutSavingCommand(1, DateTime.UtcNow));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task ValidateChangesAsyncTest()
        {
            var result = await _controller.ValidateChangesAsync(1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<object>();
        }

        [TestMethod()]
        public async Task ValidateChangesAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureNodeController(mediatrMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.ValidateChangesAsync(1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllSameLevelNodesByNode()
        {
            var result = await _controller.GetAllSameLevelNodesByNode(15, Convert.ToDateTime("2020-10-08"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetBoundedStructureFromNodeIdTest()
        {
            var result = await _controller.GetBoundedStructureFromNodeId(1, "01AR", 1, 5, Convert.ToDateTime("2021-03-03"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetRepeatedEmployeeIdsNodesByStructureTest()
        {
            var result = await _controller.GetRepeatedEmployeeIdsNodesByStructure(1, Convert.ToDateTime("2021-03-03"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureV2AsyncTest()
        {
            var result = await _controller.GetAllByScheduledStructureV2Async(1, "VTA", Convert.ToDateTime("2020-01-01"),true);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureV2AsyncTwoTest()
        {
            var result = await _controller.GetAllByScheduledStructureV2Async(null, "", Convert.ToDateTime("2020-01-01"), true);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByScheduledStructureV2AsyncThreeTest()
        {
            var result = await _controller.GetAllByScheduledStructureV2Async(null, "ARG_VTA1", Convert.ToDateTime("2020-01-01"), true);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllBranchByNodeIdQueryTest()
        {
            var result = await _controller.GetStructureBranchByNodeId(1, 1, DateTime.UtcNow.Date);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }
    }
}