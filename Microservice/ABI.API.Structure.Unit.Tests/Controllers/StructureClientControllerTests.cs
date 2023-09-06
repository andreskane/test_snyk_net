using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.StructureClient;
using ABI.API.Structure.Controllers;
using ABI.Framework.MS.Helpers.Response;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Controllers
{
    [TestClass()]
    public class StructureClientControllerTests
    {
        private static StructureClientController _controller;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<StructureClientController>>();
            var mediatrMock = new Mock<IMediator>();

            mediatrMock
                .Setup(s => s.Send(It.Is<GetOneNodeClientQuery>(p => p.NodeId.Equals(1)), default))
                .ReturnsAsync(new System.Collections.Generic.List<StructureClientDTO>() { 
                    new StructureClientDTO
                    {
                        NodeId = 1,
                        ClientId = "1"
                    }
                });

            mediatrMock
                .Setup(s => s.Send(It.Is<GetAllClientQuery>(p => p.StructureCode.Equals("ARG_VTA")), default))
                .ReturnsAsync(new System.Collections.Generic.List<StructureClientDTO>() {
                    new StructureClientDTO
                    {
                        NodeId = 1,
                        ClientId = "1"
                    }
                });

            mediatrMock
                .Setup(s => s.Send(It.Is<GetClientsExcelDataQuery>(p => p.NodeCode.Equals("TEST")), default))
                .ReturnsAsync(new System.IO.MemoryStream ()
                );

            _controller = new StructureClientController(loggerMock.Object, mediatrMock.Object);
        }

        [TestMethod]
        public async Task ControllerArgumentNull()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new StructureClientController(null, null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var results = await _controller.GetAllAsync(1, Convert.ToDateTime("2021-03-03"));
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllClientsAsyncTest()
        {
            var results = await _controller.GetAllClientsAsync("ARG_VTA", Convert.ToDateTime("2021-03-03"));
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetAllActualAsyncTest()
        {
            var results = await _controller.GetAllActualAsync(new ACL.Truck.Application.Queries.GetLastDataIOQuery { Country="AR" });
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task GetAllActualAsyncNullCountryTest()
        {
            var results = await _controller.GetAllActualAsync(new ACL.Truck.Application.Queries.GetLastDataIOQuery { Country = null });
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task GetAllActualAsyncEmptyCountryTest()
        {
            var results = await _controller.GetAllActualAsync(new ACL.Truck.Application.Queries.GetLastDataIOQuery { Country = "" });
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<ActionResult<GenericResponse>>();
        }

        [TestMethod()]
        public async Task GetClientsAsyncTest()
        {
            var results = await _controller.GetClientsAsync(new GetClientsByNodeQuery { LevelId = 8, NodeCode = "TEST", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-03-03") });
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<BaseGenericResponse>();
        }

        [TestMethod()]
        public async Task GetExcelClientsAsyncTest()
        {
            var results = await _controller.GetExcelClientsAsync(new GetClientsExcelDataQuery { LevelId = 8, NodeCode = "TEST", StructureId = 1, ValidityFrom = Convert.ToDateTime("2021-03-03") });
            results.Should().NotBeNull();
            results.Should().BeAssignableTo<FileStreamResult>();
        }
    }
}
