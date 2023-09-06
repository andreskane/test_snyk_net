using ABI.API.Structure.Controllers;
using ABI.Framework.MS.Helpers.Response;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Controllers
{
    [TestClass()]
    public class ImportProcessControllerTests
    {
        private static ImportProcessController _controller;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var loggerMock = new Mock<ILogger<ImportProcessController>>();
            var mediator = new Mock<IMediator>();

            _controller = new ImportProcessController(loggerMock.Object, mediator.Object);
        }

        [TestMethod()]
        public async Task IntegrateTruckToPortalTest()
        {
            var result = await _controller.NotifyEnd(new ACL.Truck.Application.Commands.ImportProcess.IOLoadFinishedCommand { 
                ProccessId = 262,
                RowsCount=0,
                State = ACL.Truck.Domain.Enums.ImportProcessState.Pending
            });
            result.Should().BeOfType(typeof(ActionResult<GenericResponse>));
        }
    }
}
