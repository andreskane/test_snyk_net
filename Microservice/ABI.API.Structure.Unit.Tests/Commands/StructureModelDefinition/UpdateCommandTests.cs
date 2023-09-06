﻿using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Infrastructure.Repositories;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.Unit.Tests.Inits;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModelDefinition.Tests
{
    [TestClass()]
    public class UpdateCommandTests
    {
        private static IMapper _mapper;
        private static IStructureModelDefinitionRepository _repo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));

            _mapper = mappingConfig.CreateMapper();
            _repo = new StructureModelDefinitionRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task UpdateCommandHandlerTest()
        {
            var command = new UpdateCommand
            {
                Id = 10001,
                LevelId = 7,
                ParentLevelId = 1,
                StructureModelId = 10000
            };
            var handler = new UpdateCommandHandler(_repo, _mapper);

            await handler
                .Invoking(async (i) => await i.Handle(command, default))
                .Should().NotThrowAsync<Exception>();
        }
    }
}