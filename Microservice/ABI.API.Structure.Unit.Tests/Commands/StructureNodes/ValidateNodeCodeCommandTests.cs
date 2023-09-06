using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.Framework.MS.Net.RestClient;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes.Tests
{
    [TestClass()]
    public class ValidateNodeCodeCommandTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void ValidateNodeCodeCommandTest()
        {
            var result = new ValidateNodeCodeCommand();
            result.Should().NotBeNull();
            result.StructureId.Should().Be(0);
            result.LevelId.Should().Be(0);
            result.Code.Should().BeNull();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeCommandHandlerTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO {  NodeLevelId = 1, NodeCode = "1" }
                });

            var command = new ValidateNodeCodeCommand
            {
                StructureId = 1,
                LevelId = 1,
                Code = "1"
            };
            var handler = new ValidateNodeCodeCommandHandler(mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NodeCodeExistsException>();
        }

        [TestMethod()]
        public async Task ValidateNodeCodeCommandHandlerNotExistsTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeQuery>(), default))
                .ReturnsAsync(new List<DTO.StructureNodeDTO>
                {
                    new DTO.StructureNodeDTO {  NodeLevelId = 1, NodeCode = "1" }
                });

            var command = new ValidateNodeCodeCommand
            {
                StructureId = 1,
                LevelId = 2,
                Code = "1"
            };
            var handler = new ValidateNodeCodeCommandHandler(mediatrMock.Object);
            var result = await handler.Handle(command, default);

            result.Should().Be(MediatR.Unit.Value);
        }

        [TestMethod()]
        public async Task ValidateNodeCodeCommandHandlerNotFoundStructureTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(default(StructureDomain));

            var command = new ValidateNodeCodeCommand { StructureId = 99999 };
            var handler = new ValidateNodeCodeCommandHandler(mediatrMock.Object);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().ThrowAsync<NotFoundException>();
        }
    }
}
