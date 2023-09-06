using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetEmployeeIdsNodesByStructureHandlerTests
    {
        private static ILevelRepository _repo;

        [TestMethod()]
        public async Task HandleTestAsync()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>());
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureNodesPendingQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>());
            _repo = new LevelRepository(AddDataContext._context);

            var query = new GetEmployeeIdsNodesByStructureQuery
            {
                StructureId = 1,
                ValidityFrom = DateTime.UtcNow.Date
            };
            var handler = new GetEmployeeIdsNodesByStructureQueryHandler(mediatrMock.Object, _repo);
            var result = await handler.Handle(query, default);

            result.Should().NotBeNull();
        }
    }
}