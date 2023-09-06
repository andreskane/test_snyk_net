using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures.Tests
{
    [TestClass()]
    public class CreateStructureCommandTests
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;
        private static IMapper _mapper;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            _mapper = mappingConfig.CreateMapper();

            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public void CreateStructureCommandTest()
        {
            var result = new CreateStructureCommand("TEST", 1, null, null, null);
            result.Should().NotBeNull();
            result.Name.Should().Be("TEST");
            result.StructureModelId.Should().Be(1);
            result.RootNodeId.Should().BeNull();
            result.ValidityFrom.Should().BeNull();
            result.Code.Should().BeNull();
        }

        [TestMethod()]
        public void CreateStructureCommandFullTest()
        {
            var result = new CreateStructureCommand("TEST", 1, 1, DateTime.UtcNow.Date, "ARG_VTA");
            result.Should().NotBeNull();
            result.Name.Should().Be("TEST");
            result.StructureModelId.Should().Be(1);
            result.RootNodeId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTime.UtcNow.Date);
            result.Code.Should().Be("ARG_VTA");
        }

        [TestMethod()]
        public async Task CreateStructureCommandHandlerTest()
        {
            var command = new CreateStructureCommand("TEST", 1, null, null, "ARG_VTA");
            var handler = new CreateStructureCommandHandler(_structureRepo);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync();
        }

        [TestMethod()]
        public async Task CreateStructureCommandHandlerNullParametersTest()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new CreateStructureCommandHandler(null))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }

        [TestMethod()]
        public async Task CreateStructureCommandHandlerTestV2()
        {
            var command = new CreateStructureCommandV2("TESTC", 1, null, null, "ARG_VTA_TESTC");
            var handler = new CreateStructureCommandHandlerV2(_structureRepo, _mapper);

            await handler.Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync();
        }

        [TestMethod()]
        public async Task CreateStructureCommandHandlerNullParametersTestV2()
        {
            var throws = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Task.Run(() => new CreateStructureCommandHandlerV2(null, _mapper))
            );

            throws.Should().BeOfType(typeof(ArgumentNullException));
        }
    }
}