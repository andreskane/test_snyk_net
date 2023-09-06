using ABI.API.Structure.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.DTO.Extension
{
    public static class StructureNodeDTOExtensions
    {
        /// <summary>
        /// Converts to nodedto.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static DTO.NodeDTO ToNodeDTO(this DTO.StructureNodeDTO result, DateTimeOffset validityFrom)
        {
            var list = new List<DTO.StructureNodeDTO>
            {
                result
            };

            return list.ToChildNodesDTO(validityFrom, result.NodeId, result.NodeParentId, result.NodeActive);
        }

        /// <summary>
        /// Converts to nodedto.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <returns></returns>
        public static NodeDTO ToNodeDTO(this StructureNodeDTO item, int nodeId, int? nodeIdParent)
        {
            if (item == null)
                return null;

            var node = new NodeDTO();

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
            node.IsRootNode = !node.NodeIdParent.HasValue;
            node.ValidityFrom = item.NodeValidityFrom;
            node.ValidityTo = item.NodeValidityTo;
            node.VersionType = item.VersionType;

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
        /// Converts to childnodesdto.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <param name="active">The active.</param>
        /// <returns></returns>
        public static NodeDTO ToChildNodesDTO(this List<DTO.StructureNodeDTO> result, DateTimeOffset validityFrom, int nodeId, int? nodeIdParent, bool? active)
        {
            if (nodeId > 0)
            {
                var nodesDto = result.Where(n => n.NodeId == nodeId).ToList();

                if (!nodesDto.Any())
                    return null;

                var node = nodesDto.FirstOrDefault(w =>
                    w.IsNew
                    ||
                    (
                        w.VersionType != null && w.VersionType.Equals("N")
                    )
                    ||
                    (
                        w.AristaMotiveStateId == null
                        ||
                        (
                            w.AristaMotiveStateId == (int)MotiveStateNode.Confirmed &&
                            w.NodeMotiveStateId == (int)MotiveStateNode.Confirmed
                        )
                    )
                )
                ?.ToNodeDTO(nodeId, nodeIdParent);

                if (node == null)
                    return null;

                var parent = result.FirstOrDefault(f => f.NodeId == nodeIdParent && f.ContainsNodeId == nodeId);

                if (node.VersionType == "F" && parent != null && parent.AristaValidityFrom > validityFrom)
                    return null;

                foreach (var item in nodesDto)
                {
                    if (item.ContainsNodeId.HasValue && item.NodeId != item.ContainsNodeId &&
                        (
                            item.IsNew ||
                            (
                                item.AristaMotiveStateId == (int)MotiveStateNode.Confirmed &&
                                (item.AristaChildMotiveStateId == null || item.AristaChildMotiveStateId == (int)MotiveStateNode.Confirmed) &&
                                (
                                    item.AristaValidityFrom == null ||
                                    (
                                        item.AristaValidityFrom <= validityFrom && item.AristaValidityTo >= validityFrom
                                    )
                                )
                            )
                        ))
                    {
                        var childNode = result.ToChildNodesDTO(validityFrom, item.ContainsNodeId.Value, item.NodeId, active);

                        if (childNode != null && !node.Nodes.Any(a => a.Id == childNode.Id))
                            node.Nodes.Add(childNode);
                    }
                }

                return node;
            }

            return null;
        }

        /// <summary>
        /// Converts to nodeversiondto.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public static DTO.NodeVersionDTO ToNodeVersionDTO(this DTO.StructureNodeDTO version)
        {
            var node = new DTO.NodeVersionDTO
            {
                NodeDefinitionId = version.NodeDefinitionId,
                Name = version.NodeName,
                Code = version.NodeCode,
                LevelId = version.NodeLevelId,
                NodeIdParent = version.NodeParentId,
                Active = version.NodeActive,
                AttentionModeId = version.NodeAttentionModeId,
                RoleId = version.NodeRoleId,
                SaleChannelId = version.NodeSaleChannelId,
                EmployeeId = version.NodeEmployeeId,
                ValidityFrom = version.NodeValidityFrom
            };

            if (version.NodeAttentionModeId.HasValue)
                node.AttentionMode = new DTO.ItemNodeDTO()
                {
                    Id = version.NodeAttentionModeId,
                    Name = version.NodeAttentionModeName
                };

            if (version.NodeRoleId.HasValue)
                node.Role = new DTO.ItemNodeDTO()
                {
                    Id = version.NodeRoleId,
                    Name = version.NodeRoleName
                };

            if (version.NodeSaleChannelId.HasValue)
                node.SaleChannel = new DTO.ItemNodeDTO()
                {
                    Id = version.NodeSaleChannelId,
                    Name = version.NodeSaleChannelName
                };

            return node;
        }
    }
}
