
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.Commands.Entities;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.API.Structure.Application.Validations;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class ValidateStructureCommandHandler : IRequestHandler<ValidateStructureCommand, ValidateStructure>
    {
        private readonly ILevelTruckPortalRepository _aclLevelRepository;
        private readonly IDBUHResourceRepository _dBUHResourceRepository;
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IMediator _mediator;
        private readonly IStructureRepository _structureRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IAttentionModeRoleRepository _attentionModeRoleRepo;

        public ValidateStructureCommandHandler(
            IMediator mediator,
            ILevelTruckPortalRepository aclLevelRepository,
            IStructureNodeRepository structureNodeRepository,
            IDBUHResourceRepository dBUHResourceRepository,
            ILevelRepository levelRepository,
            IStructureRepository structureRepository,
            IAttentionModeRoleRepository attentionModeRoleRepo)
        {
            _aclLevelRepository = aclLevelRepository;
            _structureNodeRepository = structureNodeRepository;
            _mediator = mediator;
            _structureRepository = structureRepository;
            _levelRepository = levelRepository;
            _dBUHResourceRepository = dBUHResourceRepository;
            _attentionModeRoleRepo = attentionModeRoleRepo;
        }


        public async Task<ValidateStructure> Handle(ValidateStructureCommand request, CancellationToken cancellationToken)
        {
            var validatorAttentionMode = new ValidateStructureAttentionModeCommandValidator();
            var validatorSaleChannel = new ValidateStructureSaleChannelCommandValidator();
            var validateStructureNodeCode = new ValidateStructureNodeCodeCommandValidator();
            var validateStructureNodeLevel = new ValidateStructureNodeLevelCommandValidator();
            var validateStructureNodeCodeNull = new ValidateStructureNodeCodeNullCommandValidator(_structureNodeRepository);
            var validateStructureNodeNameNull = new ValidateStructureNodeNameNullCommandValidator(_structureNodeRepository);
            var validateStructureNodeState = new ValidateStructureNodeStateValidator();
            var validateStructureNodeResourceLevel = new ValidateStructureNoderResourceLevelCommandValidator(_aclLevelRepository, _dBUHResourceRepository, _structureNodeRepository);
            var validateStructureNodeResponsable = new ValidateStructureNodeResponsableCommandValidator(_attentionModeRoleRepo);
            var validateStructureNodeEmployeeResponsableZones = new ValidateStructureNodeEmployeeResponsableZonesCommandValidator(_attentionModeRoleRepo);
            var validateStructureNodeEmployeeResponsableTerritories = new ValidateStructureNodeEmployeeResponsableTerritoriesCommandValidator(_attentionModeRoleRepo);
            var validateRepeatedEmployee = new ValidateEmployeeIdRepeatedCommandValidator(_mediator);

            var validate = new ValidateStructure();

            var nodes = await GetNodesAsync(request.StructureId, request.ValidityFrom);
            var levels = await _levelRepository.GetAll();
            var structure = await _structureRepository.GetStructureDataCompleteAsync(request.StructureId);
            var structureModels = structure.StructureModel.StructureModelsDefinitions.ToList();

            validatorAttentionMode.StructureModels = structureModels;
            validateStructureNodeCode.StructureModels = structureModels;
            validatorSaleChannel.StructureModels = structureModels;
            validateStructureNodeLevel.StructureModels = structureModels;
            validateStructureNodeCodeNull.Structure = structure;
            validateStructureNodeNameNull.Structure = structure;
            validateStructureNodeResourceLevel.Structure = structure;
            validateStructureNodeResponsable.StructureModels = structureModels;


            // Valida Modo de Atención
            if (!validatorAttentionMode.ValidateAttentionMode(nodes))
            {
                foreach (var item in validatorAttentionMode.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validatorAttentionMode.MessageError, "ErrorAttentionMode", 1, null);
                }
            }

            // Valida Canal de Venta
            if (!validatorSaleChannel.ValidateSaleChannel(nodes))
            {
                foreach (var item in validatorSaleChannel.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validatorSaleChannel.MessageError, "ErrorSaleChannel", 2, null);
                }
            }

            // Validar Codigo Nodo
            if (!validateStructureNodeCode.ValidateNodeCode(nodes))
            {
                foreach (var item in validateStructureNodeCode.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeCode.MessageError, "ErrorNodoCode", 1, null);
                }
            }

            // valida niveles de la estructura
            if (!validateStructureNodeLevel.ValidateLevel(nodes))
            {
                foreach (var item in validateStructureNodeLevel.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, GetAllLevelPending(item, structureModels), "ErrorNodoLevel", 2, null);
                }

            }

            // Validaciones propias de TRUCK
            // Valida que no sean nulos los códigos de los nodos
            if (!validateStructureNodeCodeNull.Validate(nodes))
            {
                foreach (var item in validateStructureNodeCodeNull.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeCodeNull.MessageError, "ErrorNodoLevel", 1, null);
                }
            }

            // Valida que no sean nulos los nombres de los nodos
            if (!validateStructureNodeNameNull.Validate(nodes))
            {
                foreach (var item in validateStructureNodeNameNull.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeNameNull.MessageError, "ErrorNodoLevel", 1, null);
                }
            }

            // Valida nodo inactivo en nodo padre activo
            if (!validateStructureNodeState.Validate(nodes))
            {
                foreach (var item in validateStructureNodeState.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeState.MessageError, "ErrorNodoState", 2, null);
                }
            }

            // Valida que las personas asignadas a un nodo tengan el nivel correcto
            if (!await validateStructureNodeResourceLevel.Validate(nodes))
            {
                foreach (var item in validateStructureNodeResourceLevel.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeResourceLevel.MessageError, "ErrorNodoLevel", 1, null);
                }
            }

            // Valida que los territorios de una zona sean todos responsables o todos no responsables
            if (!await validateStructureNodeResponsable.Validate(nodes))
            {
                foreach (var item in validateStructureNodeResponsable.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeResponsable.MessageError, "ErrorNodoResponsable", 2, null);
                }
            }

            // Valida que las personas asignadas en distintas zonas sean siempre responsables o siempre no responsables en todos los territorios
            if (!await validateStructureNodeEmployeeResponsableZones.Validate(nodes))
            {
                foreach (var item in validateStructureNodeEmployeeResponsableZones.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeEmployeeResponsableZones.MessageError, "ErrorNodoEmployeeResponsable", 2, null);
                }
            }

            // Valida que las personas asignadas en distintos territorios sean siempre responsables o siempre no responsables
            if (!await validateStructureNodeEmployeeResponsableTerritories.Validate(nodes))
            {
                foreach (var item in validateStructureNodeEmployeeResponsableTerritories.NodesError)
                {
                    validate.Valid = false;
                    var levelText = levels.FirstOrDefault(l => l.Id == item.NodeLevelId).Name;
                    validate.AddValidateError(item.NodeId, item.NodeCode, item.NodeName, item.NodeLevelId, levelText, validateStructureNodeEmployeeResponsableTerritories.MessageError, "ErrorNodoEmployeeResponsable", 2, null);
                }
            }

            // Valida que las personas asignadas a un nodo no se repita
            if (!await validateRepeatedEmployee.Validate(request.StructureId, request.ValidityFrom))
            {
                foreach (var item in validateRepeatedEmployee.EmployeeError)
                {
                    validate.Valid = false;
                    validate.AddValidateError(0, "", "", 0, "", validateRepeatedEmployee.MessageError, "personInMultipleNodes", 2, item);
                }
            }

            return validate;
        }


        /// <summary>
        /// Gets the nodes asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <returns></returns>
        private async Task<IList<DTO.StructureNodeDTO>> GetNodesAsync(int structureId, DateTimeOffset Validity)
        {
            var actual = await _mediator.Send(new GetAllStructureNodesQuery { StructureId = structureId, ValidityFrom = Validity, Active = null });
            var pending = await _mediator.Send(new GetStructureNodesPendingWithoutSavingQuery { StructureId = structureId, ValidityFrom = Validity });

            if (pending == null || !pending.Any())
                return actual;

            var results = actual
                .Where(w => !pending.Any(a => a.NodeId == w.NodeId))
                .Union(pending)
                .ToList();

            return results;
        }

        /// <summary>
        /// Gets all level pending.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="StructureModels">The structure models.</param>
        /// <returns></returns>
        private string GetAllLevelPending(DTO.StructureNodeDTO node, List<E.StructureModelDefinition> StructureModels)
        {
            var pending = "[pendingText]";

            var currentLevel = StructureModels.FirstOrDefault(s => s.LevelId == node.NodeLevelId);

            var levelsPending = StructureModels.Where(s => s.Id > currentLevel.Id).ToList();

            var count = 0;

            foreach (var item in levelsPending)
            {
                pending += $" {item.Level.Name},";
                count++;
            }

            pending = count == 1 ? pending.Replace("[pendingText]", "Falta el nivel:") : pending.Replace("[pendingText]", "Faltan los niveles:");

            return pending.TrimEnd(',');

        }
    }
}
