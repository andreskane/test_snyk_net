using ABI.API.Structure.Application.DTO;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes.Tests
{
    [TestClass()]
    public class GetStructureChangesWithoutSavingQueryTests
    {
        [TestMethod()]
        public void GetStructureChangesWithoutSavingQueryTest()
        {
            var result = new GetStructureChangesWithoutSavingQuery();
            result.StructureId = 1;
            result.ValidityFrom = DateTimeOffset.MinValue;

            result.Should().NotBeNull();
            result.StructureId.Should().Be(1);
            result.ValidityFrom.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod()]
        public async Task GetStructureChangesWithoutSavingQueryHandlerTest()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructurePendingChangesDetailQuery>(), default))
                .ReturnsAsync(new List<ChangesDTO>
                {
                    new ChangesDTO
                    {
                        Nodes = new List<ChangeNodeDTO>
                        {
                            new ChangeNodeDTO
                            {
                                Name = "TESTA",
                                Code = "CODEA",
                                Active = true,
                                AttentionModeId = 1,
                                AttentionModeName = "NAMEA",
                                EmployeeId = 1,
                                NodeMotiveStateId = 2 // Confirmado
                            },
                            new ChangeNodeDTO
                            {
                                Name = "TESTB",
                                Code = "CODEB",
                                Active = false,
                                AttentionModeId = 2,
                                AttentionModeName = "NAMEB",
                                EmployeeId = 2,
                                NodeMotiveStateId = 1 // Borrador
                            }
                        }
                    }
                });

            var command = new GetStructureChangesWithoutSavingQuery();
            var handler = new GetStructureChangesWithoutSavingQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureChangesWithoutSavingQueryHandlerTest2()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructurePendingChangesDetailQuery>(), default))
                .ReturnsAsync(new List<ChangesDTO>());

            var command = new GetStructureChangesWithoutSavingQuery();
            var handler = new GetStructureChangesWithoutSavingQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureChangesWithoutSavingQueryHandlerTest3()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructurePendingChangesDetailQuery>(), default))
                .ReturnsAsync(new List<ChangesDTO>
                {
                    new ChangesDTO
                    {
                        Nodes = new List<ChangeNodeDTO>
                        {
                            new ChangeNodeDTO
                            {
                                Name = "TESTA",
                                Code = "CODEA",
                                Active = true,
                                AttentionModeId = 1,
                                AttentionModeName = "NAMEA",
                                EmployeeId = 1,
                                AristaMotiveStateId = 2 // Confirmado
                            },
                            new ChangeNodeDTO
                            {
                                Name = "TESTB",
                                Code = "CODEB",
                                Active = false,
                                AttentionModeId = 2,
                                AttentionModeName = "NAMEB",
                                EmployeeId = 2,
                                AristaMotiveStateId = 1 // Borrador
                            }
                        }
                    }
                });

            var command = new GetStructureChangesWithoutSavingQuery();
            var handler = new GetStructureChangesWithoutSavingQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureChangesWithoutSavingQueryHandlerTest4()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(s => s.Send(It.IsAny<GetStructurePendingChangesDetailQuery>(), default))
                .ReturnsAsync(new List<ChangesDTO>
                {
                    new ChangesDTO
                    {
                        Nodes = new List<ChangeNodeDTO>
                        {
                            new ChangeNodeDTO
                            {
                                Name = "TESTB",
                                Code = "CODEB",
                                Active = false,
                                AttentionModeId = 2,
                                AttentionModeName = "NAMEB",
                                EmployeeId = 2,
                                NodeMotiveStateId = 1 // Borrador
                            }
                        }
                    }
                });

            var command = new GetStructureChangesWithoutSavingQuery();
            var handler = new GetStructureChangesWithoutSavingQueryHandler(mediatorMock.Object);
            var results = await handler.Handle(command, default);

            results.Should().NotBeNull();
        }
    }
}