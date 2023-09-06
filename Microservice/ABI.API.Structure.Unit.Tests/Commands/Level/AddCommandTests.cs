using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Level.Tests
{
    [TestClass()]
    public class AddCommandTests
    {
        private static IMapper _mapper;
        private static ILevelRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
            _repo = new LevelRepository(AddDataContext._context);
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
                Active = true
            };
            var handler = new AddCommandHandler(_repo, _mapper);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}