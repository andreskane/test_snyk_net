using ABI.API.Structure.Application.DTO.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.DTO.Extension
{
    public static class StructureBranchNodeDTOExtensions
    {


        /// <summary>
        /// Converts to childnodesdto.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <param name="Clients">The clients.</param>
        /// <returns></returns>
        public static IStructureBranchDTO ToChildBranchNodesDTO(this List<DTO.StructureNodeDTO> result, int nodeId, int? nodeIdParent, bool? active, List<StructureClientDTO> Clients)
        {
            if (nodeId > 0)
            {
                var nodesDto = result.Where(n => n.NodeId == nodeId).ToList();

                if (nodesDto.Count == 0) return null;

                var  node = new StructureBranchNodeDTO();

                foreach (var item in nodesDto)
                {
                    nodeIdParent = !nodeIdParent.HasValue ? item.NodeParentId : nodeIdParent;

                    node = (StructureBranchNodeDTO)item.ToBranchNodeDTO(node, item.NodeId, nodeIdParent);

                    ChildBranch(result, active, Clients, node, item);
                }
                return node;
            }
            return null;
        }

        /// <summary>
        /// Childs the branch.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="active">The active.</param>
        /// <param name="Clients">The clients.</param>
        /// <param name="node">The node.</param>
        /// <param name="item">The item.</param>
        private static void ChildBranch(List<StructureNodeDTO> result, bool? active, List<StructureClientDTO> Clients, IStructureBranchDTO nodeParent, StructureNodeDTO item)
        {
            if (item.ContainsNodeId.HasValue && item.NodeId != item.ContainsNodeId)
            {
                var childNode = result.ToChildBranchNodesDTO(item.ContainsNodeId.Value, item.NodeId, active, Clients);

                if (childNode.LevelId == 8 && Clients != null)
                {
                    var childNodeTerritory = ToNodoTerritoryDTO(Clients, item, childNode);

                    childNode = childNodeTerritory;
                }

                if (childNode != null)
                    nodeParent.Nodes.Add(childNode);
            }
        }

        /// <summary>
        /// Converts to nodoterritory.
        /// </summary>
        /// <param name="Clients">The clients.</param>
        /// <param name="item">The item.</param>
        /// <param name="childNode">The child node.</param>
        /// <returns></returns>
        private static StructureBranchTerritoryDTO ToNodoTerritoryDTO(List<StructureClientDTO> Clients, StructureNodeDTO item, IStructureBranchDTO childNode)
        {
            var childNodeTerritory = new StructureBranchTerritoryDTO
            {
                Id = childNode.Id,
                StructureId = childNode.StructureId,
                NodeIdParent = childNode.NodeIdParent,
                Name = childNode.Name,
                Code = childNode.Code,
                LevelId = childNode.LevelId,
                Active = childNode.Active,
                AttentionModeId = childNode.AttentionModeId,
                RoleId = childNode.RoleId,
                SaleChannelId = childNode.SaleChannelId,
                EmployeeId = childNode.EmployeeId,
                ValidityFrom = childNode.ValidityFrom,
                ValidityTo = childNode.ValidityTo,

                Clients = Clients.Where(c => c.NodeId == childNode.Id).ToList()
            };


            if (childNode.AttentionModeId.HasValue)
                childNodeTerritory.AttentionMode = new ItemNodeDTO()
                {
                    Id = item.NodeAttentionModeId,
                    Name = item.NodeAttentionModeName
                };

            if (childNode.RoleId.HasValue)
                childNodeTerritory.Role = new ItemNodeDTO()
                {
                    Id = item.NodeRoleId,
                    Name = item.NodeRoleName
                };

            if (childNode.SaleChannelId.HasValue)
                childNodeTerritory.SaleChannel = new ItemNodeDTO()
                {
                    Id = item.NodeSaleChannelId,
                    Name = item.NodeSaleChannelName
                };
            return childNodeTerritory;
        }

        /// <summary>
        /// Converts to nodedto.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <returns></returns>
        public static IStructureBranchDTO ToBranchNodeDTO(this StructureNodeDTO item, IStructureBranchDTO node, int nodeId, int? nodeIdParent)
        {

            node.Id = item.NodeId;
            node.StructureId = item.StructureId;
            node.NodeIdParent = nodeIdParent;
            node.Name = item.NodeName;
            node.Code = item.NodeCode;
            node.LevelId = item.NodeLevelId;
            node.Active = item.NodeActive;
            node.AttentionModeId = item.NodeAttentionModeId;
            node.RoleId = item.NodeRoleId;
            node.SaleChannelId = item.NodeSaleChannelId;
            node.EmployeeId = item.NodeEmployeeId;
            node.IsRootNode = nodeId == item.RootNodeId;
            node.ValidityFrom = item.NodeValidityFrom.Value;
            node.ValidityTo = item.NodeValidityTo;

            if (item.NodeAttentionModeId.HasValue)
                node.AttentionMode = new ItemNodeDTO()
                {
                    Id = item.NodeAttentionModeId,
                    Name = item.NodeAttentionModeName
                };

            if (item.NodeRoleId.HasValue)
                node.Role = new ItemNodeDTO()
                {
                    Id = item.NodeRoleId,
                    Name = item.NodeRoleName
                };

            if (item.NodeSaleChannelId.HasValue)
                node.SaleChannel = new ItemNodeDTO()
                {
                    Id = item.NodeSaleChannelId,
                    Name = item.NodeSaleChannelName
                };

            return node;

        }

        /// <summary>
        /// Converts to childnodesdto without clients
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <returns></returns>
        public static IStructureBranchDTO ToChildBranchNodesDTO(this List<DTO.StructureNodeDTO> result, int nodeId, int? nodeIdParent, bool? active)
        {
            if (nodeId > 0)
            {
                var nodesDto = result.Where(n => n.NodeId == nodeId).ToList();

                if (nodesDto.Count == 0) return null;

                var node = new StructureBranchNodeDTO();

                foreach (var item in nodesDto)
                {
                    nodeIdParent = !nodeIdParent.HasValue ? item.NodeParentId : nodeIdParent;

                    node = (StructureBranchNodeDTO)item.ToBranchNodeDTO(node, item.NodeId, nodeIdParent);

                    ChildBranch(result, active, node, item);
                }
                return node;
            }
            return null;
        }

        /// <summary>
        /// Childs the branch without clients
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="active">The active.</param>
        /// <param name="node">The node.</param>
        /// <param name="item">The item.</param>
        private static void ChildBranch(List<StructureNodeDTO> result, bool? active, IStructureBranchDTO nodeParent, StructureNodeDTO item)
        {
            if (item.ContainsNodeId.HasValue && item.NodeId != item.ContainsNodeId)
            {
                var childNode = result.ToChildBranchNodesDTO(item.ContainsNodeId.Value, item.NodeId, active);

                if (childNode != null)
                    nodeParent.Nodes.Add(childNode);
            }
        }
    }
}
