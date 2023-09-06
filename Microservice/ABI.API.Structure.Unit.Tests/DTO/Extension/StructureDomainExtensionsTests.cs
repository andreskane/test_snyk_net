using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Application.DTO.Extension.Tests
{
    [TestClass()]
    public class StructureDomainExtensionsTests
    {
        [TestMethod()]
        public void ToStructureDomainDTOTest()
        {
            var result = StructureDomainExtensions.ToStructureDomainDTO(
                new StructureDomain("TEST", 1, 2, DateTime.UtcNow.Date)
                {
                    Node = new StructureNode
                    {
                        StructureNodoDefinitions = new List<StructureNodeDefinition>
                        {
                            new StructureNodeDefinition(1, 1, 1, 1, 1, DateTimeOffset.MinValue, "TEST", true)
                        }
                    },
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        Id = 1,
                        Name = "TEST",
                        ShortName = "TST",
                        Description = "TEST",
                        Active = true,
                        CanBeExportedToTruck = true,
                        CountryId = 1,
                        Country = new Domain.Entities.Country { Id = 1, Name ="ARGENTINA", Code = "AR"}
                    }
                },
                DateTimeOffset.MaxValue,
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, VersionType = "N" }
                },
                true,
                true
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToStructureDomainDTOTwoTest()
        {
            var result = StructureDomainExtensions.ToStructureDomainDTO(
                new StructureDomain("TEST", 1, 2, DateTime.UtcNow.Date)
                {
                    Node = new StructureNode
                    {
                        StructureNodoDefinitions = new List<StructureNodeDefinition>()
                    },
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        Id = 1,
                        Name = "TEST",
                        ShortName = "TST",
                        Description = "TEST",
                        Active = true,
                        CanBeExportedToTruck = true
                    }
                },
                DateTimeOffset.MaxValue,
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, VersionType = "N" }
                },
                true,
                true
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToStructureDomainDTOThreeTest()
        {
            var result = StructureDomainExtensions.ToStructureDomainDTO(
                new StructureDomain("TEST", 1, 2, DateTime.UtcNow.Date)
                {
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        Id = 1,
                        Name = "TEST",
                        ShortName = "TST",
                        Description = "TEST",
                        Active = true,
                        CanBeExportedToTruck = true
                    }
                },
                DateTimeOffset.MaxValue,
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1, VersionType = "N" }
                },
                true,
                true
            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void ToStructureDomainDTOFourTest()
        {
            var result = StructureDomainExtensions.ToStructureDomainDTO(
                new StructureDomain("TEST", 1, 2, DateTime.UtcNow.Date)
                {
                    StructureModel = new Domain.Entities.StructureModel
                    {
                        Id = 1,
                        Name = "TEST",
                        ShortName = "TST",
                        Description = "TEST",
                        Active = true,
                        CanBeExportedToTruck = true
                    }
                },
                DateTimeOffset.MaxValue,
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { NodeId = 1 },
                    new StructureNodeDTO { NodeId = 2 }
                },
                new List<StructureNodeDTO>
                {
                    new StructureNodeDTO { VersionType = "N" }
                },
                true,
                true
            );

            result.Should().NotBeNull();
        }
    }
}