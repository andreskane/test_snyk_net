using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Interface;
using ABI.API.Structure.ACL.Truck.Application.Translators.Interface;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Tests
{
    [TestClass()]
    public class StructureAdapterTests
    {
        private static IApiTruck _apiTruck;
        private static ITruckToPortalService _truckToPortalService;
        private static ITruckService _truckService;
        private static IPortalService _portalService;
        private static IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private static IDBUHResourceRepository _dBUHResourceRepository;
        private static IStructureNodeRepository _repositoryStructureNode;
        private static IStructureNodePortalRepository _repositoryStructureNodePortal;
        private static IMediator _mediator;
        private static ITranslatorsStructuresPortalToTruck _translatorsStructuresPortalToTruck;
        private static ITranslatorsStructuresTruckToPortal _translatorsStructuresTruckToPortal;
        private static ICompareStructuresTruckAndPortal _compareStructuresTruckAndPortal;


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var mockApiTruck = new Mock<IApiTruck>();
            var mockTruckPortalService = new Mock<ITruckToPortalService>();
            var mockTruckServie = new Mock<ITruckService>();
            var mockPortalService = new Mock<IPortalService>();
            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            var mockResourceRepo = new Mock<IDBUHResourceRepository>();
            var mockNodeRepo = new Mock<IStructureNodeRepository>();
            var mockNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            var mockMediatr = new Mock<IMediator>();
            var mockTranslatorsStructuresPortalToTruck = new Mock<ITranslatorsStructuresPortalToTruck>();
            var mockTranslatorsStructuresTruckToPortal = new Mock<ITranslatorsStructuresTruckToPortal>();
            var mockCompareStructuresTruckAndPortal = new Mock<ICompareStructuresTruckAndPortal>();



            _apiTruck = mockApiTruck.Object;
            _truckToPortalService = mockTruckPortalService.Object;
            _truckService = mockTruckServie.Object;
            _portalService = mockPortalService.Object;
            _mapeoTableTruckPortal = mockMapeoTruckPortal.Object;
            _dBUHResourceRepository = mockResourceRepo.Object;
            _repositoryStructureNode = mockNodeRepo.Object;
            _repositoryStructureNodePortal = mockNodePortalRepo.Object;
            _mediator = mockMediatr.Object;
            _translatorsStructuresPortalToTruck = mockTranslatorsStructuresPortalToTruck.Object;
            _translatorsStructuresTruckToPortal = mockTranslatorsStructuresTruckToPortal.Object;
            _compareStructuresTruckAndPortal = mockCompareStructuresTruckAndPortal.Object;


    }


        [TestMethod()]
        public void StructureAdapterTest()
        {
            var result = new StructureAdapter(
                _apiTruck,
                _truckToPortalService,
                _truckService,
                _portalService,
                _mapeoTableTruckPortal,
                _dBUHResourceRepository,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal

            );

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public void StructureAdapterThrowsTest()
        {
            using var scope = new AssertionScope();

            scope
                .Invoking(i => new StructureAdapter(null, null, null, null, null, null, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();
            scope
                .Invoking(i => new StructureAdapter(_apiTruck, null, null, null, null, null, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, null, null, null, null, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, null, null, null, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, null, null, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, null, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, _dBUHResourceRepository, null, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, _dBUHResourceRepository, _repositoryStructureNode, null, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, _dBUHResourceRepository, _repositoryStructureNode, _mediator, null, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, _dBUHResourceRepository, _repositoryStructureNode, _mediator, _repositoryStructureNodePortal, null, null, null))
                .Should().Throw<ArgumentNullException>();

            scope
                  .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, _dBUHResourceRepository, _repositoryStructureNode, _mediator, _repositoryStructureNodePortal, _translatorsStructuresPortalToTruck, null, null))
                  .Should().Throw<ArgumentNullException>();

            scope
                .Invoking(i => new StructureAdapter(_apiTruck, _truckToPortalService, _truckService, _portalService, _mapeoTableTruckPortal, _dBUHResourceRepository, _repositoryStructureNode, _mediator, _repositoryStructureNodePortal, _translatorsStructuresPortalToTruck, _translatorsStructuresTruckToPortal, null))
                .Should().Throw<ArgumentNullException>();
        }

        [TestMethod()]
        public async Task StructureTruckToStructurePortalTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.GetStructureTruck(It.IsAny<string>()))
                .ReturnsAsync(new APIClient.Truck.Entities.EstructuraVentas.TruckStructure
                {
                    DataStructure = new APIClient.Truck.Entities.EstructuraVentas.DataStructure
                    {
                        EmpId = "TEST",
                        Level1 = new List<APIClient.Truck.Entities.EstructuraVentas.DataStructureLevel1>
                        {
                            new APIClient.Truck.Entities.EstructuraVentas.DataStructureLevel1
                            {
                                DveAbv = "",
                                DveFvtAbv = "",
                                DveId = "",
                                DveIdDve = "",
                                DveIdFvt = "",
                                DveNom = "",
                                DveTxt = "TEST",
                                Level2 = new List<APIClient.Truck.Entities.EstructuraVentas.DataStructureLevel2>()
                            }
                        }
                    }
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>
                {
                    new ResourceDTO
                    {
                        Id = 1,
                        Code = "1",
                        Name = "TEST",
                        Relations = new List<ResourceRelationDTO>
                        {
                            new ResourceRelationDTO
                            {
                                Code = "1",
                                Name = "TEST",
                                Attributes = new ResourceAttributesDTO
                                {
                                    VdrCod = "1",
                                    VdrTpoCat = "V"
                                }
                            }
                        }
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "TEST" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                _truckService,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );
            var result = await adapter.StructureTruckToStructurePortal("1");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task MigrationClientsTruckToPortalTest()
        {
            var mockTruckPortalService = new Mock<ITruckToPortalService>();
            mockTruckPortalService
                .Setup(s => s.GetNodesByLevelIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<StructureNode>
                {
                    new StructureNode("1", 1)
                });

            var mockApiTruck = new Mock<IApiTruck>();

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
              .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
              .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
              {
                  Id = 1,
                  Name = "ARGENTINA",
                  BusinessCode = "001",
                  StructureModelId = 1
              });

            var structure = new StructureDomain("ARGENTINA", 1, 1, DateTimeOffset.MinValue, "AR_VTA_ARGENTINA");

            structure.StructureModel = new Structure.Domain.Entities.StructureModel { 
                                    Id = 1, CountryId = 1, Country = new Structure.Domain.Entities.Country { 
                                                                            Id = 1, Code = "AR", Name = "ARGENTINA" } };

            var mockPortalService = new Mock<IPortalService>();
            mockPortalService
              .Setup(s => s.GetStrucureByName(It.IsAny<string>()))
              .ReturnsAsync(structure);

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                mockTruckPortalService.Object,
                _truckService,
                mockPortalService.Object,
                mockMapeoTruckPortal.Object,
                _dBUHResourceRepository,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );
            var result = await adapter.MigrationClientsTruckToPortal("001");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task StructureTruckToStructurePortalCompareTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.GetStructureTruck(It.IsAny<string>()))
                .ReturnsAsync(new APIClient.Truck.Entities.EstructuraVentas.TruckStructure
                {
                    DataStructure = new APIClient.Truck.Entities.EstructuraVentas.DataStructure
                    {
                        EmpId = "1",
                        Level1 = new List<APIClient.Truck.Entities.EstructuraVentas.DataStructureLevel1>
                        {
                            new APIClient.Truck.Entities.EstructuraVentas.DataStructureLevel1
                            {
                                DveId = "1",
                                DveTxt = "TEST",
                                DveIdFvt = "200",
                                DveIdDve = "1",
                                Level2 = new List<APIClient.Truck.Entities.EstructuraVentas.DataStructureLevel2>()
                            }
                        }
                    }
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockPortalService = new Mock<IPortalService>();
            mockPortalService
                .Setup(s => s.GetAllCompareByStructureId(It.IsAny<string>(), It.IsAny<bool?>()))
                .ReturnsAsync(new StructureNodeDTO
                {
                    Structure = new StructureDTO { Id = 1 }
                });



            var mockStructurePortalToTruck = new Mock<ITranslatorsStructuresPortalToTruck>();
            mockStructurePortalToTruck
                  .Setup(s => s.PortalToTruckAsync(It.IsAny<OpecpiniOut>(), It.IsAny<int>(), It.IsAny<List<NodePortalTruckDTO>>(), It.IsAny<List<ResourceDTO>>()))
                  .ReturnsAsync(new StructureTruck());

            var mockCompareStructuresTruckAndPortal = new Mock<ICompareStructuresTruckAndPortal>();
            mockCompareStructuresTruckAndPortal
                  .Setup(s => s.CompareTruckToPortal(It.IsAny<string>(), It.IsAny<TruckStructure>(), It.IsAny<StructureNodeDTO>(), It.IsAny<List<ResourceDTO>>()))
                  .ReturnsAsync(new StructurePortalCompareDTO());


            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                _truckService,
                mockPortalService.Object,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                 mockStructurePortalToTruck.Object,
                _translatorsStructuresTruckToPortal,
                mockCompareStructuresTruckAndPortal.Object
            );
            var result = await adapter.StructureTruckToStructurePortalCompare("1");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task StructureTruckToStructurePortalCompareJsonTestAsync()
        {
            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockPortalService = new Mock<IPortalService>();
            mockPortalService
                .Setup(s => s.GetAllCompareByStructureId(It.IsAny<string>(), It.IsAny<bool?>()))
                .ReturnsAsync(new StructureNodeDTO
                {
                    Structure = new StructureDTO { Id = 1 }
                });



            var mockStructurePortalToTruck = new Mock<ITranslatorsStructuresPortalToTruck>();
            mockStructurePortalToTruck
                  .Setup(s => s.PortalToTruckAsync(It.IsAny<OpecpiniOut>(), It.IsAny<int>(), It.IsAny<List<NodePortalTruckDTO>>(), It.IsAny<List<ResourceDTO>>()))
                  .ReturnsAsync(new StructureTruck());

            var mockCompareStructuresTruckAndPortal = new Mock<ICompareStructuresTruckAndPortal>();
            mockCompareStructuresTruckAndPortal
                  .Setup(s => s.CompareTruckToPortal( It.IsAny<string> (), It.IsAny<TruckStructure>(), It.IsAny<StructureNodeDTO>(), It.IsAny<List<ResourceDTO>>()))
                  .ReturnsAsync(new StructurePortalCompareDTO());

            var adapter = new StructureAdapter(
                _apiTruck,
                _truckToPortalService,
                _truckService,
                mockPortalService.Object,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                mockStructurePortalToTruck.Object,
                _translatorsStructuresTruckToPortal,
                mockCompareStructuresTruckAndPortal.Object
            );
            var result = await adapter.StructureTruckToStructurePortalCompareJson();

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task StructureTruckToStructurePortalJsonTestAsync()
        {
            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockPortalService = new Mock<IPortalService>();
            mockPortalService
                .Setup(s => s.GetAllCompareByStructureId(It.IsAny<string>(), It.IsAny<bool?>()))
                .ReturnsAsync(new StructureNodeDTO
                {
                    Structure = new StructureDTO { Id = 1 }
                });

            var adapter = new StructureAdapter(
                _apiTruck,
                _truckToPortalService,
                _truckService,
                mockPortalService.Object,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );
            var result = await adapter.StructureTruckToStructurePortalJson(null);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task StructurePortalToTruckChangesTest()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.SetOpecpini(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniOut
                {
                    Epeciniin = new APIClient.Truck.Entities.TruckImpact.Epecini
                    {
                        Empresa = "1",
                        NroVer = "1"
                    },
                    Msglog = new APIClient.Truck.Entities.TruckImpact.MsgLog
                    {
                        Level1 = new List<APIClient.Truck.Entities.TruckImpact.MsgLogLevel1>()
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockTruckService = new Mock<ITruckService>();
            mockTruckService
                .Setup(s => s.GetAllNodesSentTruckAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Application.DTO.NodePortalTruckDTO>
                {
                    new NodePortalTruckDTO { LevelId = 1, Code = "C1", Name = "TEST1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 2, Code = "C2", Name = "TEST2", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 3, Code = "C3", Name = "TEST3", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 5, Code = "C4", Name = "TEST4", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO {
                        LevelId = 6, Code = "C5", Name = "TEST5", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date,
                        ChildNodes = new List<NodePortalTruckDTO>
                        {
                            new NodePortalTruckDTO { LevelId = 8, Code = "C8", Name = "TEST8", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                        }
                    },
                    new NodePortalTruckDTO { LevelId = 7, Code = "C6", Name = "TEST6", NodeIdParent = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 8, Code = "C7", Name = "TEST7", ParentNodeCode = "P1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                });
            mockTruckService
                .Setup(s => s.GetAllAristasPortalAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PortalAristalDTO>
                {
                    new PortalAristalDTO { AristaId = 1 }
                });
            mockTruckService
                .Setup(s => s.SetVersionedIniVersion(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniOut>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(1);

            var mockStructureNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            mockStructureNodePortalRepo
                .Setup(s => s.GetNodoParent(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new NodePortalTruckDTO { ParentNodeCode = "P1" });


            var mockStructurePortalToTruck = new Mock<ITranslatorsStructuresPortalToTruck>();
            mockStructurePortalToTruck
                  .Setup(s => s.PortalToTruckAsync(It.IsAny<OpecpiniOut>(), It.IsAny<int>(), It.IsAny<List<NodePortalTruckDTO>>(), It.IsAny<List<ResourceDTO>>()))
                  .ReturnsAsync(new StructureTruck());

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                mockTruckService.Object,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                mockStructureNodePortalRepo.Object,
                mockStructurePortalToTruck.Object,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );
            var result = await adapter.StructurePortalToTruckChanges(1, DateTimeOffset.UtcNow.Date);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureVersionTruckStatusTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.GetStructureVersionStatusTruck(It.IsAny<APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionOutput());

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                _truckService,
                _portalService,
                _mapeoTableTruckPortal,
                _dBUHResourceRepository,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );
            var result = await adapter.GetStructureVersionTruckStatus(1, "TEST");

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureVersionTruckStatusThrowsTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.GetStructureVersionStatusTruck(It.IsAny<APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionInput>()))
                .ThrowsAsync(new Exception());

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                _truckService,
                _portalService,
                _mapeoTableTruckPortal,
                _dBUHResourceRepository,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            await adapter
                .Invoking(i => i.GetStructureVersionTruckStatus(1, "TEST"))
                .Should().ThrowAsync<GenericException>();
        }

        [TestMethod()]
        public async Task GetPendingVersionTruckTestAsync()
        {
            var adapter = new StructureAdapter(
                _apiTruck,
                _truckToPortalService,
                _truckService,
                _portalService,
                _mapeoTableTruckPortal,
                _dBUHResourceRepository,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            var result = await adapter.GetPendingVersionTruck(1, new APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionOutput
            {
                EstructuraVersiones = new APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersiones
                {
                    Level1 = new List<APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionesLevel1>
                    {
                        new APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionesLevel1
                        {
                            ECStsCod = "APR",
                            ECFecTra = DateTimeOffset.UtcNow.AddDays(1).ToString("yyyy-MM-dd"),
                            ECFecDes = DateTimeOffset.UtcNow.AddDays(1).ToString("yyyy-MM-dd")
                        }
                    }
                }
            });

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GetStructureVersionTruckStatusCurrentTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.GetStructureVersionStatusTruck(It.IsAny<APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.EstructuraVersiones.EstructuraVersionOutput());

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                _truckService,
                _portalService,
                _mapeoTableTruckPortal,
                _dBUHResourceRepository,
                _repositoryStructureNode,
                _mediator,
                _repositoryStructureNodePortal,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );
            var result = await adapter.GetStructureVersionTruckStatusCurrent(1);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task GenerateNewVersionThrowsTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.SetOpecpini(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniOut
                {
                    Epeciniin = new APIClient.Truck.Entities.TruckImpact.Epecini
                    {
                        Empresa = "1",
                        NroVer = "1"
                    },
                    Msglog = new APIClient.Truck.Entities.TruckImpact.MsgLog
                    {
                        Level1 = new List<APIClient.Truck.Entities.TruckImpact.MsgLogLevel1>()
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockTruckService = new Mock<ITruckService>();
            mockTruckService
                .Setup(s => s.GetAllNodesSentTruckAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Application.DTO.NodePortalTruckDTO>
                {
                    new NodePortalTruckDTO { LevelId = 1, Code = "C1", Name = "TEST1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 2, Code = "C2", Name = "TEST2", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 3, Code = "C3", Name = "TEST3", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 5, Code = "C4", Name = "TEST4", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO {
                        LevelId = 6, Code = "C5", Name = "TEST5", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date,
                        ChildNodes = new List<NodePortalTruckDTO>
                        {
                            new NodePortalTruckDTO { LevelId = 8, Code = "C8", Name = "TEST8", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                        }
                    },
                    new NodePortalTruckDTO { LevelId = 7, Code = "C6", Name = "TEST6", NodeIdParent = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 8, Code = "C7", Name = "TEST7", ParentNodeCode = "P1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                });
            mockTruckService
                .Setup(s => s.GetAllAristasPortalAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PortalAristalDTO>());
            mockTruckService
                .Setup(s => s.SetVersionedIniVersion(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniOut>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(1);
            mockTruckService
                .SetupSequence(s => s.SetActionNewVersion(It.IsAny<DateTimeOffset>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception())
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniInput());

            var mockStructureNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            mockStructureNodePortalRepo
                .Setup(s => s.GetNodoParent(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new NodePortalTruckDTO { ParentNodeCode = "P1" });

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                mockTruckService.Object,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                mockStructureNodePortalRepo.Object,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            await adapter
                .Invoking(i => i.StructurePortalToTruckChanges(1, DateTimeOffset.UtcNow.Date))
                .Should().ThrowAsync<GenericException>();
        }

        [TestMethod()]
        public async Task LoadTraysThrowsTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.SetOpecpini(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniOut
                {
                    Epeciniin = new APIClient.Truck.Entities.TruckImpact.Epecini
                    {
                        Empresa = "1",
                        NroVer = "1"
                    },
                    Msglog = new APIClient.Truck.Entities.TruckImpact.MsgLog
                    {
                        Level1 = new List<APIClient.Truck.Entities.TruckImpact.MsgLogLevel1>()
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockTruckService = new Mock<ITruckService>();
            mockTruckService
                .Setup(s => s.GetAllNodesSentTruckAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Application.DTO.NodePortalTruckDTO>
                {
                    new NodePortalTruckDTO { LevelId = 1, Code = "C1", Name = "TEST1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 2, Code = "C2", Name = "TEST2", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 3, Code = "C3", Name = "TEST3", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 5, Code = "C4", Name = "TEST4", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO {
                        LevelId = 6, Code = "C5", Name = "TEST5", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date,
                        ChildNodes = new List<NodePortalTruckDTO>
                        {
                            new NodePortalTruckDTO { LevelId = 8, Code = "C8", Name = "TEST8", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                        }
                    },
                    new NodePortalTruckDTO { LevelId = 7, Code = "C6", Name = "TEST6", NodeIdParent = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 8, Code = "C7", Name = "TEST7", ParentNodeCode = "P1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                });
            mockTruckService
                .Setup(s => s.GetAllAristasPortalAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PortalAristalDTO>());
            mockTruckService
                .Setup(s => s.SetVersionedIniVersion(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniOut>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(1);


            var mockStructureNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            mockStructureNodePortalRepo
                .Setup(s => s.GetNodoParent(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new NodePortalTruckDTO { ParentNodeCode = "P1" });

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                mockTruckService.Object,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                mockStructureNodePortalRepo.Object,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            await adapter
                .Invoking(i => i.StructurePortalToTruckChanges(1, DateTimeOffset.UtcNow.Date))
                .Should().ThrowAsync<GenericException>();
        }

        [TestMethod()]
        public async Task ActionAPRThrowsTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.SetOpecpini(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniOut
                {
                    Epeciniin = new APIClient.Truck.Entities.TruckImpact.Epecini
                    {
                        Empresa = "1",
                        NroVer = "1"
                    },
                    Msglog = new APIClient.Truck.Entities.TruckImpact.MsgLog
                    {
                        Level1 = new List<APIClient.Truck.Entities.TruckImpact.MsgLogLevel1>()
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockTruckService = new Mock<ITruckService>();
            mockTruckService
                .Setup(s => s.GetAllNodesSentTruckAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Application.DTO.NodePortalTruckDTO>
                {
                    new NodePortalTruckDTO { LevelId = 1, Code = "C1", Name = "TEST1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 2, Code = "C2", Name = "TEST2", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 3, Code = "C3", Name = "TEST3", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 5, Code = "C4", Name = "TEST4", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO {
                        LevelId = 6, Code = "C5", Name = "TEST5", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date,
                        ChildNodes = new List<NodePortalTruckDTO>
                        {
                            new NodePortalTruckDTO { LevelId = 8, Code = "C8", Name = "TEST8", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                        }
                    },
                    new NodePortalTruckDTO { LevelId = 7, Code = "C6", Name = "TEST6", NodeIdParent = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 8, Code = "C7", Name = "TEST7", ParentNodeCode = "P1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                });
            mockTruckService
                .Setup(s => s.GetAllAristasPortalAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PortalAristalDTO>());
            mockTruckService
                .Setup(s => s.SetVersionedIniVersion(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniOut>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(1);
            mockTruckService
                .Setup(s => s.SetActionAPR(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var mockStructureNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            mockStructureNodePortalRepo
                .Setup(s => s.GetNodoParent(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new NodePortalTruckDTO { ParentNodeCode = "P1" });

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                mockTruckService.Object,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                mockStructureNodePortalRepo.Object,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            await adapter
                .Invoking(i => i.StructurePortalToTruckChanges(1, DateTimeOffset.UtcNow.Date))
                .Should().ThrowAsync<GenericException>();
        }

        [TestMethod()]
        public async Task ActionAPRWithErrorsTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.SetOpecpini(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniOut
                {
                    Epeciniin = new APIClient.Truck.Entities.TruckImpact.Epecini
                    {
                        Empresa = "1",
                        NroVer = "1"
                    },
                    Msglog = new APIClient.Truck.Entities.TruckImpact.MsgLog
                    {
                        Level1 = new List<APIClient.Truck.Entities.TruckImpact.MsgLogLevel1>
                        {
                            new APIClient.Truck.Entities.TruckImpact.MsgLogLevel1
                            {
                                ECLogSts = "ERR"
                            }
                        }
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockTruckService = new Mock<ITruckService>();
            mockTruckService
                .Setup(s => s.GetAllNodesSentTruckAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Application.DTO.NodePortalTruckDTO>
                {
                    new NodePortalTruckDTO { LevelId = 1, Code = "C1", Name = "TEST1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 2, Code = "C2", Name = "TEST2", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 3, Code = "C3", Name = "TEST3", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 5, Code = "C4", Name = "TEST4", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO {
                        LevelId = 6, Code = "C5", Name = "TEST5", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date,
                        ChildNodes = new List<NodePortalTruckDTO>
                        {
                            new NodePortalTruckDTO { LevelId = 8, Code = "C8", Name = "TEST8", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                        }
                    },
                    new NodePortalTruckDTO { LevelId = 7, Code = "C6", Name = "TEST6", NodeIdParent = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 8, Code = "C7", Name = "TEST7", ParentNodeCode = "P1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                });
            mockTruckService
                .Setup(s => s.GetAllAristasPortalAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PortalAristalDTO>());
            mockTruckService
                .Setup(s => s.SetVersionedIniVersion(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniOut>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(1);

            var mockStructureNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            mockStructureNodePortalRepo
                .Setup(s => s.GetNodoParent(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new NodePortalTruckDTO { ParentNodeCode = "P1" });


            var mockStructurePortalToTruck = new Mock<ITranslatorsStructuresPortalToTruck>();
            mockStructurePortalToTruck
                  .Setup(s => s.PortalToTruckAsync(It.IsAny<OpecpiniOut>(), It.IsAny<int>(), It.IsAny<List<NodePortalTruckDTO>>(), It.IsAny<List<ResourceDTO>>()))
                  .ReturnsAsync(new StructureTruck ());


            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                mockTruckService.Object,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                mockStructureNodePortalRepo.Object,
                mockStructurePortalToTruck.Object,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            var result = await adapter.StructurePortalToTruckChanges(1, DateTimeOffset.UtcNow.Date);

            result.Should().NotBeNull();
        }

        [TestMethod()]
        public async Task LogNodeAristaThrowsTestAsync()
        {
            var mockApiTruck = new Mock<IApiTruck>();
            mockApiTruck
                .Setup(s => s.SetOpecpini(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniInput>()))
                .ReturnsAsync(new APIClient.Truck.Entities.TruckImpact.OpecpiniOut
                {
                    Epeciniin = new APIClient.Truck.Entities.TruckImpact.Epecini
                    {
                        Empresa = "1",
                        NroVer = "1"
                    },
                    Msglog = new APIClient.Truck.Entities.TruckImpact.MsgLog
                    {
                        Level1 = new List<APIClient.Truck.Entities.TruckImpact.MsgLogLevel1>()
                    }
                });

            var mockMapeoTruckPortal = new Mock<IMapeoTableTruckPortal>();
            mockMapeoTruckPortal
                .Setup(s => s.GetAllLevelTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.LevelTruckPortal>
                {
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 1, LevelTruck = 1, LevelTruckName = "DIRECCION", TypeEmployeeTruck = "D" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 2, LevelTruck = 2, LevelTruckName = "AREA", TypeEmployeeTruck = "R" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 3, LevelTruck = 3, LevelTruckName = "GERENCIA", TypeEmployeeTruck = "G" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 5, LevelTruck = 4, LevelTruckName = "REGION", TypeEmployeeTruck = "J" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 6, LevelTruck = 5, LevelTruckName = "JEFATURA", TypeEmployeeTruck = "O" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 7, LevelTruck = 5, LevelTruckName = "ZONA", TypeEmployeeTruck = "S" },
                    new Domain.Entities.LevelTruckPortal { LevelPortalId = 8, LevelTruck = 6, LevelTruckName = "TERRITORIO", TypeEmployeeTruck = "V,C" }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllBusinessTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.BusinessTruckPortal>
                {
                    new Domain.Entities.BusinessTruckPortal { BusinessCode = "TEST", StructureModelId = 1 }
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetAllTypeVendorTruckPortal())
                .ReturnsAsync(new List<Domain.Entities.TypeVendorTruckPortal>
                {
                    new Domain.Entities.TypeVendorTruckPortal()
                });
            mockMapeoTruckPortal
                .Setup(s => s.GetOneBusinessTruckPortal(It.IsAny<string>()))
                .ReturnsAsync(new Domain.Entities.BusinessTruckPortal
                {
                    Name = "TEST"
                });

            var mockResourcesRepo = new Mock<IDBUHResourceRepository>();
            mockResourcesRepo
                .Setup(s => s.GetAllResource())
                .ReturnsAsync(new List<ResourceDTO>());

            var mockTruckService = new Mock<ITruckService>();
            mockTruckService
                .Setup(s => s.GetAllNodesSentTruckAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Application.DTO.NodePortalTruckDTO>
                {
                    new NodePortalTruckDTO { LevelId = 1, Code = "C1", Name = "TEST1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 2, Code = "C2", Name = "TEST2", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 3, Code = "C3", Name = "TEST3", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 5, Code = "C4", Name = "TEST4", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO {
                        LevelId = 6, Code = "C5", Name = "TEST5", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date,
                        ChildNodes = new List<NodePortalTruckDTO>
                        {
                            new NodePortalTruckDTO { LevelId = 8, Code = "C8", Name = "TEST8", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                        }
                    },
                    new NodePortalTruckDTO { LevelId = 7, Code = "C6", Name = "TEST6", NodeIdParent = 1, ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date },
                    new NodePortalTruckDTO { LevelId = 8, Code = "C7", Name = "TEST7", ParentNodeCode = "P1", ValidityFrom = DateTimeOffset.UtcNow.Date, ValidityTo = DateTimeOffset.UtcNow.Date }
                });
            mockTruckService
                .Setup(s => s.GetAllAristasPortalAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
                .ReturnsAsync(new List<PortalAristalDTO>());
            mockTruckService
                .Setup(s => s.SetVersionedIniVersion(It.IsAny<APIClient.Truck.Entities.TruckImpact.OpecpiniOut>(), It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(1);
            mockTruckService
                .Setup(s => s.SetVersionedNode(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var mockStructureNodePortalRepo = new Mock<IStructureNodePortalRepository>();
            mockStructureNodePortalRepo
                .Setup(s => s.GetNodoParent(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new NodePortalTruckDTO { ParentNodeCode = "P1" });

            var adapter = new StructureAdapter(
                mockApiTruck.Object,
                _truckToPortalService,
                mockTruckService.Object,
                _portalService,
                mockMapeoTruckPortal.Object,
                mockResourcesRepo.Object,
                _repositoryStructureNode,
                _mediator,
                mockStructureNodePortalRepo.Object,
                _translatorsStructuresPortalToTruck,
                _translatorsStructuresTruckToPortal,
                _compareStructuresTruckAndPortal
            );

            await adapter
                .Invoking(i => i.StructurePortalToTruckChanges(1, DateTimeOffset.UtcNow.Date))
                .Should().ThrowAsync<GenericException>();
        }
    }
}