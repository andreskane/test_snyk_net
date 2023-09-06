using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Validations.Extensions;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;
        private List<Tuple<int?, int?>> _responsableAttentionModes;

        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public string MessageError { get; set; }


        public ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidator(IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _attentionModeRoleRepo = attentionModeRoleRepo;

            NodesError = new List<DTO.StructureNodeDTO>();
            MessageError = "Persona asignada es responsable en un territorio y no responsable en otro";
        }


        public async Task<bool> Validate(IList<DTO.StructureNodeDTO> nodes)
        {
            var attentionModeRoleRepo = await _attentionModeRoleRepo.GetAll();

            _responsableAttentionModes = attentionModeRoleRepo
                .Where(w => w.EsResponsable)
                .Select(s => new Tuple<int?, int?>(s.AttentionModeId, s.RoleId))
                .Distinct()
                .ToList();

            var territories = nodes
                .Where(w =>
                    w.NodeLevelId == 8 &&
                    w.NodeEmployeeId.HasValue
                )
                .GroupBy(g => g.NodeEmployeeId.Value)
                .Where(w => w.Count() > 1)
                .ToDictionary(k => k.Key, v => v.ToList());

            CheckTerritories(territories);

            if (NodesError.Count > 0)
            {
                NodesError = NodesError
                    .OrderBy(o => o.NodeName)
                    .ThenBy(o => o.NodeCode)
                    .ToList();

                return false;
            }

            return true;
        }

        private void CheckTerritories(IDictionary<int, List<DTO.StructureNodeDTO>> employeeTerritories)
        {
            foreach (var et in employeeTerritories)
            {
                if (!et.Value.Any())
                    continue;

                if (et.Value.CheckConsistency(_responsableAttentionModes) != -1)
                    continue;

                foreach (var n in et.Value)
                {
                    NodesError.Add(new DTO.StructureNodeDTO
                    {
                        NodeId = n.NodeId,
                        NodeName = n.NodeName,
                        NodeCode = n.NodeCode,
                        NodeActive = n.NodeActive,
                        NodeLevelId = n.NodeLevelId
                    });
                }
            }
        }
    }
}
