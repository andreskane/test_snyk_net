using ABI.API.Structure.Application.Queries.StructureNodes;
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
    public class GetRepeatedEmployeeIdsNodesByStructureQueryTests
    {
        private static IMediator _mediator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetEmployeeIdsNodesByStructureQuery>(), default))
                .ReturnsAsync(new List<Application.DTO.EmployeeIdNodesDTO>
                {
                    new Application.DTO.EmployeeIdNodesDTO
                    {
                        EmployeeId = 2280,
                        Nodes = new List<Application.DTO.EmployeeIdNodesItemDTO>
                        {
                            new Application.DTO.EmployeeIdNodesItemDTO
                            {
                                Code = "1302",
                                Id = 288,
                                Name = "NORES MARCOS"
                            },
                            new Application.DTO.EmployeeIdNodesItemDTO
                            {
                                Code = "1303",
                                Id = 315,
                                Name = "GRION GABRIEL"
                            }
                        }
                    },
                    new Application.DTO.EmployeeIdNodesDTO
                    {
                        EmployeeId = 2180,
                        Nodes = new List<Application.DTO.EmployeeIdNodesItemDTO>
                        {
                            new Application.DTO.EmployeeIdNodesItemDTO
                            {
                                Code = "1302",
                                Id = 288,
                                Name = "NORES MARCOS"
                            }
                        }
                    }
                });

            _mediator = mediator.Object;
        }

        [TestMethod()]
        public void GetRepeatedEmployeeIdsNodesByStructureQueryTest()
        {
            var result = new GetRepeatedEmployeeIdsNodesByStructureQuery();
            result.StructureId = 1;
            result.ValidityFrom = Convert.ToDateTime("2021-03-03");

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(Convert.ToDateTime("2021-03-03"));
        }

        [TestMethod()]
        public async Task GetRepeatedEmployeeIdsNodesByStructureHandlerTest()
        {
            var command = new GetRepeatedEmployeeIdsNodesByStructureQuery
            {
                StructureId = 1,
                ValidityFrom = Convert.ToDateTime("2021-02-26")
            };
            var handler = new GetRepeatedEmployeeIdsNodesByStructureHandler(_mediator);
            var results = await handler.Handle(command, default);
            results.Should().HaveCount(1);
        }
    }
}
