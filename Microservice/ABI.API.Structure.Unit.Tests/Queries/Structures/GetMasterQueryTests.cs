using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Infrastructure;
using ABI.API.Structure.Application.Queries.StructureModelDefinition;
using ABI.API.Structure.Application.Queries.StructureModels;
using ABI.API.Structure.Application.Queries.Structures;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Queries.Structures
{
    [TestClass()]
    public class GetMasterQueryTests
    {
        private static IMapper _mapper;
        private static IMediator _mediator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig()));
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(s => s.Send(It.IsAny<GetAllOrderV2Query>(), default))
                .ReturnsAsync(new List<StructureModelV2DTO>
                {
                    new StructureModelV2DTO{ 
                        Id=1,
                        Name="TEST"
                    }
                });
            mediator
                .Setup(s => s.Send(It.IsAny<GetAllByStructureModelV2Query>(), default))
                .ReturnsAsync(new List<StructureModelDefinitionV2DTO>
                {
                    new StructureModelDefinitionV2DTO
                    {
                        LevelId =1
                    }
                });
            mediator
                .Setup(s => s.Send(It.IsAny<Application.Queries.Role.GetAllOrderQuery>(), default))
                .ReturnsAsync(new List<RoleDTO>
                {
                    new RoleDTO
                    {
                        Name ="TEST",
                        Id =1
                    }
                });
            mediator
                .Setup(s => s.Send(It.IsAny<Application.Queries.SaleChannel.GetAllOrderQuery>(), default))
                .ReturnsAsync(new List<SaleChannelDTO>
                {
                    new SaleChannelDTO
                    {
                        Name ="TEST",
                        Id =1
                    }
                });
            mediator
                .Setup(s => s.Send(It.IsAny<Application.Queries.AttentionMode.GetAllOrderQuery>(), default))
                .ReturnsAsync(new List<AttentionModeDTO>
                {
                    new AttentionModeDTO
                    {
                        Name ="TEST",
                        Id =1
                    }
                });
            mediator
                .Setup(s => s.Send(It.IsAny<Application.Queries.Level.GetAllOrderQuery>(), default))
                .ReturnsAsync(new List<LevelDTO>
                {
                    new LevelDTO
                    {
                        Name ="TEST",
                        Id =1
                    }
                });

            _mediator = mediator.Object;
            _mapper = mappingConfig.CreateMapper();
        }

        [TestMethod()]
        public async Task GetByIdQueryHandlerTestAsync()
        {
            var command = new GetMastersQuery();
            var handler = new GetMastersQueryHandler(_mediator, _mapper);
            var result = await handler.Handle(command, default);

            result.Should().NotBeNull();
        }
    }
}
