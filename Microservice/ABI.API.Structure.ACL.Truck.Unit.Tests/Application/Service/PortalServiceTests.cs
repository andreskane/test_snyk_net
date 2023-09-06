using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Application.Service
{
    [TestClass()]
    public class PortalServiceTests
    {
        private static IStructureRepository _repository;
        private static IStructureNodeRepository _structureNodeRepository;
        private static IMediator _mediator;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            var mockStructureRepo = new Mock<IStructureRepository>();
            var mockStructureNodeRepo = new Mock<IStructureNodeRepository>();
            var mockMediatr = new Mock<IMediator>();

            _repository = mockStructureRepo.Object;
            _structureNodeRepository = mockStructureNodeRepo.Object;
            _mediator = mockMediatr.Object;
        }


        [TestMethod()]
        public void PortalServiceTest()
        {
            var result = new PortalService(_repository, _structureNodeRepository, _mediator);

            result.Should().NotBeNull();
        }

    }
}
