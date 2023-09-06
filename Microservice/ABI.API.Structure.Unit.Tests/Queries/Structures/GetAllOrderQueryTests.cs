using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Queries.LogImpactTruck;
using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures.Tests
{
    [TestClass()]
    public class GetAllOrderQueryTests
    {
        private static IMapper _mapper;
        private static IMediator _mediator;
        private static IStructureRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetThereAreChangesWithoutSavingQuery>(), default))
                .ReturnsAsync(true);
            mediator
                .Setup(s => s.Send(It.IsAny<GetThereAreScheduledChangesQuery>(), default))
                .ReturnsAsync(true);
            mediator
                .Setup(s => s.Send(It.IsAny<GetAllStructureStatesQuery>(), default))
                .ReturnsAsync(new List<VersionedStructureStateDTO>
                {
                    new VersionedStructureStateDTO
                    {
                        StructureId = 1,
                        StateId = 1
                    }
                });

            _mediator = mediator.Object;
            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task GetAllOrderQueryHandlerTest()
        {
            var command = new GetAllOrderQuery();
            var handler = new GetAllOrderQueryHandler(_repo, _mapper, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeEmpty();
        }

        [TestMethod()]
        public async Task GetAllOrderQueryHandlerCodeTest()
        {
            var command = new GetAllOrderQuery { Country = "AR" };
            var handler = new GetAllOrderQueryHandler(_repo, _mapper, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeEmpty();
        }
    }
}