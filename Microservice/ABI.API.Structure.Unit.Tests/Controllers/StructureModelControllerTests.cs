using ABI.API.Structure.Application.Exceptions;
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
using System.Threading.Tasks;
using C = ABI.API.Structure.Application.Commands.StructureModel;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class StructureModelControllerTests
    {
        private static StructureModelController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<StructureModelController>>();
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.StructureModels.GetByIdQuery>(p => p.Id.Equals(1)), default))
                .ReturnsAsync(new Application.DTO.StructureModelDTO());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.AddCommand>(p => p.Name != null && p.Name.Equals("EXISTS")), default))
                .ThrowsAsync(new NameExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.AddCommand>(p => p.Code != null && p.Code.Equals("TEST")), default))
                .ThrowsAsync(new StructureCodeExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.AddCommand>(p => p.Code != null && p.Code.Equals("EXIST")), default))
                .ThrowsAsync(new NodeCodeLengthException(3));
            mediatrMock
                .Setup(s => s.Send(It.Is<C.UpdateCommand>(p => p.Name != null && p.Name.Equals("EXISTS")), default))
                .ThrowsAsync(new NameExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.UpdateCommand>(p => p.Code != null && p.Code.Equals("TEST")), default))
                .ThrowsAsync(new StructureCodeExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.UpdateCommand>(p => p.Code != null && p.Code.Equals("EXISTS")), default))
                .ThrowsAsync(new NodeCodeLengthException(3));

            _controller = new StructureModelController(loggerMock.Object, mediatrMock.Object);
        }


        [TestMethod()]
        public async Task CreatTwoTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new StructureModelController(null, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var result = await _controller.GetAllAsync(true);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllForSelectAsyncTest()
        {
            var result = await _controller.GetAllForSelectAsync(true);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
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
            var result = await _controller.CreateDataAsync(new C.AddCommand());
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNameExistsTest()
        {
            var result = await _controller.CreateDataAsync(new C.AddCommand { Name = "EXISTS" });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task CreateDataAsyncCodeExistsTest()
        {
            var result = await _controller.CreateDataAsync(new C.AddCommand { Code = "TEST" });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task CreateDataAsyncCodeLengthTest()
        {
            var result = await _controller.CreateDataAsync(new C.AddCommand { Code = "EXIST" });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureModelController(null, mediatrMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.CreateDataAsync(new C.AddCommand());
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsync()
        {
            var result = await _controller.UpdateDataAsync(new C.UpdateCommand());
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncNameExistsTest()
        {
            var result = await _controller.UpdateDataAsync(new C.UpdateCommand { Name = "EXISTS" });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncCodeExistsTest()
        {
            var result = await _controller.UpdateDataAsync(new C.UpdateCommand { Code = "TEST" });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }
        [TestMethod()]
        public async Task UpdateDataAsyncCodeLengthTest()
        {
            var result = await _controller.UpdateDataAsync(new C.UpdateCommand { Code = "EXISTS" });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureModelController(null, mediatrMock.Object);
            controller.ModelState.AddModelError("TEST", "TESTMESSAGE");
            var result = await controller.UpdateDataAsync(new C.UpdateCommand());
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
        public async Task GetCountriesTest()
        {
            var result = await _controller.GetCountries();
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }
    }
}