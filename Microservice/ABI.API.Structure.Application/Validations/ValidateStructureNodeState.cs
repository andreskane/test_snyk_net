using ABI.API.Structure.Application.Commands.StructureNodes;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace ABI.API.Structure.Application.Validations
{
    public class ValidateStructureNodeStateValidator : AbstractValidator<ValidateStructureCommand>
    {
        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public string MessageError { get; set; }


        public ValidateStructureNodeStateValidator()
        {
            NodesError = new List<DTO.StructureNodeDTO>();
        }


        public bool Validate(IList<DTO.StructureNodeDTO> nodes)
        {
            MessageError = "Nodo activo en nodo padre inactivo";

            var nodesWithParents = nodes
                .Where(w => w.NodeParentId.HasValue);

            var invalids = nodesWithParents
                .Join(nodesWithParents,
                    c => c.NodeParentId,
                    p => p.NodeId,
                    (c, p) => new { c.NodeId, c, p }
                )
                .Where(w => w.p.NodeActive == false && w.c.NodeActive == true)
                .GroupBy(g => g.NodeId)
                .Select(s => s.First())
                .Select(s => new DTO.StructureNodeDTO
                {
                    NodeId = s.c.NodeId,
                    NodeName = s.c.NodeName,
                    NodeCode = s.c.NodeCode,
                    NodeActive = s.c.NodeActive,
                    NodeLevelId = s.c.NodeLevelId,
                    NodeParentId = s.c.NodeParentId
                })
                .ToList();

            NodesError.AddRange(invalids);

            return !invalids.Any();
        }
    }
}
