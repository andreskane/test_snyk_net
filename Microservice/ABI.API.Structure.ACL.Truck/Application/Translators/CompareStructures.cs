using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
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

    public class CompareStructures : ICompareStructuresTruckAndPortal
    {
        #region Properties

        private string JefatureName { get; set; }

        private TransformationBase LevelTransf { get; set; }
        private TransformationBase ActiveTransf { get; set; }
        private TransformationBase EmployeeTransf { get; set; }
        private TransformationBase StructureModelTransf { get; set; }
        private TransformationBase AttentionModeTransf { get; set; }
        private TransformationBase RoleTransf { get; set; }
        private TransformationBase RoleTerritoryTransf { get; set; }
        private TransformationBase VacantePersonTransf { get; set; }

        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;

        #endregion

        #region Builders

        public CompareStructures(IMapeoTableTruckPortal mapeoTableTruckPortal)
        {
            _mapeoTableTruckPortal = mapeoTableTruckPortal;

            // Truck => Portal
            LevelTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.LevelTruckToPortalTransformation);
            ActiveTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.ActiveNodeTruckToPortalTransformation);
            EmployeeTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.EmployeeIdTruckToPortalTransformation);
            StructureModelTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.StructureModelTruckToPortalTransformation);
            AttentionModeTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.AttentionModeTruckToPortalTransformation);
            RoleTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.RoleTruckToPortalTransformation);
            RoleTerritoryTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.RoleTerritoryTruckToPortalTransformation);
            VacantePersonTransf = StructureTranformation.InstanciateTransformation(TypeTrasformation.VacantPersonTruckToPortalTransformation);

            LevelTransf.Items = _mapeoTableTruckPortal.GetAllLevelTruckPortal().GetAwaiter().GetResult();
            StructureModelTransf.Items = _mapeoTableTruckPortal.GetAllBusinessTruckPortal().GetAwaiter().GetResult();
            RoleTransf.Items = LevelTransf.Items;
            RoleTerritoryTransf.Items = _mapeoTableTruckPortal.GetAllTypeVendorTruckPortal().GetAwaiter().GetResult();
            AttentionModeTransf.Items = RoleTerritoryTransf.Items;
       
 
        }

        #endregion

        #region Compare

        public async Task<StructurePortalCompareDTO> CompareTruckToPortal(string name, TruckStructure truck, StructureNodeDTO portal, List<ResourceDTO> resources)
        {
            EmployeeTransf.Items = resources;
            VacantePersonTransf.Items = resources;

            try
            {
                var list = new List<NodePortalCompareDTO>();

                var structure = new StructurePortalCompareDTO
                {
                    StructureModelId = (int)await StructureModelTransf.DoTransform(truck.DataStructure.EmpId),
                    StructureId = portal.Structure.Id,
                    Date = DateTimeOffset.UtcNow.ToOffset(-3) //HACER: Ojo multipaís
                };

                structure.TruckNodes = await TruckToNodePortalCompare(truck, name);
                structure.PortalNodes = PortalToNodePortalCompare(portal);

                structure.UpdateNodes = GetDifference(structure.TruckNodes, structure.PortalNodes);

                return structure;
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                return null;
            }
        }

        /// <summary>
        /// Gets the difference.
        /// </summary>
        /// <param name="truck">The truck.</param>
        /// <param name="portal">The portal.</param>
        /// <returns></returns>
        private List<NodePortalCompareDTO> GetDifference(List<NodePortalCompareDTO> truck, List<NodePortalCompareDTO> portal)
        {

            //Dif Nodos New
            var nodesNew = GetNodesNewDifference(truck, portal);

            UpdateJefatura(ref nodesNew, truck, portal);

            //Dif Data node Draft

            var nodesDraft = truck
                   .Join(portal,
                       (t) => new { t.LevelId, t.Code },
                       (s) => new { s.LevelId, s.Code },
                       (t, s) => new NodePortalCompareDTO
                       {
                           Active = t.Active,
                           Code = t.Code,
                           EmployeeId = t.EmployeeId,
                           IsRootNode = t.IsRootNode,
                           LevelId = t.LevelId,
                           Name = t.Name,
                           ParentNodeCode = t.ParentNodeCode,
                           ParentNodeLevelId = t.ParentNodeLevelId,
                           TypeActionNode = TypeActionNode.Draft,
                           RoleId = t.RoleId,
                           AttentionModeId = t.AttentionModeId
                       }
                       )
                   .Where(w =>
                   {
                       var p = portal.FirstOrDefault(a => a.Code == w.Code && a.LevelId == w.LevelId);

                       if (p == null)
                           return true;
                       else
                       {
                           var r = (p.Active != w.Active ||
                                p.EmployeeId != w.EmployeeId ||
                                p.Name != w.Name ||
                                p.RoleId != w.RoleId ||
                                p.AttentionModeId != w.AttentionModeId
                                );

                           return r;
                       }

                   })

            .ToList();

            return nodesNew.Union(nodesDraft).ToList();
        }

        #endregion

        #region methods TruckToPortal

        /// <summary>
        /// Updates the jefatura.
        /// </summary>
        /// <param name="nodesNew">The nodes new.</param>
        /// <param name="truck">The truck.</param>
        /// <param name="portal">The portal.</param>
        private void UpdateJefatura(ref List<NodePortalCompareDTO> nodesNew, List<NodePortalCompareDTO> truck, List<NodePortalCompareDTO> portal)
        {

            var levelIdJefature = 6;
            var LevelIdZone = 7;

            //Solo Jefaturas
            var list = nodesNew.Where(n => n.LevelId == levelIdJefature).ToList();


            foreach (var item in list)
            {
                var ZoneTruck = truck.Where(t => t.LevelId == LevelIdZone && t.ParentNodeCode == item.Code).ToList();
                var codelist = ZoneTruck.Select(z => z.Code).ToList();

                var ZonePortal = portal.Where(p => p.LevelId == LevelIdZone && codelist.Contains(p.Code)).ToList();
                var nodesJefatureNew = GetNodesNewDifference(ZoneTruck, ZonePortal);

                if (nodesJefatureNew.Count == 0)
                {
                    var zonaP = ZonePortal.FirstOrDefault();

                    var jefaturePortal = portal.FirstOrDefault(p => p.LevelId == levelIdJefature && p.Code == zonaP.ParentNodeCode);

                    if (jefaturePortal != null)
                    {
                        var jefaturePortalTruck = truck.FirstOrDefault(n => n.LevelId == levelIdJefature && n.Code == item.Code);
                        jefaturePortalTruck.Code = jefaturePortal.Code;

                        foreach (var itemZ in ZoneTruck)
                        {
                            itemZ.ParentNodeCode = jefaturePortal.Code;
                        }
                        nodesNew.Remove(item);

                    }
                }
            }
        }

        /// <summary>
        /// Gets the nodes new.
        /// </summary>
        /// <param name="ZoneTruck">The zone truck.</param>
        /// <param name="ZonePortal">The zone portal.</param>
        /// <returns></returns>
        private List<NodePortalCompareDTO> GetNodesNewDifference(List<NodePortalCompareDTO> ZoneTruck, List<NodePortalCompareDTO> ZonePortal)
        {
            var nodesNew = ZoneTruck.Where(t => !ZonePortal.Any(p => p.Code == t.Code && p.LevelId == t.LevelId))
                                    .Select(n => new NodePortalCompareDTO
                                    {
                                        Active = n.Active,
                                        Code = n.Code,
                                        EmployeeId = n.EmployeeId,
                                        IsRootNode = n.IsRootNode,
                                        LevelId = n.LevelId,
                                        Name = n.Name,
                                        ParentNodeCode = n.ParentNodeCode,
                                        ParentNodeLevelId = n.ParentNodeLevelId,
                                        TypeActionNode = TypeActionNode.New,
                                        RoleId = n.RoleId,
                                        AttentionModeId = n.AttentionModeId
                                    }
                                            ).ToList();

            return nodesNew;
        }

        /// <summary>
        /// Trucks to portal.
        /// </summary>
        /// <param name="truck">The truck.</param>
        /// <returns></returns>
        private async Task<List<NodePortalCompareDTO>> TruckToNodePortalCompare(TruckStructure truck, string name)
        {
            var list = new List<NodePortalCompareDTO>();

            var nodes = truck.DataStructure.Level1.FirstOrDefault(l => l.DveTxt == name);

            await NodeLevel1TruckToPortal(nodes, list);

            return list;
        }

        /// <summary>
        /// Nodes the level1.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task NodeLevel1TruckToPortal(DataStructureLevel1 level, List<NodePortalCompareDTO> listNode)
        {
            var node = new NodePortalCompareDTO
            {
                Name = level.DveTxt.ToUpper().Trim(),
                Code = level.DveId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("DIRECCION"),
                IsRootNode = true,
                Active = (bool)await ActiveTransf.DoTransform(level.DveIdFvt),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("DIRECCION", LevelTransf.Items, level.DveIdDve)),
                RoleId = (int?)await RoleTransf.DoTransform("DIRECCION"),
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("DIRECCION", LevelTransf.Items, level.DveIdDve))

            };

            listNode.Add(node);

            foreach (var item in level.Level2)
            {
                await NodeLevel2TruckToPortal(item, listNode, node.Code, node.LevelId);
            }
        }

        /// <summary>
        /// Nodes the level2.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task NodeLevel2TruckToPortal(DataStructureLevel2 level, List<NodePortalCompareDTO> listNode, string parentNodeCode, int parentNodeLevelId)
        {
            var node = new NodePortalCompareDTO
            {
                Name = level.AveTxt.ToUpper().Trim(),
                Code = level.AveId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("AREA"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.AveIdFvt),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("AREA", LevelTransf.Items, level.AveIdGea)),
                RoleId = (int?)await RoleTransf.DoTransform("AREA"),
                ParentNodeCode = parentNodeCode,
                ParentNodeLevelId = parentNodeLevelId,
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("AREA", LevelTransf.Items, level.AveIdGea))
            };

            listNode.Add(node);

            foreach (var item in level.Level3)
            {
                await NodeLevel3TruckToPortal(item, listNode, node.Code, node.LevelId);
            }

        }

        /// <summary>
        /// Nodes the level3.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task NodeLevel3TruckToPortal(DataStructureLevel3 level, List<NodePortalCompareDTO> listNode, string parentNodeCode, int parentNodeLevelId)
        {
            var node = new NodePortalCompareDTO
            {
                Name = level.GrcTxt.ToUpper().Trim(),
                Code = level.GrcId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("GERENCIA"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.GrcIdFvt),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("GERENCIA", LevelTransf.Items, level.GrcIdGte)),
                RoleId = (int?)await RoleTransf.DoTransform("GERENCIA"),
                ParentNodeCode = parentNodeCode,
                ParentNodeLevelId = parentNodeLevelId,
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("GERENCIA", LevelTransf.Items, level.GrcIdGte))
            };

            listNode.Add(node);

            foreach (var item in level.Level4)
            {
                await NodeLevel4TruckToPortal(item, listNode, node.Code, node.LevelId);
            }
        }

        /// <summary>
        /// Nodes the level4.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task NodeLevel4TruckToPortal(DataStructureLevel4 level, List<NodePortalCompareDTO> listNode, string parentNodeCode, int parentNodeLevelId)
        {
            var node = new NodePortalCompareDTO
            {
                Name = level.RegTxt.ToUpper().Trim(),
                Code = level.RegId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("REGION"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.RegIdFvt),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("REGION", LevelTransf.Items, level.RegIdJfe)),
                RoleId = (int?)await RoleTransf.DoTransform("REGION"),
                ParentNodeCode = parentNodeCode,
                ParentNodeLevelId = parentNodeLevelId,
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("REGION", LevelTransf.Items, level.RegIdJfe))
            };

            listNode.Add(node);

            NodePortalCompareDTO node5 = null;

            foreach (var item in level.Level5.OrderBy(o => o.CooNom).ToList())
            {
                if (JefatureName != item.CooNom.ToUpper().Trim())
                {

                    var activeNode5 = true;
                    var activeCodeNode5 = level.Level5.FirstOrDefault(l => l.CooNom == item.CooNom && l.ZonIdFvt == "200");

                    if (activeCodeNode5 == null)
                        activeNode5 = false;

                    node5 = await NodeLevels5TruckToPortal(item, listNode, node.Code, node.LevelId, activeNode5);
                    JefatureName = item.CooNom.ToUpper().Trim();
                }

                if (node5 != null)
                    await NodeLevels6TruckToPortal(item, listNode, node5.Code, node5.LevelId);
            }

            JefatureName = string.Empty;

        }

        /// <summary>
        /// Nodes the level5.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task<NodePortalCompareDTO> NodeLevels5TruckToPortal(DataStructureLevel5 level, List<NodePortalCompareDTO> listNode, string parentNodeCode, int parentNodeLevelId, bool parentActive)
        {

            var node5 = new NodePortalCompareDTO
            {
                Name = level.CooNom.ToUpper().Trim(),
                Code = $"{level.ZonIdCoor.Trim()}_{parentNodeCode}",
                LevelId = (int)await LevelTransf.DoTransform("JEFATURA"),
                IsRootNode = false,
                Active = parentActive,
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("JEFATURA", LevelTransf.Items, level.ZonIdCoor)),
                RoleId = (int?)await RoleTransf.DoTransform("JEFATURA"),
                ParentNodeCode = parentNodeCode,
                ParentNodeLevelId = parentNodeLevelId,
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("JEFATURA", LevelTransf.Items, level.ZonIdCoor))
            };

            listNode.Add(node5);

            return node5;
        }


        /// <summary>
        /// Nodes the level5.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task NodeLevels6TruckToPortal(DataStructureLevel5 level, List<NodePortalCompareDTO> listNode, string parentNodeCode, int parentNodeLevelId)
        {


            #region Nodo Nivel 6 - Zona

            var node6 = new NodePortalCompareDTO
            {
                Name = level.ZonTxt.ToUpper().Trim(),
                Code = level.ZonId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("ZONA"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.ZonIdFvt),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("ZONA", LevelTransf.Items, level.ZonIdSup)),
                RoleId = (int?)await RoleTransf.DoTransform("ZONA"),
                ParentNodeCode = parentNodeCode,
                ParentNodeLevelId = parentNodeLevelId,
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("ZONA", LevelTransf.Items, level.ZonIdSup))
            };


            listNode.Add(node6);

            #endregion

            foreach (var item in level.Level6)
            {
                await NodeLevel7TruckToPortal(item, listNode, node6.Code, node6.LevelId);
            }

        }

        /// <summary>
        /// Nodes the level6.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="validity">The validity.</param>
        /// <returns></returns>
        private async Task NodeLevel7TruckToPortal(DataStructureLevel6 level, List<NodePortalCompareDTO> listNode, string parentNodeCode, int parentNodeLevelId)
        {
            var node = new NodePortalCompareDTO
            {
                Name = level.TrrTxt.ToUpper().Trim(),
                Code = level.TrrId.Trim(),
                LevelId = (int)await LevelTransf.DoTransform("TERRITORIO"),
                IsRootNode = false,
                Active = (bool)await ActiveTransf.DoTransform(level.TrrIdFvt),
                EmployeeId = (int?)await EmployeeTransf.DoTransform(GetLevelTRuckPortal("TERRITORIO", LevelTransf.Items, level.VdrCod)),
                AttentionModeId = (int?)await AttentionModeTransf.DoTransform(level.TpoVdrId),
                RoleId = (int?)await RoleTerritoryTransf.DoTransform(level.TpoVdrId),
                ParentNodeCode = parentNodeCode,
                ParentNodeLevelId = parentNodeLevelId,
                VacantPerson = (bool)await VacantePersonTransf.DoTransform(GetLevelTRuckPortal("TERRITORIO", LevelTransf.Items, level.VdrCod))
            };

            listNode.Add(node);
        }


        /// <summary>
        /// Gets the level t ruck portal.
        /// </summary>
        /// <param name="leveltruck">The leveltruck.</param>
        /// <param name="items">The items.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        private object GetLevelTRuckPortal(string leveltruck, object items, string employeeId)
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

        #region Portal

        /// <summary>
        /// Portals to node portal compare.
        /// </summary>
        /// <param name="portal">The portal.</param>
        /// <returns></returns>
        private List<NodePortalCompareDTO> PortalToNodePortalCompare(StructureNodeDTO portal)
        {
            var list = new List<NodePortalCompareDTO>();

            NodeLevelPortal(portal.Nodes, list, null, null);

            return list;
        }

        /// <summary>
        /// Nodes the level portal.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="listNode">The list node.</param>
        /// <param name="parentNodeCode">The parent node code.</param>
        private void NodeLevelPortal(List<NodeDTO> nodes, List<NodePortalCompareDTO> listNode, string parentNodeCode, int? parentNodeLevelId)
        {

            foreach (var item in nodes)
            {
                var node = new NodePortalCompareDTO
                {
                    Name = item.Name.ToUpper().Trim(),
                    Code = item.Code,
                    LevelId = item.LevelId,
                    IsRootNode = false,
                    Active = item.Active.Value,
                    EmployeeId = item.EmployeeId,
                    RoleId = item.RoleId,
                    AttentionModeId = item.AttentionModeId,
                    ParentNodeCode = parentNodeCode,
                    ParentNodeLevelId = parentNodeLevelId
                };

                if (listNode.Count == 0)
                {
                    node.IsRootNode = true;
                }

                listNode.Add(node);

                NodeLevelPortal(item.Nodes, listNode, node.Code, node.LevelId);

            }
        }

        #endregion

    }
}
