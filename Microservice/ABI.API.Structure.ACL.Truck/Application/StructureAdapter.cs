using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Queries;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Interface;
using ABI.API.Structure.ACL.Truck.Application.Translators.Interface;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Mock;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Helpers.Extensions;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck
{
    public class StructureAdapter : IStructureAdapter
    {
        private readonly IApiTruck _apiTruck;
        private readonly ITruckToPortalService _truckToPortalService;
        private readonly ITruckService _truckService;
        private readonly IPortalService _portalService;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IStructureNodeRepository _repositoryStructureNode;
        private readonly IStructureNodePortalRepository _repositoryStructureNodePortal;
        private readonly IMediator _mediator;
        private readonly ITranslatorsStructuresPortalToTruck _translatorsStructuresPortalToTruck;
        private readonly ITranslatorsStructuresTruckToPortal _translatorsStructuresTruckToPortal;
        private readonly ICompareStructuresTruckAndPortal _compareStructuresTruckAndPortal;


        public StructureAdapter(
            IApiTruck apiTruck,
            ITruckToPortalService truckToPortalService,
            ITruckService truckService,
            IPortalService portalService,
            IMapeoTableTruckPortal mapeoTableTruckPortal,
            IDBUHResourceRepository dBUHResourceRepository,
            IStructureNodeRepository repositoryStructureNode,
            IMediator mediator,
            IStructureNodePortalRepository repositoryStructureNodePortal,
            ITranslatorsStructuresPortalToTruck translatorsStructuresPortalTruck,
            ITranslatorsStructuresTruckToPortal translatorsStructures,
            ICompareStructuresTruckAndPortal compareStructures)
        {
            _apiTruck = apiTruck ?? throw new ArgumentNullException(nameof(apiTruck));
            _truckToPortalService = truckToPortalService ?? throw new ArgumentNullException(nameof(truckToPortalService));
            _truckService = truckService ?? throw new ArgumentNullException(nameof(truckService));
            _portalService = portalService ?? throw new ArgumentNullException(nameof(portalService));
            _mapeoTableTruckPortal = mapeoTableTruckPortal ?? throw new ArgumentNullException(nameof(mapeoTableTruckPortal));
            _dBUHResourceRepository = dBUHResourceRepository ?? throw new ArgumentNullException(nameof(dBUHResourceRepository));
            _repositoryStructureNode = repositoryStructureNode ?? throw new ArgumentNullException(nameof(repositoryStructureNode));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryStructureNodePortal = repositoryStructureNodePortal ?? throw new ArgumentNullException(nameof(repositoryStructureNodePortal));
            _translatorsStructuresPortalToTruck = translatorsStructuresPortalTruck ?? throw new ArgumentNullException(nameof(translatorsStructuresPortalTruck));
            _translatorsStructuresTruckToPortal = translatorsStructures ?? throw new ArgumentNullException(nameof(translatorsStructures));
            _compareStructuresTruckAndPortal = compareStructures ?? throw new ArgumentNullException(nameof(compareStructures));
        }

        #region Structure TruckToPortal

        /// <summary>
        /// Structures the truck to structure portal.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public async Task<ProcessDTO> StructureTruckToStructurePortal(string empId)
        {
            var process = new ProcessDTO();

            process.Start();
            process.AddLog("TRUCK - Llamado a la API");
            var truck = await _apiTruck.GetStructureTruck(empId);
            process.AddLog("TRUCK - Obtención de Estructura");

            var resource = await _dBUHResourceRepository.GetAllResource();
            var mapeoTruck = await _mapeoTableTruckPortal.GetOneBusinessTruckPortal(empId);

            if (mapeoTruck != null)
            {
                var portal = await _translatorsStructuresTruckToPortal.TruckToPortal(truck, mapeoTruck.Name, resource.ToList());
                process.AddLog("PORTAL - Transformación de Estructura");

                process.StructureId = await _truckToPortalService.MigrateEstructureAsync(portal, mapeoTruck.Name);
                process.AddLog("PORTAL - Migración de Estructura");
            }
            process.Stop();

            return await Task.Run(() => process);

        }

         /// <summary>
        /// Structures the truck to structure portal compare.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<ProcessDTO> StructureTruckToStructurePortalCompare(string code)
        {
            var process = new ProcessDTO();

            process.Start();
            process.AddLog("TRUCK - Llamado a la API");
            var truck = await _apiTruck.GetStructureTruck(code);

            process.AddLog("TRUCK - Obtención de Estructura");

            var resource = await _dBUHResourceRepository.GetAllResource();
    
            var mapeoTruck = await _mapeoTableTruckPortal.GetOneBusinessTruckPortal(code);

            if (mapeoTruck != null)
            {
                var structurePortal = await _portalService.GetAllCompareByStructureId(mapeoTruck.Name, null);
                process.AddLog("Portal - Obtención de Estructura");

                var portalDiferent = await _compareStructuresTruckAndPortal.CompareTruckToPortal(mapeoTruck.Name, truck, structurePortal, resource.ToList());
                process.AddLog("TRUCK PORTAL - Diferencia en Estructura");

                process.StructureId = await _truckToPortalService.SaveCompare(portalDiferent.StructureId, portalDiferent.UpdateNodes);
                process.AddLog("PORTAL - Guardado de nodos");
            }
            process.Stop();

            return await Task.Run(() => process);

        }

        #region Json Truck Test

        /// <summary>
        /// Structures the truck to structure portal compare.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<ProcessDTO> StructureTruckToStructurePortalCompareJson()
        {
            var process = new ProcessDTO();

            process.Start();
            process.AddLog("TRUCK - Llamado a la API");
            var truck = FactoryMock.GetMockJson<TruckStructure>(string.Format("MockFile{0}JsonTruckStructureNewNodes.json", Path.DirectorySeparatorChar));

            process.AddLog("TRUCK - Obtención de Estructura");

            var resource = await _dBUHResourceRepository.GetAllResource();
  
            var mapeoTruck = "ARGENTINA - TRUCK DEMO";

            var structurePortal = await _portalService.GetAllCompareByStructureId(mapeoTruck, null);

            process.AddLog("Portal - Obtención de Estructura");

            var portalDiferent = await _compareStructuresTruckAndPortal.CompareTruckToPortal("ARGENTINA", truck, structurePortal, resource.ToList());
            process.AddLog("TRUCK PORTAL - Diferencia en Estructura");

            process.StructureId = await _truckToPortalService.SaveCompare(portalDiferent.StructureId, portalDiferent.UpdateNodes);
            process.AddLog("PORTAL - Guardado de nodos");
            process.Stop();

            return await Task.Run(() => process);

        }

        /// <summary>
        /// Structures the truck to structure portal json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public async Task<ProcessDTO> StructureTruckToStructurePortalJson(string json)
        {
            var process = new ProcessDTO();

            process.Start();
            process.AddLog("TRUCK - Llamado a la API");

            TruckStructure truck;
            if (!string.IsNullOrEmpty(json))
            {
                truck = JsonConvert.DeserializeObject<TruckStructure>(json);
            }
            else
            {
                truck = FactoryMock.GetMockJson<TruckStructure>(string.Format("MockFile{0}JsonTruckStructure.json", Path.DirectorySeparatorChar));
            }
            process.AddLog("TRUCK - Obtención de Estructura");

            var resource = await _dBUHResourceRepository.GetAllResource();

            var portal =  await _translatorsStructuresTruckToPortal.TruckToPortal(truck, "ARGENTINA", resource.ToList());

            process.AddLog("PORTAL - Transformación de Estructura");

            process.StructureId = await _truckToPortalService.MigrateEstructureAsync(portal, "ARGENTINA - TRUCK DEMO");
            process.AddLog("PORTAL - Migración de Estructura");

            process.Stop();

            return await Task.Run(() => process);
        }

        #endregion

        #endregion

        #region Client TruckToPortal

        /// <summary>
        /// Migrations the clients truck to portal.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<ProcessDTO> MigrationClientsTruckToPortal(string empId)
        {
            var process = new ProcessDTO();

            process.Start();

            var mapeoTruck = await _mapeoTableTruckPortal.GetOneBusinessTruckPortal(empId);
            var structure = await _portalService.GetStrucureByName(mapeoTruck.Name);
            var emp = empId.ToInt();

            var CodeCountry = emp.ToString().PadLeft(2, '0') + structure.StructureModel.Country.Code;

            process.AddLog("PORTAL - Inicia Migración de Clinete");
            await StructureTruckToStructurePortalClients(structure.Id, CodeCountry, process);
            process.AddLog("PORTAL - Finalizado Migración de Clinete");

            process.Stop();

            return await Task.Run(() => process);

        }

        /// <summary>
        /// Structures the truck to structure portal client.
        /// </summary>
        /// <param name="codeLevel">The code level.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        private async Task StructureTruckToStructurePortalClients(int structureId, string CodeCountry, ProcessDTO process)
        {
            var validityFrom = DateTimeOffset.UtcNow.Today(-3);

            process.AddLog("Truck - Obteniendo datos de Truck");
            var clientes = await _mediator.Send(new GetLastDataIOQuery { Country = CodeCountry });
            var territorys = await _truckToPortalService.GetNodesTerritoryAsync(structureId);

            await _truckToPortalService.MigrateEstructureClientsAsync(clientes, validityFrom, territorys);
        }



        #endregion

        #region Structure PortalToTruck


        /// <summary>Structures the portal to truck changes.</summary>
        /// <param name="strictureID">The stricture identifier.</param>
        /// <param name="validityFrom">The date.</param>
        public async Task<ProcessDTO> StructurePortalToTruckChanges(int structureId, DateTimeOffset validityFrom)
        {
            var process = new ProcessDTO
            {
                StructureId = structureId
            };

            process.Start();
            // 1. Obtengo los datos con los cambios
            process.AddLog("PORTAL - Obtengo los datos");
            var nodes = await _truckService.GetAllNodesSentTruckAsync(validityFrom, structureId);
            var aristas = await _truckService.GetAllAristasPortalAsync(validityFrom, structureId);

            var resource = await _dBUHResourceRepository.GetAllResource();
  
            //2. Genero nueva Version
            var opeiniOut = await GenerateNewVersion(validityFrom, process);
            var logImpactTruckId = await _truckService.SetVersionedIniVersion(opeiniOut, structureId, validityFrom);

            //3. Cargo bandejas de truck
            process.AddLog("Portal - Cargo Bandejas");
            var structureTruck = await _translatorsStructuresPortalToTruck.PortalToTruckAsync(opeiniOut, structureId, nodes.ToList(), resource.ToList());

            // 4. Envio a TRuck
            await LoadTrays(process, structureTruck, logImpactTruckId);
            await LogNodeArista(logImpactTruckId, nodes.ToList(), aristas.ToList());

            //5. APR - envio version para validar y aprobar
            await ActionAPR(validityFrom, process, opeiniOut, logImpactTruckId);

            process.AddLog("TRUCK - Finalizo el envio");
            process.Stop();

            return await Task.Run(() => process);

        }

        /// <summary>
        /// Generates the new version.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        private async Task<OpecpiniOut> GenerateNewVersion(DateTimeOffset date, ProcessDTO process)
        {
            try
            {
                process.AddLog("TRUCK - Llamado a la API, metodo Opecpini - Obtener Versión nueva");
                var opeini = await _truckService.SetActionNewVersion(date, "001");
                var opeiniOut = await _apiTruck.SetOpecpini(opeini);
                return opeiniOut;
            }
            catch (Exception ex)
            {
                process.AddLog("TRUCK - Error al generar nueva version");
                throw new GenericException("Error al generar nueva version", ex);
            }

        }

        /// <summary>
        /// Actions the apr.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="process">The process.</param>
        /// <param name="opeiniOut">The opeini out.</param>
        /// <param name="logImpactTruckId">The log impact truck identifier.</param>
        /// <exception cref="GenericException">Problemas en el envio de Bandejas</exception>
        private async Task ActionAPR(DateTimeOffset date, ProcessDTO process, OpecpiniOut opeiniOut, int logImpactTruckId)
        {
            try
            {
                process.AddLog("TRUCK - Envio comando Opecpini APR");
                var opeiniAPR = await _truckService.SetActionAPR(date, opeiniOut.Epeciniin.Empresa, opeiniOut.Epeciniin.NroVer);
                var opeAPR = await _apiTruck.SetOpecpini(opeiniAPR);


                var errors = opeAPR.Msglog.Level1.Where(e => e.ECLogSts == "ERR").ToList();

                if (errors.Count > 0)
                    await _truckService.SetVersionedLog(logImpactTruckId, VersionedLogState.EBT, opeAPR);
                else
                    await _truckService.SetVersionedLog(logImpactTruckId, VersionedLogState.ACP, opeAPR);
            }
            catch (Exception ex)
            {
                await _truckService.SetVersionedLog(logImpactTruckId, VersionedLogState.EACP, ex.Message);
                throw new GenericException("Problemas en el envio de Bandejas", ex); 
            }
        }

        /// <summary>
        /// Loads the trays.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="process">The process.</param>
        /// <param name="nodes">The nodes.</param>
        /// <param name="translator">The translator.</param>
        /// <param name="opeiniOut">The opeini out.</param>
        /// <param name="logImpactTruckId">The log impact truck identifier.</param>
        /// <exception cref="GenericException">Problemas en el envio de Bandejas</exception>
        private async Task LoadTrays(ProcessDTO process, StructureTruck structureTruck, int logImpactTruckId)
        {
            try
            {
                await _apiTruck.Ptecdire(structureTruck.Ptecdire);
                await _apiTruck.Ptecarea(structureTruck.Ptecarea);
                await _apiTruck.Ptecgere(structureTruck.Ptecgere);
                await _apiTruck.Ptecregi(structureTruck.Ptecregi);
                await _apiTruck.Pteczoco(structureTruck.Pteczoco);
                await _apiTruck.Pteczona(structureTruck.Pteczona);
                await _apiTruck.Ptecterr(structureTruck.Ptecterr);

                await _truckService.SetVersionedLog(logImpactTruckId, VersionedLogState.EB, structureTruck);

                process.AddLog("TRUCK - Envio a las bandejas");
            }
            catch (Exception ex)
            {
                process.AddLog("TRUCK - Error Envio a las bandejas");
                await _truckService.SetVersionedLog(logImpactTruckId, VersionedLogState.EEB, ex.Message);
                throw new GenericException("Problemas en el envio de Bandejas", ex);
            }
        }

        /// <summary>
        /// Logs the node arista.
        /// </summary>
        /// <param name="logImpactTruckId">The log impact truck identifier.</param>
        /// <param name="nodes">The nodes.</param>
        /// <param name="aristas">The aristas.</param>
        /// <exception cref="GenericException">Problemas en el envio de Bandejas</exception>
        private async Task LogNodeArista(int logImpactTruckId, List<NodePortalTruckDTO> nodes, List<PortalAristalDTO> aristas)
        {
            try
            {
                foreach (var item in nodes)
                {
                    var ndId = item.NodeDefinitionId.HasValue ? item.NodeDefinitionId.Value : 0;

                    await _truckService.SetVersionedNode(logImpactTruckId, item.NodeId, ndId);

                    foreach (var itemChild in item.ChildNodes)
                    {
                        if (itemChild.ValidityFrom.Value == item.ValidityFrom.Value)
                        {
                            var ndChId = itemChild.NodeDefinitionId ?? 0;
                            await _truckService.SetVersionedNode(logImpactTruckId, itemChild.NodeId, ndChId);
                        }
                    }

                }

                foreach (var item in aristas)
                {
                    var aId = item.AristaId.HasValue ? item.AristaId.Value : 0;

                    await _truckService.SetVersionedArista(logImpactTruckId, aId);
                }

            }
            catch (Exception ex)
            {
                throw new GenericException("Problemas al guardar el Log", ex);
            }
        }


        #endregion

        #region Structura truck- Portal Action


        /// <summary>
        /// Gets the structure version truck status current.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        public async Task<EstructuraVersionOutput> GetStructureVersionTruckStatusCurrent(int company)
        {
            return await GetStructureVersionTruckStatus(company, null);
        }

        /// <summary>
        /// Gets the structure version truck status.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public async Task<EstructuraVersionOutput> GetStructureVersionTruckStatus(int company, string version)
        {
            try
            {
                var ini = await _truckService.GetStructureVersionTruckInput(company, version);

                var data = await _apiTruck.GetStructureVersionStatusTruck(ini);

                return await Task.Run(() => data);

            }
            catch (Exception ex)
            {
                version = !string.IsNullOrEmpty(version) ? version : "Actual";

                throw new GenericException($"Problemas para obtener la version: {version} de Truck", ex);
            }
        }

        /// <summary>
        /// Gets the pending version truck.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        public async Task<PendingVersionTruckDTO> GetPendingVersionTruck(int company, EstructuraVersionOutput versions)
        {
            var ValeDto = new PendingVersionTruckDTO();
            var users = new List<string>
            {
                "AR3ROBOT",
                "ESTCMQ1"
            };

            var codeTruck = new List<string>
            {
                "APR",
                "ING"
            };

            var versionTruck = versions.EstructuraVersiones.Level1.FirstOrDefault(l => !users.Contains(l.ECUsuAlt)
                                                        && !users.Contains(l.ECUsuApr)
                                                        && codeTruck.Contains(l.ECStsCod)
                                                        && l.ECFecDes == versions.EstructuraVersiones.Level1.Max(x => x.ECFecDes)
                                                        && l.ECTipCre != "S"
                                                        && l.ECFecTra.ToDateOffset() > DateTimeOffset.UtcNow.Date);
            if (versionTruck != null)
            {
                ValeDto.LastVersionDate = !string.IsNullOrEmpty(versionTruck.ECFecDes) ? versionTruck.ECFecDes.ToDateOffset() : null;
                ValeDto.VersionTruck = versionTruck.ECVerNro.ToString();
                ValeDto.StructureEdit = false;
                ValeDto.Message = $"Existe en Truck una versión: {ValeDto.VersionTruck} programada para fecha {ValeDto.LastVersionDate.Value:dd/MM/yyyy zzz} . La estructura no podrá ser editada en este momento. únicamente podrá visualizar a la fecha actual";
            }

            return await Task.Run(() => ValeDto);
        }

        #endregion
    }
}