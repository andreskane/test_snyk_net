using ABI.API.Structure.ACL.Truck.Application.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedCancelChangeCommandTests
    {
        [TestMethod()]
        public void VersionedCancelChangeCommandTest()
        {
            var result = new VersionedCancelChangeCommand();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.UtcNow.Date;
            result.Username = "TEST";

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
            result.Username.Should().Be("TEST");
        }

        [TestMethod()]
        public async Task VersionedCancelChangeCommandHandlerCanceledFalseTestAsync()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(s => s.Send(It.IsAny<VersionedCancelAllChangesCommand>(), default))
                .ReturnsAsync(false);

            var command = new VersionedCancelChangeCommand
            {
                StructureId = 1,
                ValidityFrom = DateTimeOffset.UtcNow,
                Username = "TEST"
            };
            var handler = new VersionedCancelChangeCommandHandler(mockMediator.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task VersionedCancelChangeCommandHandlerTestAsync()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(s => s.Send(It.IsAny<VersionedCancelAllChangesCommand>(), default))
                .ReturnsAsync(true);

            var command = new VersionedCancelChangeCommand
            {
                StructureId = 1,
                ValidityFrom = DateTimeOffset.UtcNow,
                Username = "TEST"
            };
            var handler = new VersionedCancelChangeCommandHandler(mockMediator.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }
    }
}