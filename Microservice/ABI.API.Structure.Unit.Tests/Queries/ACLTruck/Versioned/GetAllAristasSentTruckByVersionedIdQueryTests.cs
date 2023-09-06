using ABI.API.Structure.ACL.Truck.Application.Queries.Versioned;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure;
using ABI.API.Structure.ACL.Truck.Repositories;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetAllAristasSentTruckByVersionedIdQueryTests
    {
        private static StructureContext _context;
        private static TruckACLContext _contextACL;
        private static IStructureNodePortalRepository _structureNodePortalRepository;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
            _contextACL = AddDataTruckACLContext._context;
            _structureNodePortalRepository = new StructureNodePortalRepository(_context);
        }


        [TestMethod()]
        public void GetAllAristasSentTruckByVersionedIdQueryTest()
        {
            var result = new GetAllAristasSentTruckByVersionedIdQuery
            {
                VersionedId = 1
            };

            result.Should().NotBeNull();
            result.VersionedId.Should().Be(1);
        }


        [TestMethod()]
        public async Task GetAllAristasSentTruckByVersionedIdQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.Is<GetOneVersioneByIdQuery>(w => w.VersionedId.Equals(1)), default))
                .ReturnsAsync(new Versioned {
                                    StructureId = 1, 
                                    Validity = DateTimeOffset.MinValue, 
                                    Version = "000001",
                                    GenerateVersionDate = DateTime.MinValue,
                                    StatusId = 2,
                                    User = "DemoTest"
                
                
                });

            var command = new GetAllAristasSentTruckByVersionedIdQuery { VersionedId = 1 };
            var handler = new GetAllAristasSentTruckByVersionedIdQueryHandler(_contextACL, 
                                                                              _context,
                                                                              _structureNodePortalRepository,
                                                                              mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

    }
}