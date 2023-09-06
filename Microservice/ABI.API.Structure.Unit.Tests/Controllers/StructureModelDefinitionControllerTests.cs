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
using C = ABI.API.Structure.Application.Commands.StructureModelDefinition;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class StructureModelDefinitionControllerTests
    {
        private static StructureModelDefinitionController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<StructureModelDefinitionController>>();
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.StructureModelDefinition.GetByIdQuery>(p => p.Id.Equals(1)), default))
                .ReturnsAsync(new Application.DTO.StructureModelDefinitionDTO());
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.StructureModelDefinition.GetAllByStructureModelQuery>(p => p.Id.Equals(1)), default))
                .ReturnsAsync(new List<Application.DTO.StructureModelDefinitionDTO>());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.AddCommand>(p => p.Id != null && p.Id.Equals(2)), default))
                .ThrowsAsync(new ElementExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.UpdateCommand>(p => p.Id != null && p.Id.Equals(2)), default))
                .ThrowsAsync(new ElementExistsException());

            _controller = new StructureModelDefinitionController(loggerMock.Object, mediatrMock.Object);
        }


        [TestMethod()]
        public async Task CreatTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new StructureModelDefinitionController(null, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var result = await _controller.GetAllAsync();
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
        public async Task GetAllByStructureModelAsyncTest()
        {
            var result = await _controller.GetAllByStructureModelAsync(1);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllByStructureModelAsyncNotFoundTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await _controller.GetAllByStructureModelAsync(2)
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
        public async Task CreateDataAsyncElementExistsTest()
        {
            var result = await _controller.CreateDataAsync(new C.AddCommand { Id = 2 });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureModelDefinitionController(null, mediatrMock.Object);
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
            var result = await _controller.UpdateDataAsync(new C.UpdateCommand { Id = 2 });
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncModelInvalidTest()
        {
            var mediatrMock = new Mock<IMediator>();
            var controller = new StructureModelDefinitionController(null, mediatrMock.Object);
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
    }
}