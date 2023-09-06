using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Exceptions;
using ABI.Framework.MS.Domain.Exceptions;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using ABI.Framework.MS.Service.Exceptions;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C = ABI.API.Structure.Application.Commands.Structures;
using Q = ABI.API.Structure.Application.Queries.Structures;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class StructureControllerTests
    {
        private static StructureController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<StructureController>>();
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetByIdQuery>(p => p.Id.Equals(1)), default))
                .ReturnsAsync(new StructureDTO());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.ValidateValidityCommand>(p => p.StructureId.Equals(2)), default))
                .ThrowsAsync(new DateGreaterThanTodayException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateStructureCommand>(p => p.Name.Equals("NODEROOTA")), default))
                .ThrowsAsync(new NameExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateStructureCommand>(p => p.Code.Equals("ARG_VTA_NODEROOTA")), default))
                .ThrowsAsync(new StructureCodeExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateStructureCommandV2>(p => p.Name.Equals("NODEROOTA")), default))
                .ThrowsAsync(new NameExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateStructureCommandV2>(p => p.Code.Equals("ARG_VTA_NODEROOTA")), default))
                .ThrowsAsync(new StructureCodeExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.CreateStructureCommandV2>(p => p.Code.Equals("ARG_VTA_NODEROOTAABC")), default))
                .ThrowsAsync(new NodeCodeLengthException(20));
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditStructureCommand>(p => p.Name.Equals("NODEROOTA")), default))
                .ThrowsAsync(new NameExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditStructureCommand>(p => p.Code.Equals("ARG_VTA_NODEROOTA")), default))
                .ThrowsAsync(new StructureCodeExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.EditStructureCommand>(p => p.Code.Equals("ARG_VTA_NODEROOTAABC")), default))
                .ThrowsAsync(new NodeCodeLengthException(20));

            mediatrMock
                .Setup(s => s.Send(It.Is<Q.GetMostVisitedFiltersQuery>(p => p.StructureCode.Equals("ARG_VTA_TEST")), default))
                .ReturnsAsync(new List<MostVisitedFilterDto>());

            mediatrMock
                .Setup(s => s.Send(It.Is<C.SaveMostVisitedFiltersCommand>(p => p.StructureCode.Equals("TEST")), default))
                .ReturnsAsync(MediatR.Unit.Value);

            _controller = new StructureController(mediatrMock.Object, loggerMock.Object);
        }


        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var results = await _controller.GetAllAsync(null);
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetOneAsyncTest()
        {
            var result = await _controller.GetOneAsync(1);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetOneAsyncNotFoundTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                Task.Run(() => _controller.GetOneAsync(2))
            );

            throws.Should().BeOfType(typeof(NotFoundException));
        }

        [TestMethod()]
        public async Task CreateDataAsyncTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateStructureCommand("", 1, null, null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataNameExistsExceptionAsyncTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateStructureCommand("NODEROOTA", 1, null, null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataStructureCodeExistsExceptionAsyncTest()
        {
            var result = await _controller.CreateDataAsync(new C.CreateStructureCommand("", 1, null, null, "ARG_VTA_NODEROOTA"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataV2AsyncTest()
        {
            var result = await _controller.CreateDataV2Async(new C.CreateStructureCommandV2("", 1, null, null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataV2NameExistsExceptionAsyncTest()
        {
            var result = await _controller.CreateDataV2Async(new C.CreateStructureCommandV2("NODEROOTA", 1, null, null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataV2StructureCodeExistsExceptionAsyncTest()
        {
            var result = await _controller.CreateDataV2Async(new C.CreateStructureCommandV2("", 1, null, null, "ARG_VTA_NODEROOTA"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task CreateDataV2StructureAsyncCodeLengthTest()
        {
            var result = await _controller.CreateDataV2Async(new C.CreateStructureCommandV2("", 1, null, null, "ARG_VTA_NODEROOTAABC"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncModelInvalidTest()
        {
            var loggerMock = new Mock<ILogger<StructureController>>();
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureController(mediatrMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.CreateDataAsync(new C.CreateStructureCommand("", 1, null, null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataV2AsyncModelInvalidTest()
        {
            var loggerMock = new Mock<ILogger<StructureController>>();
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureController(mediatrMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.CreateDataV2Async(new C.CreateStructureCommandV2("", 1, null, null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditStructureCommand(1, "", null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncModelInvalidTest()
        {
            var loggerMock = new Mock<ILogger<StructureController>>();
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureController(mediatrMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.UpdateDataAsync(new C.EditStructureCommand(1, "", null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task UpdateDataNameExistsAsyncTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditStructureCommand(1, "NODEROOTA", null, ""));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task UpdateDataCodeExistsAsyncTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditStructureCommand(1, "", null, "ARG_VTA_NODEROOTA"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task UpdateDataCodeLengthAsyncTest()
        {
            var result = await _controller.UpdateDataAsync(new C.EditStructureCommand(1, "", null, "ARG_VTA_NODEROOTAABC"));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task DeleteDataAsyncTest()
        {
            var result = await _controller.DeleteDataAsync(1);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task ValidateValidityTest()
        {
            var result = await _controller.ValidateValidity(new C.ValidateValidityCommand(1, DateTimeOffset.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<object>();
        }

        [TestMethod()]
        public async Task ValidateValidityDateGreaterThanTodayTest()
        {
            var result = await _controller.ValidateValidity(new C.ValidateValidityCommand(2, DateTimeOffset.UtcNow.Date));
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<object>();
        }

        [TestMethod()]
        public async Task GetMasterTest()
        {
            var result = await _controller.GetMasters();
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<object>();
        }

        [TestMethod()]
        public async Task GetMostVisitedFiltersAsyncTest()
        {
            var result = await _controller.GetMostVisitedFiltersAsync(new Q.GetMostVisitedFiltersQuery { 
                StructureCode = "ARG_VTA_TEST"
            });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<object>();
        }

        [TestMethod()]
        public async Task SaveMostVisitedFiltersTest()
        {
            var result = await _controller.SaveMostVisitedFilters(new C.SaveMostVisitedFiltersCommand
            {
                StructureCode ="TEST",
                FilterType = 1,
                Name ="TEST",
                Value = 1
            });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<object>();
        }
    }
}