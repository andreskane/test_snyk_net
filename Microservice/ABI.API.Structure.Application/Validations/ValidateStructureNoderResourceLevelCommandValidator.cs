using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Validations
{

    public class ValidateStructureNoderResourceLevelCommandValidator : AbstractValidator<ValidateStructureCommand>
    {
        private readonly ILevelTruckPortalRepository _aclLevelRepository;
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IDBUHResourceRepository _dBUHResourceRepository;

        public List<DTO.StructureNodeDTO> NodesError { get; set; }

        public StructureDomain Structure { get; set; }

        public string MessageError { get; set; }


        public ValidateStructureNoderResourceLevelCommandValidator(ILevelTruckPortalRepository aclLevelRepository, IDBUHResourceRepository dBUHResourceRepository, IStructureNodeRepository structureNodeRepository)
        {
            NodesError = new List<DTO.StructureNodeDTO>();
            _aclLevelRepository = aclLevelRepository;
            _structureNodeRepository = structureNodeRepository;
            _dBUHResourceRepository = dBUHResourceRepository;
        }


        public async Task<bool> Validate(IList<DTO.StructureNodeDTO> nodes)
        {
            if (!Structure.StructureModel.CanBeExportedToTruck)
                return true;

            MessageError = "Persona asignada no corresponde al nivel del nodo";

            var responsibles = await _dBUHResourceRepository.GetAllResource();
            var levels = await _aclLevelRepository.GetAll();

            var wrongLevels = nodes
                .Where(w => w.NodeEmployeeId.HasValue)
                .Select(s => new
                {
                    Node = s,
                    Responsible = s.NodeEmployeeId.HasValue ? responsibles.FirstOrDefault(f => Convert.ToInt32(f.Id) == s.NodeEmployeeId.Value) : null,
                    Levels = levels
                        .First(f => f.LevelPortalId == s.NodeLevelId)
                        .TypeEmployeeTruck
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                })
                .Where(d =>
                {
                    if (d.Responsible != null)
                    {
                        var vdrTpoCats = d.Responsible.Relations
                            .Where(w => w.Type == 1)
                            .Select(a => a.Attributes.VdrTpoCat)
                            .ToList();

                        var levels = d.Levels.ToList();
                        var intersect = levels.Intersect(vdrTpoCats);

                        if (!intersect.Any())
                            return true;
                    }

                    return false;
                })
                .Where(c =>
                {
                    var nodeDef = _structureNodeRepository.GetNodoDefinitionPendingAsync(c.Node.NodeId).GetAwaiter().GetResult();

                    if (nodeDef != null && nodeDef.EmployeeId.HasValue)
                    {
                        var employee = responsibles.FirstOrDefault(f => Convert.ToInt32(f.Id) == nodeDef.EmployeeId.Value);

                        var vdrTpoCats = employee.Relations.Select(a => a.Attributes.VdrTpoCat).ToList();

                        var levels = c.Levels.ToList();
                        var intersect = levels.Intersect(vdrTpoCats);

                        if (!intersect.Any())
                            return true;
                    }
                    else
                        return true;

                    return false;
                })
                .GroupBy(g => new { g.Node.NodeId, g.Node.NodeCode, g.Node.NodeName, g.Node.NodeLevelId })
                .Select(s => new DTO.StructureNodeDTO { NodeId = s.Key.NodeId, NodeCode = s.Key.NodeCode, NodeName = s.Key.NodeName, NodeLevelId = s.Key.NodeLevelId });

            NodesError.AddRange(wrongLevels);

            if (NodesError.Count > 0)
                return false;

            return true;
        }
    }
}
