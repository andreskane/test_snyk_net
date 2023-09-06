using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModel.Tests
{
    [TestClass()]
    public class AddCommandTests
    {
        private static IMapper _mapper;
        private static IStructureModelRepository _repo;
        private static IStructureModelDefinitionRepository _defRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureModelRepository(AddDataContext._context);
            _defRepo = new StructureModelDefinitionRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task AddCommandHandlerTestAsync()
        {
            var command = new AddCommand
            {
                Name = "TEST",
                Description = "TEST",
                ShortName = "TST",
                Erasable = false,
                Active = true,
                CountryId =1,
                Code="VTF"
            };
            var handler = new AddCommandHandler(_repo, _mapper, _defRepo);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }

        [TestMethod()]
        public async Task AddCommandHandlerWithStructureModelTestAsync()
        {
            var command = new AddCommand
            {
                Name = "TEST3",
                Description = "TEST",
                ShortName = "TST",
                Erasable = false,
                Active = true,
                CountryId = 1,
                Code = "VTG",

                StructureModelSourceId = 1
            };
            var handler = new AddCommandHandler(_repo, _mapper, _defRepo);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}