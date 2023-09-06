using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class TruckService : ITruckService
    {
        private readonly IStructureNodePortalRepository _structureNodePortalRepository;
        private readonly IVersionedAristaRepository _versionedAristaRepository;
        private readonly IVersionedNodeRepository _VersionedNodeRepository;
        private readonly IVersionedRepository _VersionedRepository;
        private readonly IVersionedLogRepository _VersionedLogRepository;

        public TruckService()
        { }

        public TruckService(
            IStructureNodePortalRepository structureNodePortalRepository,
            IVersionedAristaRepository versionedAristaRepository,
            IVersionedNodeRepository versionedNodeRepository,
            IVersionedRepository versionedRepository,
            IVersionedLogRepository versionedLogRepository
        )
        {
            _structureNodePortalRepository = structureNodePortalRepository ?? throw new ArgumentNullException(nameof(structureNodePortalRepository));

            _VersionedRepository = versionedRepository;
            _versionedAristaRepository = versionedAristaRepository;
            _VersionedNodeRepository = versionedNodeRepository;
            _VersionedLogRepository = versionedLogRepository;
          

        }

        #region Action Truck



        /// <summary>
        /// Gets the new version structure truck.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <returns></returns>
        public Task<OpecpiniInput> SetActionNewVersion(DateTimeOffset validityFrom, string companyTruckId)
            => GetTypeProcessTruck(TypeProcessTruck.NEW, validityFrom, companyTruckId, null);

        /// <summary>
        /// Sets the action apr structure truck.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public Task<OpecpiniInput> SetActionAPR(DateTimeOffset validityFrom, string companyTruckId, string version)
            => GetTypeProcessTruck(TypeProcessTruck.APR, validityFrom, companyTruckId, version);

        /// <summary>
        /// Sets the action ope.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public Task<OpecpiniInput> SetActionOPE(DateTimeOffset validityFrom, string companyTruckId, string version)
            => GetTypeProcessTruck(TypeProcessTruck.OPE, validityFrom, companyTruckId, version);

        /// <summary>
        /// Sets the action RCH.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public Task<OpecpiniInput> SetActionRCH(DateTimeOffset validityFrom, string companyTruckId, string version)
            => GetTypeProcessTruck(TypeProcessTruck.RCH, validityFrom, companyTruckId, version);

        /// <summary>
        /// Sets the action upd.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public Task<OpecpiniInput> SetActionUPD(DateTimeOffset validityFrom, string companyTruckId, string version)
            => GetTypeProcessTruck(TypeProcessTruck.UPD, validityFrom, companyTruckId, version);

        /// <summary>
        /// Sets the action FCH.
        /// </summary>
        /// <param name="newDate">The new date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public Task<OpecpiniInput> SetActionFCH(DateTimeOffset newValidityFrom, string companyTruckId, string version)
            => GetTypeProcessTruck(TypeProcessTruck.FCH, newValidityFrom, companyTruckId, version);

        /// <summary>Gets the type process truck.</summary>
        /// <param name="process">The process.</param>
        /// <param name="date">The date.</param>
        /// <param name="companyTruckId">The company truck identifier.</param>
        /// <param name="version">The version.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<OpecpiniInput> GetTypeProcessTruck(TypeProcessTruck process, DateTimeOffset validityFrom, string companyTruckId, string version)
        {
            var ini = new OpecpiniInput();

            ini.Epeciniin.TipoProceso = process.ToString();
            ini.Epeciniin.NroVer = string.Empty;
            ini.Epeciniin.LogSts = string.Empty;

            switch (process)
            {
                case TypeProcessTruck.NEW:
                    ini.Epeciniin.Empresa = companyTruckId;
                    ini.Epeciniin.FechaDesde = validityFrom.Date.ToString("yy/MM/dd");
                    ini.Epeciniin.FechaHasta = DateTimeOffset.MaxValue.ToString("yy/MM/dd");

                    break;
                case TypeProcessTruck.APR:
                case TypeProcessTruck.UPD:
                case TypeProcessTruck.RCH:
                    ini.Epeciniin.Empresa = companyTruckId;
                    ini.Epeciniin.FechaDesde = validityFrom.Date.ToString("yy/MM/dd");
                    ini.Epeciniin.FechaHasta = DateTimeOffset.MaxValue.ToString("yy/MM/dd");
                    ini.Epeciniin.NroVer = version;
                    break;
                case TypeProcessTruck.OPE:
                    // No se envia Fecha desde y hasta por que truck lo resuelve,
                    //y se impacta en el momento
                    ini.Epeciniin.NroVer = version;
                    break;
                case TypeProcessTruck.FCH:
                    ini.Epeciniin.FechaDesde = validityFrom.Date.ToString("yy/MM/dd");
                    ini.Epeciniin.FechaHasta = "39/12/31";
                    ini.Epeciniin.NroVer = version;
                    break;
            }

            return Task.Run(() => ini);
        }

        /// <summary>
        /// Gets the structure version truck input.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public Task<EstructuraVersionInput> GetStructureVersionTruckInput(int company, string version)
        {
            version = string.IsNullOrEmpty(version) ? "000000" : version;
            //000000 trae la ultima version de truck

            var ini = new EstructuraVersionInput { EmpId = company, ECVerNro = version };

            return Task.Run(() => ini);
        }

        #endregion


        #region Data Portal 

        /// <summary>
        /// Gets all nodes sent truck asynchronous.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="structureId">The structure identifier.</param>
        /// <returns></returns>
        public async Task<IList<NodePortalTruckDTO>> GetAllNodesSentTruckAsync(DateTimeOffset validityFrom, int structureId)
        {
            var nodes = await _structureNodePortalRepository.GetAllGradeChangesForTruck(structureId, validityFrom);

            foreach (var item in nodes)
            {

                switch (item.LevelId)
                {
                    case 6: //Jefatura
                        var child = await _structureNodePortalRepository.GetAllChildNodeForTruck(structureId, item.NodeId, validityFrom);

                        item.ChildNodes = child.ToList();

                        break;
                }


            }



            return nodes;


        }

        /// <summary>
        /// Gets all aristas portal asynchronous.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="structureId">The structure identifier.</param>
        /// <returns></returns>
        public async Task<IList<PortalAristalDTO>> GetAllAristasPortalAsync(DateTimeOffset validityFrom, int structureId)
        {
            return await _structureNodePortalRepository.GetAllAristasGradeChangesForTruck(structureId, validityFrom);
        }

        #endregion

        #region Versioned

        public async Task<Versioned> SetVersionedNew( int strictureId, DateTimeOffset validityFrom, string user)
        {
            var impactVersioned = new Versioned
            {
                StructureId = strictureId,
                Date = DateTimeOffset.UtcNow,
                Version = null,
                Validity = validityFrom,
                StatusId = (int)VersionedState.PendienteDeEnvio,
                User = user
            };

            var id = await _VersionedRepository.Create(impactVersioned);

            await SetVersionedLog(id, VersionedLogState.EPE, impactVersioned);

            return impactVersioned;
        }

        /// <summary>
        /// Sets the versioned ini.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="strictureId">The stricture identifier.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        public async Task<int> SetVersionedIniVersion(OpecpiniOut ini, int strictureId, DateTimeOffset validityFrom)
        {
            var impactVersioned = new Versioned
            {
                StructureId = strictureId,
                Date = DateTimeOffset.UtcNow,
                Version = ini.Epeciniin.NroVer,
                Validity = validityFrom,
                StatusId = (int)VersionedState.PendienteDeEnvio,
                User = ""


            };

            var id = await _VersionedRepository.Create(impactVersioned);

            await SetVersionedLog(id, VersionedLogState.CNV, ini);

            return id;
        }

        /// <summary>
        /// Sets the versioned log.
        /// </summary>
        /// <param name="versionedId">The versioned identifier.</param>
        /// <param name="state">The state.</param>
        /// <param name="objectValue">The object value.</param>
        public async Task SetVersionedLog(int versionedId, VersionedLogState state, object objectValue)
        {
            var impactVersionedLog = new VersionedLog
            {
                VersionedId = versionedId,
                Date = DateTimeOffset.UtcNow,
                LogStatusId = (int)state,
                Detaill = JsonConvert.SerializeObject(objectValue)
            };

            await _VersionedLogRepository.Create(impactVersionedLog);
        }

        /// <summary>
        /// Sets the versioned log.
        /// </summary>
        /// <param name="versionedId">The versioned identifier.</param>
        /// <param name="state">The state.</param>
        /// <param name="text">The text.</param>
        public async Task SetVersionedLog(int versionedId, VersionedLogState state, string text)
        {
            var impactVersionedLog = new VersionedLog
            {
                VersionedId = versionedId,
                Date = DateTimeOffset.UtcNow,
                LogStatusId = (int)state,
                Detaill = text
            };

            await _VersionedLogRepository.Create(impactVersionedLog);
        }

        /// <summary>
        /// Sets the versioned node.
        /// </summary>
        /// <param name="versionedId">The versioned identifier.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeDefinitionId">The node definition identifier.</param>
        public async Task SetVersionedNode(int versionedId, int nodeId, int nodeDefinitionId)
        {
            var impactVersionedNode = new VersionedNode
            {
                VersionedId = versionedId,
                NodeId = nodeId,
                NodeDefinitionId = nodeDefinitionId
            };
            //todo:esto llevarlo a nivel de repositorio y sacarlo del generic
            await _VersionedNodeRepository.Create(impactVersionedNode);
        }

        /// <summary>
        /// Sets the versioned arista.
        /// </summary>
        /// <param name="versionedId">The versioned identifier.</param>
        /// <param name="aristaId">The arista identifier.</param>
        public async Task SetVersionedArista(int versionedId, int aristaId)
        {
            var impactVersionedArista = new VersionedArista
            {
                VersionedId = versionedId,
                AristaId = aristaId
            };

            await _versionedAristaRepository.Create(impactVersionedArista);
        }

        #endregion
    }
}
