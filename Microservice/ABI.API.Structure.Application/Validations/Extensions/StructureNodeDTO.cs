using System;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations.Extensions
{
    public static class StructureNodeDTO
    {
        public static int CheckConsistency(this List<DTO.StructureNodeDTO> nodes, List<Tuple<int?, int?>> responsableAttentionModes)
        {
            var allResponsables = nodes.All(all =>
                responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.NodeAttentionModeId) &&
                    a.Item2.Equals(all.NodeRoleId)
                )
            );

            var allNoResponsable = nodes.All(all =>
                !responsableAttentionModes.Any(a =>
                    a.Item1.Equals(all.NodeAttentionModeId) &&
                    a.Item2.Equals(all.NodeRoleId)
                )
            );

            if (allResponsables)
                return 0;
            else if (allNoResponsable)
                return 1;
            else
                return -1;
        }
    }
}
