using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.DTO.Extension
{
    public static class StructureDomainExtensions
    {
        public static StructureDomainDTO ToStructureDomainDTO(this StructureDomain structureDomain, DateTimeOffset validityFrom, List<DTO.StructureNodeDTO> result, List<DTO.StructureNodeDTO> resultPending, bool? active, bool changesWithoutSaving)
        {
            var nodes = SetVersionType(result, resultPending);

            var structureNode = new DTO.StructureDomainDTO();

            var listNode = new List<NodeDTO>();

            var structure = new StructureDTO
            {
                Id = structureDomain.Id,
                Name = structureDomain.Name,
                ValidityFrom = structureDomain.ValidityFrom,
                Erasable = false,
                StructureModelId = structureDomain.StructureModelId,
                Code = structureDomain.Code
            };

            if (structureDomain.Node != null && structureDomain.Node.StructureNodoDefinitions != null)
            {
                structure.FirstNodeName = structureDomain.Node.StructureNodoDefinitions.Count > 0 ? structureDomain.Node.StructureNodoDefinitions.FirstOrDefault().Name : "-";
            }
            else
                structure.FirstNodeName = "-";

            if (resultPending != null)
                structureNode.HasUnsavedChanges = changesWithoutSaving;

            var structureModel = new DTO.StructureModelDTO();

            if (structureDomain.StructureModel != null)
            {
                structureModel.Id = structureDomain.StructureModel.Id;
                structureModel.Name = structureDomain.StructureModel.Name;
                structureModel.ShortName = structureDomain.StructureModel.ShortName;
                structureModel.Description = structureDomain.StructureModel.Description;
                structureModel.Active = structureDomain.StructureModel.Active.Value;
                structureModel.CanBeExportedToTruck = structureDomain.StructureModel.CanBeExportedToTruck;
                structureModel.Code = structureDomain.StructureModel.Code;
                structureModel.CountryId = structureDomain.StructureModel.CountryId;

                if(structureDomain.StructureModel.Country != null)
                {
                    var country = new CountryDTO
                    {
                        Id = structureDomain.StructureModel.Country.Id,
                        Name = structureDomain.StructureModel.Country.Name,
                        Code = structureDomain.StructureModel.Country.Code
                    };

                    structureModel.Country = country;
                }
            }

            structure.StructureModel = structureModel;
            structureNode.Structure = structure;

            if (structureDomain.RootNodeId.HasValue)
            {
                var node = nodes.ToChildNodesDTO(validityFrom, structureDomain.RootNodeId.Value, null, active);

                if (node != null)
                    listNode.Add(node);
            }

            structureNode.Nodes = listNode;
            return structureNode;
        }

        private static List<DTO.StructureNodeDTO> SetVersionType(List<DTO.StructureNodeDTO> result, List<DTO.StructureNodeDTO> resultPending)
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
