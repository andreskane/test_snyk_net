﻿using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.StructureNodes
{
    [TestClass()]
    public class GetBoundedStructureFromNodeIdQueryTests
    {
        private static StructureContext _context;
        private static IMediator _mediator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
            var node6 = new StructureNode("12624_8", 6);
            var node8 = new StructureNode("1610_8", 6);
            var node17 = new StructureNode("2646_8", 6);
            var node32 = new StructureNode("8", 5);

            var arista1 = new StructureArista(1, 32, 1, 17, 1, new DateTime(2020, 10, 08));
            arista1.EditMotiveStateId(2);
            arista1.NodeFrom = node32;
            arista1.NodeTo = node17;

            var arista2 = new StructureArista(1, 32, 1, 6, 1, new DateTime(2020, 10, 08));
            arista2.EditMotiveStateId(2);
            arista2.NodeFrom = node32;
            arista2.NodeTo = node6;

            var arista3 = new StructureArista(1, 32, 1, 8, 1, new DateTime(2020, 10, 08));
            arista3.EditMotiveStateId(2);
            arista3.NodeFrom = node32;
            arista3.NodeTo = node8;

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetAllAristaQuery>(), default))
                .ReturnsAsync(new List<StructureArista> {

                   arista1,arista2,arista3
                });

            _mediator = mediator.Object;
        }

        [TestMethod()]
        public async Task GetBoundedStructureFromNodeIdQueryTest()
        {
            var command = new GetBoundedStructureFromNodeIdQuery {
                FromNodeId = 32,
                StructureId = 1,
                SocietyCode = "01AR",
                ToLevelId = 6,
                ValidityFrom = Convert.ToDateTime("2021-02-26")
            };
            var handler = new GetBoundedStructureFromNodeIdQueryHandler(_mediator, _context);
            var results = await handler.Handle(command, default);
            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetBoundedStructureFromNodeIdQueryNullTest()
        {
            var command = new GetBoundedStructureFromNodeIdQuery
            {
                FromNodeId = 1,
                StructureId = 1,
                SocietyCode = "01AR",
                ToLevelId = 6,
                ValidityFrom = Convert.ToDateTime("2021-02-26")
            };
            var handler = new GetBoundedStructureFromNodeIdQueryHandler(_mediator, _context);
            var results = await handler.Handle(command, default);
            results.Should().BeNull();
        }
    }
}
