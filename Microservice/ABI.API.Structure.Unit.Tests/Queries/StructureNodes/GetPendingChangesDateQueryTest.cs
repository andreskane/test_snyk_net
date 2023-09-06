using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.StructureNodes
{
    [TestClass()]
    public class GetPendingChangesDateQueryTest
    {
        private static StructureContext _context;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
        }


        [TestMethod()]
        public void GetPendingChangesDateQuery()
        {
            var result = new GetPendingChangesDateQuery
            {
                StructureId = 1,
                ValidityFrom = DateTime.MinValue
            };

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTime.MinValue);
        }

        [TestMethod()]  
        public async Task GetPendingChangesDateQueryTestHandlerTestAsync()
        {

            var command = new GetPendingChangesDateQuery { StructureId = 1 , ValidityFrom = DateTimeOffset.Parse("2021-06-15T00:00:00-03:00") };
            var handler = new GetPendingChangesDateQueryHandler(_context);
            var results = await handler.Handle(command, default);

            results.Should().BeNull();
        }
    }
}
