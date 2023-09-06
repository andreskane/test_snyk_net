using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Extension
{
    public static class StructureNodeExtensions
    {

        /// <summary>
        /// Maps the entity structure node custom.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static StructureNodeDTO ToStructureNodeDTO(List<PortalStructureNodeDTO> result, List<PortalStructureNodeDTO> resultPending, StructureDomain structureDomain, bool? active, bool changesWithoutSaving)
        {
            var nodes = SetVersionType(result, resultPending);

            var structureNode = new StructureNodeDTO();

            var listNode = new List<NodeDTO>();

            var structure = new StructureDTO
            {
                Id = structureDomain.Id,
                Name = structureDomain.Name,
                Validity = structureDomain.ValidityFrom,
                Erasable = false,
                StructureModelId = structureDomain.StructureModelId
            };

            if (structureDomain.Node != null && structureDomain.Node.StructureNodoDefinitions != null)
            {
                structure.FirstNodeName = structureDomain.Node.StructureNodoDefinitions.Count > 0 ? structureDomain.Node.StructureNodoDefinitions.FirstOrDefault().Name : "-";
            }
            else
                structure.FirstNodeName = "-";

            if (resultPending != null)
                structureNode.ChangesWithoutSaving = changesWithoutSaving;

            var structureModel = new StructureModelDTO();

            if (structureDomain.StructureModel != null)
            {
                structureModel.Id = structureDomain.StructureModel.Id;
                structureModel.Name = structureDomain.StructureModel.Name;
                structureModel.ShortName = structureDomain.StructureModel.ShortName;
                structureModel.Description = structureDomain.StructureModel.Description;
                structureModel.Active = structureDomain.StructureModel.Active;
                structureModel.CanBeExportedToTruck = structureDomain.StructureModel.CanBeExportedToTruck;
            }

            structure.StructureModel = structureModel;
            structureNode.Structure = structure;

            if (structureDomain.RootNodeId.HasValue)
            {
                var node = GetNodeCustom(nodes, structureDomain.RootNodeId.Value, null, active);

                if (node != null)
                    listNode.Add(node);
            }

            structureNode.Nodes = listNode;
            return structureNode;
        }

        /// <summary>
        /// Gets the node custom.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <param name="active">The active.</param>
        /// <returns></returns>
        private static NodeDTO GetNodeCustom(List<PortalStructureNodeDTO> result, int nodeId, int? nodeIdParent, bool? active)
        {
            if (nodeId > 0)
            {
                var nodesDto = result.Where(n => n.NodeId == nodeId).ToList();
                if (nodesDto == null) return null;
                if (nodesDto.Count == 0) return null;

                var node = new NodeDTO();
                GetData(result, nodeId, nodeIdParent, active, nodesDto, node);
                return node;
            }
            return null;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <param name="active">The active.</param>
        /// <param name="nodesDto">The nodes dto.</param>
        /// <param name="node">The node.</param>
        private static void GetData(List<PortalStructureNodeDTO> result, int nodeId, int? nodeIdParent, bool? active, List<PortalStructureNodeDTO> nodesDto, NodeDTO node)
        {
            foreach (var item in nodesDto)
            {
                GetNode(nodeId, nodeIdParent, node, item);
                GetAdditionalData(node, item);

                if (item.ContainsNodeId.HasValue && item.NodeId != item.ContainsNodeId)
                {
                    var childNode = GetNodeCustom(result, item.ContainsNodeId.Value, item.NodeId, active);
                    if (childNode != null)
                        node.Nodes.Add(childNode);
                }
            }
        }

        /// <summary>
        /// Gets the additional data.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="item">The item.</param>
        private static void GetAdditionalData(NodeDTO node, PortalStructureNodeDTO item)
        {
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
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <param name="node">The node.</param>
        /// <param name="item">The item.</param>
        private static void GetNode(int nodeId, int? nodeIdParent, NodeDTO node, PortalStructureNodeDTO item)
        {
            node.Id = item.NodeId;
            node.StructureId = item.StructureId;
            node.NodeIdParent = nodeIdParent;
            node.Name = item.NodeName;
            node.Code = item.NodeCode;
            node.LevelId = item.NodeLevelId;
            node.Active = item.NodeActive;
            node.ChildNodeActiveFieldCanBeEdited = item.NodeActive;
            node.AttentionModeId = item.NodeAttentionModeId;
            node.RoleId = item.NodeRoleId;
            node.SaleChannelId = item.NodeSaleChannelId;
            node.EmployeeId = item.NodeEmployeeId;
            node.IsRootNode = nodeId == item.RootNodeId;
            node.ValidityFrom = item.NodeValidityFrom;
            node.ValidityTo = item.NodeValidityTo;
            node.VersionType = item.VersionType;
        }

        /// <summary>
        /// Sets the type of the version.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="resultPending">The result pending.</param>
        /// <returns></returns>
        private static List<PortalStructureNodeDTO> SetVersionType(List<PortalStructureNodeDTO> result, List<PortalStructureNodeDTO> resultPending)
        {
            if (resultPending != null)
            {
                foreach (var item in resultPending)
                {
                    var nodes = result.Where(n => n.NodeId == item.NodeId).ToList();

                    if (nodes != null && nodes.Count > 0)
                    {
                        foreach (var itemNode in nodes)
                        {
                            itemNode.VersionType = item.VersionType;
                        }
                    }
                }

            }
            return result;
        }

    }
}
