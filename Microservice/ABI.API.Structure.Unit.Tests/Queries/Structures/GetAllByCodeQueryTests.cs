using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures.Tests
{
    [TestClass()]
    public class GetAllByCodeQueryTests
    {

         [TestMethod()]
        public async Task GetAllByCodeQueryHandlerTest()
        {
            var command = new GetAllByCodeQuery {Code = "ARG_VTA7" };
            var handler = new GetAllByCodeQueryHandler(AddDataContext._context);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}