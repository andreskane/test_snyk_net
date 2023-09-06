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
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Structures.Tests
{
    [TestClass()]
    public class GetByIdQueryTests
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

            _mediator = mediator.Object;
            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void GetByIdQueryTest()
        {
            var result = new GetByIdQuery();
            result.Id = 1;

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [TestMethod()]
        public async Task GetByIdQueryHandlerTestAsync()
        {
            var command = new GetByIdQuery { Id = 1 };
            var handler = new GetByIdQueryHandler(_repo, _mapper, _mediator);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}