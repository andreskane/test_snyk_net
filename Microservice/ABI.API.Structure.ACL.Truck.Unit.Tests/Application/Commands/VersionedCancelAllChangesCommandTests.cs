using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Commands
{
    [TestClass()]
    public class VersionedCancelAllChangesCommandTests
    {
        [TestMethod()]
        public void VersionedCancelAllChangesCommandTest()
        {
            var result = new VersionedCancelAllChangesCommand();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.UtcNow.Date;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.UtcNow.Date);
        }

        [TestMethod()]
        public async Task VersionedCancelAllChangesCommandHandlerVersionNullTestAsync()
        {
            var mockMediator = new Mock<IMediator>();
            var mockVersionedRepo = new Mock<IVersionedRepository>();
            var mockTruckService = new Mock<ITruckService>();
            var command = new VersionedCancelAllChangesCommand();
            var handler = new VersionedCancelAllChangesCommandHandler(
                mockMediator.Object,
                mockVersionedRepo.Object,
                mockTruckService.Object
            );
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }

        [TestMethod()]
        public async Task VersionedCancelAllChangesCommandHandlerVersionPendingTestAsync()
        {
            var mockMediator = new Mock<IMediator>();
            var mockVersionedRepo = new Mock<IVersionedRepository>();
            mockVersionedRepo
                .Setup(s => s.Filter(It.IsAny<Expression<Func<Domain.Entities.Versioned, bool>>>()))
                .Returns(new List<Domain.Entities.Versioned>
                    {
                        new Domain.Entities.Versioned()
                    }.AsQueryable()
                );

            var mockTruckService = new Mock<ITruckService>();
            var command = new VersionedCancelAllChangesCommand();
            var handler = new VersionedCancelAllChangesCommandHandler(
                mockMediator.Object,
                mockVersionedRepo.Object,
                mockTruckService.Object
            );
            var result = await handler.Handle(command, default);

            result.Should().BeTrue();
        }

        [TestMethod()]
        public async Task VersionedCancelAllChangesCommandHandlerThrowsTestAsync()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(s => s.Send(It.IsAny<GetOneCompanyTruckQuery>(), default))
                .ThrowsAsync(new Exception());

            var mockVersionedRepo = new Mock<IVersionedRepository>();
            mockVersionedRepo
                .Setup(s => s.Filter(It.IsAny<Expression<Func<Domain.Entities.Versioned, bool>>>()))
                .Returns(new List<Domain.Entities.Versioned>
                    {
                        new Domain.Entities.Versioned()
                    }.AsQueryable()
                );

            var mockTruckService = new Mock<ITruckService>();
            var command = new VersionedCancelAllChangesCommand();
            var handler = new VersionedCancelAllChangesCommandHandler(
                mockMediator.Object,
                mockVersionedRepo.Object,
                mockTruckService.Object
            );
            var result = await handler.Handle(command, default);

            result.Should().BeFalse();
        }
    }
}