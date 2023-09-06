using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.Structures
{
    [TestClass()]
    public class GetNodeTreeByNodeIdQueryTests
    {
        private static IMediator _mediator;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetNodeTreeByNodeIdQuery>(), default));

            _mediator = mediator.Object;
        }

        [TestMethod()]
        public async Task GetNodeTreeByNodeIdHandlerTest()
        {
            var command = new GetNodeTreeByNodeIdQuery { 
                StructureId = 1, 
                NodeRootId = 1,
                NodeId = 32, 
                ValidityFrom = new DateTime(2021, 2, 26) 
            };
            var handler = new GetNodeTreeByNodeIdqueryHandler(AddDataContext._context, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}
