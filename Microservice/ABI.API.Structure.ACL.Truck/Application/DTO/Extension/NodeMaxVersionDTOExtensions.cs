using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.ACL.Truck.Application.DTO.Extension
{
    public static class NodeMaxVersionDTOExtensions
    {
        public static string ToTypeVersion(this List<PortalNodeMaxVersionDTO> nodeMaxVersion, int nodeId)
        {
            var node = nodeMaxVersion.FirstOrDefault(m => m.NodeId == nodeId);
            return node != null && node.ValidityFrom.HasValue ? "B" : "N";
        }
    }
}
