using ABI.API.Structure.Application.Behaviors;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Application.Validations;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Infrastructure.RepositoriesDomain;
using ABI.API.Structure.Unit.Tests.Inits;
using ABI.API.Structure.Unit.Tests.Mock;
using ABI.Framework.MS.Domain.Exceptions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Unit.Tests.Validations
{
    [TestClass()]
    public class ValidatorBehaviorTest
    {
        private static IStructureRepository _structureRepo;
        private static IStructureNodeRepository _nodeRepo;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _structureRepo = new StructureRepository(AddDataContext._context);
            _nodeRepo = new StructureNodeRepository(AddDataContext._context);
        }


        [TestMethod()]
        public async Task PipelineBehaviorTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeQuery>(), default))
                .ReturnsAsync(new List<Application.DTO.StructureNodeDTO>
                {
                    new Application.DTO.StructureNodeDTO
                    {
                        NodeLevelId = 1,
                        NodeCode = "1"
                    }
                });

            var loggerMock = new Moq.Mock<ILogger<ValidatorBehavior<ValidateStructureCommand, MediatR.Unit>>>();
            var structureModels = FactoryMock.GetMockJson<List<StructureModelDefinition>>(FactoryMock.GetMockPath("ValidationLevelStructureModelDefinition.json"));
            var command = new ValidateNodeCodeCommand { StructureId = 1, Code = "1", LevelId = 1 };
            var commandHandler = new ValidateNodeCodeCommandHandler(mediatrMock.Object);
            var behavior = new ValidatorBehavior<ValidateStructureCommand, MediatR.Unit>(
                new List<ValidateStructureNodeCodeCommandValidator>
                {
                    new ValidateStructureNodeCodeCommandValidator { StructureModels = structureModels }
                }
                .ToArray(),
                loggerMock.Object
            );
            var behaviorCommand = new ValidateStructureCommand(1, DateTimeOffset.MinValue);

            await behavior.Invoking((i) =>
                i.Handle(behaviorCommand, default, () => commandHandler.Handle(command, default))
            ).Should().ThrowAsync<NodeCodeExistsException>();
        }

        [TestMethod()]
        public async Task PipelineBehaviorFailureTest()
        {
            var mediatrMock = new Mock<IMediator>();
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetStructureDomainQuery>(), default))
                .ReturnsAsync(new StructureDomain());
            mediatrMock
                .Setup(s => s.Send(It.IsAny<GetAllNodeQuery>(), default))
                .ReturnsAsync(new List<Application.DTO.StructureNodeDTO>
                {
                    new Application.DTO.StructureNodeDTO
                    {
                        NodeLevelId = 1,
                        NodeCode = "1"
                    }
                });

            var loggerMock = new Moq.Mock<ILogger<ValidatorBehavior<ValidateStructureCommand, MediatR.Unit>>>();
            var command = new ValidateNodeCodeCommand { StructureId = 1, Code = "1", LevelId = 1 };
            var commandHandler = new ValidateNodeCodeCommandHandler(mediatrMock.Object);
            var validatorMock = new Mock<IValidator<ValidateStructureCommand>>();
            validatorMock
                .Setup(s => s.Validate(It.IsAny<ValidateStructureCommand>()))
                .Returns(new ValidationResult(
                    new List<ValidationFailure>
                    {
                        new ValidationFailure("TEST", "ERROR")
                    }
                ));

            var behavior = new ValidatorBehavior<ValidateStructureCommand, MediatR.Unit>(
                new List<IValidator<ValidateStructureCommand>>
                {
                    validatorMock.Object
                }
                .ToArray(),
                loggerMock.Object
            );
            var behaviorCommand = new ValidateStructureCommand(1, DateTimeOffset.MinValue);

            await behavior.Invoking((i) =>
                i.Handle(behaviorCommand, default, () => commandHandler.Handle(command, default))
            ).Should().ThrowAsync<DomainException>();
        }
    }
}
