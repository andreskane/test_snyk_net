using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;

namespace ABI.API.Structure.Application.DTO.Extension
{
    public static class NodeMaxVersionDTOExtensions
    {
        public static string ToTypeVersion(this string typeVersion, NodePendingDTO node, StructureArista arista)
        {
            if (node.NodeMotiveStateId == (int)MotiveStateNode.Draft && arista.MotiveStateId == (int)MotiveStateNode.Draft)
            {
                return "N"; //Nuevo nodo
            }

            if (node.NodeMotiveStateId == (int)MotiveStateNode.Draft && arista.MotiveStateId == (int)MotiveStateNode.Confirmed)
            {
                return "B"; //Nodo Borrador
            }

            return null;
        }
    }
}
