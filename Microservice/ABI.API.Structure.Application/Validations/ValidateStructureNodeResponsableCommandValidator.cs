using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureNodeResponsableCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;
        private List<Tuple<int?, int?>> _responsableAttentionModes;

        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public List<StructureModelDefinition> StructureModels { get; set; }

        public string MessageError { get; set; }


        public ValidateStructureNodeResponsableCommandValidator(IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _attentionModeRoleRepo = attentionModeRoleRepo;

            NodesError = new List<DTO.StructureNodeDTO>();
        }

        private async Task LoadResponsables()
        {
            var attentionModeRoleRepo = await _attentionModeRoleRepo.GetAll();

            _responsableAttentionModes = attentionModeRoleRepo
                .Where(w => w.EsResponsable)
                .Select(s => new Tuple<int?, int?>(s.AttentionModeId, s.RoleId))
                .Distinct()
                .ToList();
        }

        public async Task<bool> Validate(IList<DTO.StructureNodeDTO> nodes)
        {
            await LoadResponsables();

            MessageError = "Modos de atención incompatibles en territorios";

            var zones = nodes
                .Where(w => w.NodeLevelId == 8 && w.NodeAttentionModeId.HasValue)
                .GroupBy(g => g.NodeParentId);

            foreach (var z in zones)
            {
                var drafts = z.Where(w => w.NodeMotiveStateId == (int)MotiveStateNode.Draft);
                var actual = z.Where(w =>
                    w.NodeMotiveStateId == (int)MotiveStateNode.Draft ||
                    (
                        w.NodeMotiveStateId == (int)MotiveStateNode.Confirmed &&
                        !drafts.Any(a => a.NodeId == w.NodeId)
                    )
                );

                var ok = (
                    actual.All(all => _responsableAttentionModes.Any(a => a.Item1.Equals(all.NodeAttentionModeId) && a.Item2.Equals(all.NodeRoleId))) ||
                    actual.All(all => !_responsableAttentionModes.Any(a => a.Item1.Equals(all.NodeAttentionModeId) && a.Item2.Equals(all.NodeRoleId)))
                );

                if (!ok)
                {
                    var zone = nodes.First(s => s.NodeId == z.Key);

                    NodesError.Add(new DTO.StructureNodeDTO
                    {
                        NodeId = zone.NodeId,
                        NodeName = zone.NodeName,
                        NodeCode = zone.NodeCode,
                        NodeActive = zone.NodeActive,
                        NodeLevelId = zone.NodeLevelId
                    });
                }
            }

            if (NodesError.Count > 0)
                return false;

            return true;
        }
    }
}
