using ABI.API.Structure.ACL.Truck.Application.Queries.TypeVendor;
using ABI.API.Structure.ACL.TruckTests.Inits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Queries.TypeVendor
{
    [TestClass()]
    public class GetByAttentionModeIdQueryTests
    {

        [TestMethod()]
        public async Task GetByAttentionModeIdQueryWithResultTest()
        {
            var command = new GetByAttentionModeIdQuery { AttentionModeId = 3, RoleId =4 };
            var handler = new GetByAttentionModeIdHandler(AddDataTruckACLContext._context);
            var results = await handler.Handle(command, default);
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public async Task GetByAttentionModeIdQueryWithoutResultTest()
        {
            var command = new GetByAttentionModeIdQuery { AttentionModeId = 999, RoleId = 999 };
            var handler = new GetByAttentionModeIdHandler(AddDataTruckACLContext._context);
            var results = await handler.Handle(command, default);
            Assert.IsNull(results);
        }

        [TestMethod()]
        public async Task GetByAttentionModeIdQueryNullRoleTest()
        {
            var command = new GetByAttentionModeIdQuery { AttentionModeId = 8, RoleId = null };
            var handler = new GetByAttentionModeIdHandler(AddDataTruckACLContext._context);
            var results = await handler.Handle(command, default);
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public async Task GetByAttentionModeIdQueryNullRoleWithoutResultTest()
        {
            var command = new GetByAttentionModeIdQuery { AttentionModeId = 999, RoleId = null };
            var handler = new GetByAttentionModeIdHandler(AddDataTruckACLContext._context);
            var results = await handler.Handle(command, default);
            Assert.IsNull(results);
        }
    }
}
