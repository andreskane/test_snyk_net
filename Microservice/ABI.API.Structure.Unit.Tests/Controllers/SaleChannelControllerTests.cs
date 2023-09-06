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
using C = ABI.API.Structure.Application.Commands.SaleChannel;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers.Tests
{
    [TestClass()]
    public class SaleChannelControllerTests
    {
        private static SaleChannelController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<SaleChannelController>>();
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.Is<Q.SaleChannel.GetByIdQuery>(p => p.Id.Equals(1)), default))
                .ReturnsAsync(new Application.DTO.SaleChannelDTO());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.AddCommand>(p => p.Name != null && p.Name.Equals("EXISTS")), default))
                .ThrowsAsync(new NameExistsException());
            mediatrMock
                .Setup(s => s.Send(It.Is<C.UpdateCommand>(p => p.Name != null && p.Name.Equals("EXISTS")), default))
                .ThrowsAsync(new NameExistsException());

            _controller = new SaleChannelController(loggerMock.Object, mediatrMock.Object);
        }


        [TestMethod()]
        public async Task CreatTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new SaleChannelController(null, null))
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
            result.Should().BeAssignableTo<ActionResult<BaseGenericResponse>>();
        }

        [TestMethod()]
        public async Task CreateDataAsyncNameExistsTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NameExistsException>(() =>
                Task.Run(() => _controller.CreateDataAsync(new C.AddCommand { Name = "EXISTS" }))
            );

            throws.Should().BeOfType(typeof(NameExistsException));
        }

        [TestMethod()]
        public async Task UpdateDataAsync()
        {
            var result = await _controller.UpdateDataAsync(new C.UpdateCommand());
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<BaseGenericResponse>>();
        }

        [TestMethod()]
        public async Task UpdateDataAsyncNameExistsTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<NameExistsException>(() =>
                Task.Run(() => _controller.UpdateDataAsync(new C.UpdateCommand { Name = "EXISTS" }))
            );

            throws.Should().BeOfType(typeof(NameExistsException));
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