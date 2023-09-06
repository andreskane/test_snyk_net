using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using ABI.API.Structure.ACL.Truck.Application.DTO.Resource;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.API.Structure.ACL.Truck.Application.Transformations.Interface;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public class TranslatorsStructuresTruckToPortal : ITranslatorsStructuresTruckToPortal
    {
        #region Properties

        private string JefatureName { get; set; }
        private NodePortalDTO JefatureNode { get; set; }

        private TransformationBase LevelTransf { get; set; }
        private TransformationBase ActiveTransf { get; set; }
        private TransformationBase EmployeeTransf { get; set; }
        private TransformationBase AttentionModeTransf { get; set; }
        private TransformationBase RoleTransf { get; set; }
        private TransformationBase RoleTerritoryTransf { get; set; }
        private TransformationBase StructureModelTransf { get; set; }
        private TransformationBase VacantePersonTransf { get; set; }

        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;

        #endregion

        #region Builders

        public TranslatorsStructuresTruckToPortal(

            IMapeoTableTruckPortal mapeoTableTruckPortal)
        {

            _mapeoTableTruckPortal = mapeoTableTruckPortal;

            // Truck => Portal
            LevelTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.LevelTruckToPortalTransformation);
            ActiveTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodeTruckToPortalTransformation);
            EmployeeTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.EmployeeIdTruckToPortalTransformation);
            AttentionModeTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.AttentionModeTruckToPortalTransformation);
            RoleTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.RoleTruckToPortalTransformation);
            RoleTerritoryTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.RoleTerritoryTruckToPortalTransformation);
            StructureModelTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.StructureModelTruckToPortalTransformation);
            VacantePersonTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.VacantPersonTruckToPortalTransformation);

            LevelTransf.Items = _mapeoTableTruckPortal.GetAllLevelTruckPortal().GetAwaiter().GetResult();
            StructureModelTransf.Items = _mapeoTableTruckPortal.GetAllBusinessTruckPortal().GetAwaiter().GetResult();
            RoleTransf.Items = LevelTransf.Items;
            RoleTerritoryTransf.Items = _mapeoTableTruckPortal.GetAllTypeVendorTruckPortal().GetAwaiter().GetResult();
            AttentionModeTransf.Items = RoleTerritoryTransf.Items;
                   
        }

        #endregion

        #region methods TruckToPortal

        /// <summary>
        /// Trucks to portal.
        /// </summary>
        /// <param name="truck">The truck.</param>
        /// <returns></returns>
        public async Task<StructurePortalDTO> TruckToPortal(TruckStructure truck, string name, List<ResourceDTO> resources)
        {

            EmployeeTransf.Items = resources;
            VacantePersonTransf.Items = resources;

            var structura = new StructurePortalDTO
            {
                StructureModelId = (int)await StructureModelTransf.DoTransform(truck.DataStructure.EmpId),
            };
            structura.ValidityFrom = DateTimeOffset.UtcNow.Today(-3);

            var truckNode = truck.DataStructure.Level1.FirstOrDefault(l => l.DveTxt.ToUpper() == name);

            structura.Nodes.Add(await NodeLevel1TruckToPortal(truckNode, structura.ValidityFrom));

            return structura;
        }

        /// <summary>
        /// Nodes the level1.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevel1TruckToPortal(DataStructureLevel1 level, DateTimeOffset? validityFrom = null)
        {
            var node = new NodePortalDTO
            {
                Name = level.DveTxt.ToUpper().Trim(),
                Code = level.DveId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("DIRECCION"),
                IsRootNode = true,
                Active = (bool)await ActiveTransf.DoTransform(level.DveIdFvt),
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                RoleId = (int?)await RoleTransf.DoTransform("DIRECCION"),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("DIRECCION", LevelTransf.Items, level.DveIdDve)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("DIRECCION", LevelTransf.Items, level.DveIdDve))
            };

            if (validityFrom.HasValue)
                node.ValidityFrom = validityFrom.Value;

            foreach (var item in level.Level2)
            {
                node.Nodes.Add(await NodeLevel2TruckToPortal(item, validityFrom));
            }


            return node;
        }

        /// <summary>
        /// Nodes the level2.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevel2TruckToPortal(DataStructureLevel2 level, DateTimeOffset? validityFrom = null)
        {
            var node = new NodePortalDTO
            {
                Name = level.AveTxt.ToUpper().Trim(),
                Code = level.AveId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("AREA"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.AveIdFvt),
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                RoleId = (int?)await RoleTransf.DoTransform("AREA"),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("AREA", LevelTransf.Items, level.AveIdGea)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("AREA", LevelTransf.Items, level.AveIdGea))
            };

            if (validityFrom.HasValue)
                node.ValidityFrom = validityFrom.Value;

            foreach (var item in level.Level3)
            {
                node.Nodes.Add(await NodeLevel3TruckToPortal(item, validityFrom));
            }


            return node;
        }

        /// <summary>
        /// Nodes the level3.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevel3TruckToPortal(DataStructureLevel3 level, DateTimeOffset? validityFrom = null)
        {
            var node = new NodePortalDTO
            {
                Name = level.GrcTxt.ToUpper().Trim(),
                Code = level.GrcId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("GERENCIA"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.GrcIdFvt),
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                RoleId = (int?)await RoleTransf.DoTransform("GERENCIA"),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("GERENCIA", LevelTransf.Items, level.GrcIdGte)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("GERENCIA", LevelTransf.Items, level.GrcIdGte))
            };

            if (validityFrom.HasValue)
                node.ValidityFrom = validityFrom.Value;

            foreach (var item in level.Level4)
            {
                node.Nodes.Add(await NodeLevel4TruckToPortal(item, validityFrom));
            }


            return node;
        }

        /// <summary>
        /// Nodes the level4.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevel4TruckToPortal(DataStructureLevel4 level, DateTimeOffset? validityFrom = null)
        {
            var node = new NodePortalDTO
            {
                Name = level.RegTxt.ToUpper().Trim(),
                Code = level.RegId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("REGION"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.RegIdFvt),
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                RoleId = (int?)await RoleTransf.DoTransform("REGION"),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("REGION", LevelTransf.Items, level.RegIdJfe)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("REGION", LevelTransf.Items, level.RegIdJfe))
            };

            if (validityFrom.HasValue)
                node.ValidityFrom = validityFrom.Value;

            NodePortalDTO node5 = null;



            foreach (var item in level.Level5.OrderBy(o => o.CooNom).ToList())
            {
                if (JefatureName != item.CooNom.ToUpper().Trim())
                {
                    if (node5 != null)
                    {
                        node.Nodes.Add(node5);
                    }

                    var activeNode5 = true;
                    var activeCodeNode5 = level.Level5.FirstOrDefault(l => l.CooNom == item.CooNom && l.ZonIdFvt == "200");

                    if (activeCodeNode5 == null)
                        activeNode5 = false;

                    node5 = await NodeLevels5TruckToPortal(item, activeNode5, node.Code, validityFrom);
                    JefatureNode = node5;
                    JefatureName = item.CooNom.ToUpper().Trim();
                }
                else
                {
                    node5 = JefatureNode;
                }

                node5.Nodes.Add(await NodeLevels6TruckToPortal(item, validityFrom));

            }

            if (node5 != null)
                node.Nodes.Add(node5);

            JefatureNode = null;
            JefatureName = string.Empty;

            return node;
        }

        /// <summary>
        /// Nodes the level5.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevels5TruckToPortal(DataStructureLevel5 level, bool activeParent, string parentNodeCode, DateTimeOffset? validityFrom = null)
        {
            var node5 = new NodePortalDTO
            {
                Name = level.CooNom.ToUpper().Trim(),
                Code = $"{level.ZonIdCoor.Trim()}_{parentNodeCode}",
                LevelId = (int)await LevelTransf.DoTransform("JEFATURA"),
                IsRootNode = false,
                Active = activeParent,
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                RoleId = (int?)await RoleTransf.DoTransform("JEFATURA"),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("JEFATURA", LevelTransf.Items, level.ZonIdCoor)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("JEFATURA", LevelTransf.Items, level.ZonIdCoor))
            };


            if (validityFrom.HasValue)
                node5.ValidityFrom = validityFrom.Value;

            return node5;

        }


        /// <summary>
        /// Nodes the level5.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevels6TruckToPortal(DataStructureLevel5 level, DateTimeOffset? validityFrom = null)
        {


            #region Nodo Nivel 6 - Zona

            var node6 = new NodePortalDTO
            {
                Name = level.ZonTxt.ToUpper().Trim(),
                Code = level.ZonId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("ZONA"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.ZonIdFvt),
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                RoleId = (int?)await RoleTransf.DoTransform("ZONA"),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("ZONA", LevelTransf.Items, level.ZonIdSup)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("ZONA", LevelTransf.Items, level.ZonIdSup))
            };

            if (validityFrom.HasValue)
                node6.ValidityFrom = validityFrom.Value;

            #endregion


            foreach (var item in level.Level6)
            {
                node6.Nodes.Add(await NodeLevel7TruckToPortal(item, node6.ValidityFrom));
            }


            return node6;

        }

        /// <summary>
        /// Nodes the level6.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validityFrom">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalDTO> NodeLevel7TruckToPortal(DataStructureLevel6 level, DateTimeOffset? validityFrom = null)
        {

            var node = new NodePortalDTO
            {
                Name = level.TrrTxt.ToUpper().Trim(),
                Code = level.TrrId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("TERRITORIO"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.TrrIdFvt),
                ValidityFrom = validityFrom,
                ValidityTo = DateTimeOffset.MaxValue.ToOffset(-3),
                AttentionModeId = (int?)await AttentionModeTransf.DoTransform(level.TpoVdrId),
                RoleId = (int?)await RoleTerritoryTransf.DoTransform(level.TpoVdrId),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTruckPortal("TERRITORIO", LevelTransf.Items, level.VdrCod)),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTruckPortal("TERRITORIO", LevelTransf.Items, level.VdrCod))
            };

            if (validityFrom.HasValue)
                node.ValidityFrom = validityFrom.Value;

            return node;
        }


        /// <summary>
        /// Gets the level t ruck portal.
        /// </summary>
        /// <param name="leveltruck">The leveltruck.</param>
        /// <param name="items">The items.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        private object GetLevelTruckPortal(string leveltruck, object items, string employeeId)
        {
            var item = (items as List<LevelTruckPortal>).FirstOrDefault(l => l.LevelTruckName == leveltruck);

            dynamic dyItem = new
            {
                item.LevelPortalId,
                item.TypeEmployeeTruck,
                EmployeeId = employeeId
            };

            return dyItem;
        }


        #endregion
    }
}
