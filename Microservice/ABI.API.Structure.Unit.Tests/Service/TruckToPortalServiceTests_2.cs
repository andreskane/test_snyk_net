using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureModel;
using ABI.API.Structure.ACL.Truck.Application.Service;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.ACL.Truck.Service.Tests
{
    [TestClass()]
    public class TruckToPortalServiceTests
    {

        private static  IStructureRepository _repository;
        private static  IStructureNodeRepository _structureNodeRepository;
        private static  IMediator _mediator;
        private static StructureContext _context;


        [ClassInitialize()]
        public static void Setup(TestContext context)
        {
            _context = AddDataContext._context;
            _repository = new StructureRepository(AddDataContext._context);
            _structureNodeRepository = new StructureNodeRepository(AddDataContext._context);

            var country = new CountryDTO { Id = 1, Name = "ARGENTINA", Code = "AR" };

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetStructureModelByIdQuery>(), default))
                .ReturnsAsync(new StructureModelDTO
                {
                    Id = 1,
                    Name = "Test",
                    ShortName = "Test",
                    Description = "Test",
                    Code = "VTA",
                    Active = true,
                    CanBeExportedToTruck = false,
                    CountryId = 1,
                    Country = country
                }) ;


            _mediator = mediator.Object;
        }

        [TestMethod()]
        public void TruckToPortalServiceTest()
        {
            var service = new TruckToPortalService(_repository, _structureNodeRepository, _mediator);

            service.Should().NotBeNull();
            service._Structure.Should().BeNull();
            service._StructureAristaItems.Should().BeNull();
        }

        [TestMethod()]
        public void MigrateEstructureAsyncTest()
        {
            var service = new TruckToPortalService(_repository, _structureNodeRepository, _mediator);

            var portal = new StructurePortalDTO
            {
                StructureModelId = 1,
                ValidityFrom = DateTime.UtcNow.Date,
                Nodes = new List<NodePortalDTO>()
            };

            var node = new NodePortalDTO {
                Name = "TEST",
                Code = "12",
                LevelId = 1,
                AttentionModeId = 1,
                EmployeeId = 1,
                IsRootNode = true,
                Active = true,
                ValidityFrom = DateTime.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date,
                VacantPerson = true,
                Nodes = new List<NodePortalDTO>()

            };

            portal.Nodes.Add(node);

            var result = service.MigrateEstructureAsync(portal, "Test");

            result.Should().NotBeNull();
        }


        [TestMethod()]
        public void MigrateEstructureAsyncLongCodeTest()
        {
            var service = new TruckToPortalService(_repository, _structureNodeRepository, _mediator);

            var portal = new StructurePortalDTO
            {
                StructureModelId = 1,
                ValidityFrom = DateTime.UtcNow.Date,
                Nodes = new List<NodePortalDTO>()
            };

            var node = new NodePortalDTO
            {
                Name = "TEST",
                Code = "12",
                LevelId = 1,
                AttentionModeId = 1,
                EmployeeId = 1,
                IsRootNode = true,
                Active = true,
                ValidityFrom = DateTime.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date,
                VacantPerson = true,
                Nodes = new List<NodePortalDTO>()

            };

            portal.Nodes.Add(node);

            var result = service.MigrateEstructureAsync(portal, "ARGENINA_TEST_NUEVO_BUENOS");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void MigrateEstructureAsyncNullTest()
        {

            var mediatorNull = new Mock<IMediator>();
            mediatorNull
                .Setup(s => s.Send(It.IsAny<GetStructureModelByIdQuery>(), default))
                         .ReturnsAsync(new StructureModelDTO
                         {
                             Id = 1,
                             Name = "Test",
                             ShortName = "Test",
                             Description = "Test",
                             Code = "VTA",
                             Active = true,
                             CanBeExportedToTruck = false,
                             CountryId = 1,
                             Country = null
                         });


            var service = new TruckToPortalService(_repository, _structureNodeRepository, mediatorNull.Object);

            var portal = new StructurePortalDTO
            {
                StructureModelId = 1,
                ValidityFrom = DateTime.UtcNow.Date,
                Nodes = new List<NodePortalDTO>()
            };

            var node = new NodePortalDTO
            {
                Name = "TEST",
                Code = "12",
                LevelId = 1,
                AttentionModeId = 1,
                EmployeeId = 1,
                IsRootNode = true,
                Active = true,
                ValidityFrom = DateTime.UtcNow.Date,
                ValidityTo = DateTimeOffset.MaxValue.Date,
                VacantPerson = true,
                Nodes = new List<NodePortalDTO>()

            };

            portal.Nodes.Add(node);

            var result = service.MigrateEstructureAsync(portal, "ARGENINA_TEST_NUEVO_BUENOS");

            result.Should().NotBeNull();
        }
    }
}