using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures.Tests
{
    [TestClass()]
    public class GetAllAristaQueryTest
    {
        private static IMediator _mediator;
        private static IStructureNodeRepository _structureNodeRepository;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
         
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetAllAristaQuery>(), default));
   
            _mediator = mediator.Object;
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);
        }

        [TestMethod()]
        public async Task GetAllAristaQueryHandlerTest()
        {
            var command = new GetAllAristaQuery { StructureId = 1, NodeId = 32, ValidityFrom = new DateTime(2021, 2, 26) };
            var handler = new GetAllAristaQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllAristaQueryWithLevelHandlerTest()
        {
            var command = new GetAllAristaQuery { StructureId = 1, LevelId=4, NodeId = 32, ValidityFrom = new DateTime(2021, 2, 26) };
            var handler = new GetAllAristaQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllAristaQueryWithLevelHighHandlerTest()
        {
            var command = new GetAllAristaQuery { StructureId = 1, LevelId = 8, NodeId = 32, ValidityFrom = new DateTime(2021, 2, 26) };
            var handler = new GetAllAristaQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetAllAristaQueryWithLevelHighAndConfirmedHandlerTest()
        {
            var command = new GetAllAristaQuery { StructureId = 1, LevelId = 8, NodeId = 32, ValidityFrom = new DateTime(2021, 2, 26), OnlyConfirmedAndCurrent = true };
            var handler = new GetAllAristaQueryHandler(_structureNodeRepository, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}