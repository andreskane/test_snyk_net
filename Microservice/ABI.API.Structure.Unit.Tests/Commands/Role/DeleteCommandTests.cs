using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Role.Tests
{
    [TestClass()]
    public class DeleteCommandTests
    {
        private static IRoleRepository _repo;
        private static IAttentionModeRoleRepository _attentionModeRoleRepo;
        private static ITypeVendorTruckRepository _typeVendorTruckRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _repo = new RoleRepository(AddDataContext._context);
            _attentionModeRoleRepo = new AttentionModeRoleRepository(AddDataContext._context);
            _typeVendorTruckRepo = new TypeVendorTruckRepository(AddDataTruckACLContext._context);
        }


        [TestMethod()]
        public void DeleteCommandTest()
        {
            var result = new DeleteCommand(1);
            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task DeleteCommandHandlerTest()
        {
            var command = new DeleteCommand(99999);
            var handler = new DeleteCommandHandler(_repo, _attentionModeRoleRepo, _typeVendorTruckRepo);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}