using ABI.API.Structure.Application.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllNodeMaxVersionQueryHandlerTests
    {
        [TestMethod()]
        public async Task GetAllNodeMaxVersionQueryHandlerTestAsync()
        {
            var model = new GetAllNodeMaxVersionQuery
            {
                Nodes = new List<NodeAristaDTO>
                {
                    new NodeAristaDTO
                    {
                        NodeValidityFrom = new DateTime(2021, 1, 1),
                        AristaMotiveStateId = 2
                    },
                    new NodeAristaDTO
                    {
                        NodeValidityFrom = new DateTime(2021, 1, 1),
                        NodeMotiveStateId = 1,
                        AristaMotiveStateId = 1
                    }
                },
                ValidityFrom = new DateTime(2021, 1, 1)
            };
            var handler = new GetAllNodeMaxVersionQueryHandler();
            var results = await handler.Handle(model, default);

            results.Should().HaveCount(2);
        }
    }
}