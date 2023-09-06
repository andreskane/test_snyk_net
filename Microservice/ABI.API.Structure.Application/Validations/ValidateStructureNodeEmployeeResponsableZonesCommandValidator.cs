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

    public class ValidateStructureNodeEmployeeResponsableZonesCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;
        private List<Tuple<int?, int?>> _responsableAttentionModes;

        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public string MessageError { get; set; }


        public ValidateStructureNodeEmployeeResponsableZonesCommandValidator(IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _attentionModeRoleRepo = attentionModeRoleRepo;

            NodesError = new List<DTO.StructureNodeDTO>();
            MessageError = "Persona asignada es responsable en una zona y no responsable en otra";
        }


        public async Task<bool> Validate(IList<DTO.StructureNodeDTO> nodes)
        {
            var attentionModeRoleRepo = await _attentionModeRoleRepo.GetAll();

            _responsableAttentionModes = attentionModeRoleRepo
                .Where(w => w.EsResponsable)
                .Select(s => new Tuple<int?, int?>(s.AttentionModeId, s.RoleId))
                .Distinct()
                .ToList();

            var zones = nodes
                .Where(w =>
                    w.NodeLevelId == 7 &&
                    w.NodeEmployeeId.HasValue
                )
                .GroupBy(g => g.NodeEmployeeId.Value)
                .Where(w => w.GroupBy(g => g.NodeId).Count() > 1)
                .Select(s => new
                {
                    EmployeeId = s.Key,
                    Zones = s
                        .GroupBy(g => g.NodeId)
                        .Select(zones => new
                        {
                            Zone = nodes.First(f =>
                                f.NodeId == zones.Key &&
                                f.NodeEmployeeId == s.Key
                            ),
                            Territories = zones
                                .Join(nodes,
                                    z => z.ContainsNodeId,
                                    t => t.NodeId,
                                    (z, t) => t
                                )

                        })
                })
                .ToDictionary(
                    k => k.EmployeeId,
                    v => v.Zones.ToDictionary(
                        kz => kz.Zone,
                        kv => kv.Territories.ToList()
                    )
                );

            CheckZones(zones);

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

        private void CheckZones(Dictionary<int, Dictionary<DTO.StructureNodeDTO, List<DTO.StructureNodeDTO>>> employeeZones)
        {
            foreach (var ez in employeeZones)
            {
                var consistencyList = new List<int>();

                foreach (var z in ez.Value)
                {
                    if (!z.Value.Any())
                        continue;

                    var consistency = z.Value.CheckConsistency(_responsableAttentionModes);

                    if (consistency == -1)
                        continue;

                    consistencyList.Add(consistency);
                }

                if (consistencyList.Distinct().Count() > 1)
                    NodesError.AddRange(
                        ez.Value.Select(s => new DTO.StructureNodeDTO
                        {
                            NodeId = s.Key.NodeId,
                            NodeName = s.Key.NodeName,
                            NodeCode = s.Key.NodeCode,
                            NodeActive = s.Key.NodeActive,
                            NodeLevelId = s.Key.NodeLevelId
                        })
                    );
            }
        }
    }
}
