using ABI.API.Structure.ACL.Truck.Application.Queries.CategoryVendor;
using ABI.API.Structure.ACL.Truck.Application.Queries.TypeVendor;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Queries.CategoryVendor
{
    [TestClass()]
    public class GetResponsableByAttentioModeRoleQueryTests
    {
        private static IMediator _mediator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetByAttentionModeIdQuery>(), default))
                .ReturnsAsync(new TypeVendorTruckPortal
                {
                    VendorTruckId = 29
                });
            mediator
                .Setup(s => s.Send(It.IsAny<GetByVendorTruckIdQuery>(), default))
                .ReturnsAsync(new Truck.Application.Service.Models.CategoryVendor
                {
                    CategoryResponsable = "S"
                });

            _mediator = mediator.Object;
        }

        [TestMethod()]
        public async Task GetResponsableByAttentioModeRoleQueryHandlerTest()
        {
            var command = new GetResponsableByAttentioModeRoleQuery { AttentionModeId = 3, RoleId = 4};
            var handler = new GetResponsableByAttentioModeRoleHandler(_mediator);
            var results = await handler.Handle(command, default);
            results.Should().BeTrue();
        }
    }
}
