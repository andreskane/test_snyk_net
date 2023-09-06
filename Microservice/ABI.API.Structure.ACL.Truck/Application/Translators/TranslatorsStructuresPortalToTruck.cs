using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Application.Translators.Interface;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.TruckImpact;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Helpers.Extensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class TranslatorsStructuresPortalToTruck : ITranslatorsStructuresPortalToTruck
    {
        #region Properties

        private TransformationBase LevelTransf { get; set; }
        private TransformationBase ActiveTransf { get; set; }
        private TransformationBase EmployeeTransf { get; set; }
        private TransformationBase VendorTruck { get; set; }
        private TransformationBase ShortNameTransf { get; set; }

        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IStructureNodePortalRepository _repositoryStructureNodePortal;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly IMediator _mediator;

        #endregion

        #region Builders

        public TranslatorsStructuresPortalToTruck(
             IDBUHResourceRepository dBUHResourceRepository,
            IStructureNodeRepository repositoryStructureNode,
            IMediator mediator,
            IStructureNodePortalRepository repositoryStructureNodePortal,
            IMapeoTableTruckPortal mapeoTableTruckPortal)
        {
            _dBUHResourceRepository = dBUHResourceRepository;
            _mediator = mediator;
            _repositoryStructureNodePortal = repositoryStructureNodePortal;
            _mapeoTableTruckPortal = mapeoTableTruckPortal;


            // Portal => Truck
            LevelTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.LevelPortalToTruckTransformation);
            ActiveTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodePortalToTruckTransformation);
            EmployeeTransf = new EmployeeIdPortalToTruckTransformation(_dBUHResourceRepository, _repositoryStructureNodePortal, _mediator);
            ShortNameTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.ShortNameTransformation);
            VendorTruck = StructureTranformation.InstanciateTransformation(TypeTrasformation.VendorTruckPortalToTruckTransformation);

            LevelTransf.Items = _mapeoTableTruckPortal.GetAllLevelTruckPortal().GetAwaiter().GetResult();
            VendorTruck.Items = _mapeoTableTruckPortal.GetAllTypeVendorTruckPortal().GetAwaiter().GetResult();
            

        }

        #endregion

        #region methods PortalToTruck

        /// <summary>
        /// Portals to truck.
        /// </summary>
        /// <param name="portal">The portal.</param>
        /// <returns></returns>
        public async Task<StructureTruck> PortalToTruckAsync(OpecpiniOut ini, int structureId, List<DTO.NodePortalTruckDTO> nodes, List<ResourceDTO> resources)
        {
            EmployeeTransf.Items = resources;

            var structureTruck = new StructureTruck
            {
                OpecpiniOut = ini
            };

            foreach (var item in nodes)
            {

                var levelTruck = (string) await LevelTransf.DoTransform(item.LevelId);

                switch (levelTruck)
                {
                    case "DIRECCION":
                        structureTruck.Ptecdire.Tecdire.Add(await NodeLevel1PortalToTruck(ini.Epeciniin, item));
                        break;

                    case "AREA":
                        structureTruck.Ptecarea.Tecarea.Add(await NodeLevel2PortalToTruck(ini.Epeciniin, item));
                        break;

                    case "GERENCIA":
                        structureTruck.Ptecgere.Tecgere.Add(await NodeLevel3PortalToTruck(ini.Epeciniin, item));
                        break;

                    case "REGION":
                        structureTruck.Ptecregi.Tecregi.Add(await NodeLevel4PortalToTruck(ini.Epeciniin, item));
                        break;

                    case "JEFATURA":

                        foreach (var itemChild in item.ChildNodes)
                        {

                            var zoco = structureTruck.Pteczoco.Teczoco.FirstOrDefault(p => p.ECZonId == itemChild.Code);

                            if(zoco != null)
                            {
                                structureTruck.Pteczoco.Teczoco.Remove(zoco);
                            }

                            structureTruck.Pteczoco.Teczoco.Add(await NodeLevel5PortalToTruck(ini.Epeciniin, item, itemChild));
                        }
                        break;

                    case "ZONA":
                        structureTruck.Pteczona.Teczona.Add(await NodeLevel6PortalToTruckAsync(ini.Epeciniin, structureId, item));
                        break;

                    case "TERRITORIO":
                        structureTruck.Ptecterr.Tecterr.Add(await NodeLevel7PortalToTruck(ini.Epeciniin, item));
                        break;

                    default:
                        break;
                }

            }

            return structureTruck;
        }

        /// <summary>
        /// Nodes the level1 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Etecdire> NodeLevel1PortalToTruck(Epecini ini, DTO.NodePortalTruckDTO node)
        {
            var level = new Etecdire
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECDveAbv = (string)await ShortNameTransf.DoTransform(node.Name),
                ECDveId = node.Code.ToInt(),
                ECDveIdFvt = (int)await ActiveTransf.DoTransform(node.ActiveNode),
                ECDveIdDve = (int?)await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECDveTxt = node.Name,
                ECDveUsuMo = "UserPortal",
                ECDveFecMo = node.ValidityFrom.Value.Date,
                ECDveHorMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECDveTipCr = string.Empty
            };


            return await Task.Run(() => level);
        }

        /// <summary>
        /// Nodes the level2 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Etecarea> NodeLevel2PortalToTruck(Epecini ini, DTO.NodePortalTruckDTO node)
        {
            var level = new Etecarea
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECDveId = node.ParentNodeCode.ToInt(),
                ECAveAbv = (string)await ShortNameTransf.DoTransform(node.Name),
                ECAveId = node.Code.ToInt(),
                ECAveIdFvt = (int)await ActiveTransf.DoTransform(node.ActiveNode),
                ECAveIdGea = (int?)await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECAveTxt = node.Name,
                ECAveUsuMo = "UserPortal",
                ECAveFecMo = node.ValidityFrom.Value.Date,
                ECAveHorMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECAveTipCr = string.Empty
            };


            return await Task.Run(() => level); 
        }

        /// <summary>
        /// Nodes the level3 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Etecgere> NodeLevel3PortalToTruck(Epecini ini, DTO.NodePortalTruckDTO node)
        {
            var level = new Etecgere
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECAveId = node.ParentNodeCode.ToInt(),
                ECGrcAbv = (string) await ShortNameTransf.DoTransform(node.Name),
                ECGrcId = node.Code.ToInt(),
                ECGrcIdFvt = (int) await ActiveTransf.DoTransform(node.ActiveNode),
                ECGrcIdGte = (int?) await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECGrcTxt = node.Name,
                ECGrcUsuMo = "UserPortal",
                ECGrcFecMo = node.ValidityFrom.Value.Date,
                ECGrcHorMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECGrcTipCr = string.Empty
            };


            return await Task.Run(() => level);
        }

        /// <summary>
        /// Nodes the level4 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Etecregi> NodeLevel4PortalToTruck(Epecini ini, DTO.NodePortalTruckDTO node)
        {
            var level = new Etecregi
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECGrcId = node.ParentNodeCode.ToInt(),
                ECRegAbv = (string)await ShortNameTransf.DoTransform(node.Name),
                ECRegId = node.Code.ToInt(),
                ECRegIdFvt = (int)await ActiveTransf.DoTransform(node.ActiveNode),
                ECRegIdJfe = (int?)await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECRegTxt = node.Name,
                ECRegUsuMo = "UserPortal",
                ECRegFecMo = node.ValidityFrom.Value.Date,
                ECRegHorMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECRegTipCr = string.Empty
            };


            return await Task.Run(() => level);
        }

        /// <summary>
        /// Nodes the level5 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Eteczoco> NodeLevel5PortalToTruck(Epecini ini, DTO.NodePortalTruckDTO node, DTO.NodePortalTruckDTO nodeChild)
        {
            var level = new Eteczoco
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECZonId = nodeChild.Code.ToString(),
                ECZonIdCoo = (int?)await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECZoCoUsMo = "UserPortal",
                ECZoCoFeMo = node.ValidityFrom.Value.Date,
                ECZoCoHoMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECZoCoTipCr = string.Empty
            };

            return await Task.Run(() => level);
        }

        /// <summary>
        /// Nodes the level6 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Eteczona> NodeLevel6PortalToTruckAsync(Epecini ini, int structureId, DTO.NodePortalTruckDTO node)
        {
 
            var nodeParentLevelJefatura = await _repositoryStructureNodePortal.GetNodoParent(structureId, node.NodeIdParent.Value);

 
            var level = new Eteczona
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECRegId = nodeParentLevelJefatura.ParentNodeCode.ToInt(),
                ECZonAbv = (string)await ShortNameTransf.DoTransform(node.Name),
                ECZonId = node.Code,
                ECZonIdFvt = (int)await ActiveTransf.DoTransform(node.ActiveNode),
                ECZonIdSup = (int?)await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECZonTxt = node.Name,
                ECZonUsuMo = "UserPortal",
                ECZonFecMo = node.ValidityFrom.Value.Date,
                ECZonHorMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECZonTipCr = string.Empty
            };

            return level;
        }

        /// <summary>
        /// Nodes the level7 portal to truck.
        /// </summary>
        /// <param name="ini">The ini.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private async Task<Etecterr> NodeLevel7PortalToTruck(Epecini ini, DTO.NodePortalTruckDTO node)
        {
            var level = new Etecterr
            {
                EmpId = ini.Empresa.ToInt(),
                ECVerNro = ini.NroVer.ToInt(),
                ECZonId = node.ParentNodeCode.ToString(),
                ECTrrAbv = (string) await ShortNameTransf.DoTransform(node.Name),
                ECTrrId = node.Code.ToInt(),
                ECTrrFvtId = (int?) await ActiveTransf.DoTransform(node.ActiveNode),
                VdrCod = (int?)await EmployeeTransf.DoTransform(GetLevelPortalTruck(node, LevelTransf.Items, ini.Empresa)),
                ECTrrTxt = node.Name,
                ECTrrUsuMo = "UserPortal",
                ECTrrFecMo = node.ValidityFrom.Value.Date,
                ECTrrHorMo = node.ValidityFrom.Value.TimeOfDay.ToString(),
                ECTrrTipCr = string.Empty,
                TpoVdrId = (int?)await VendorTruck.DoTransform(node)
            };

            return await Task.Run(() => level);
        }

        private static object GetLevelPortalTruck(DTO.NodePortalTruckDTO node,  object itemsLevel,  string companyId)
        {
            var item = (itemsLevel as List<LevelTruckPortal>).FirstOrDefault(l => l.LevelPortalId == node.LevelId);

            dynamic dyItem = new
            {
                item.LevelPortalId,
                item.TypeEmployeeTruck,
                CompanyId = companyId,
                Node = node
            };

            return dyItem;
        }

        #endregion
    }
}
