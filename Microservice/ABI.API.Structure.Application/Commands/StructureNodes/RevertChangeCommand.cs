using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.Framework.MS.Helpers.Extensions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class RevertChangeCommand : IRequest<Unit>
    {
        public ChangeTrackingObjectType ObjectType { get; set; }
        public int NodeId { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class RevertChangeCommandHandler : IRequestHandler<RevertChangeCommand, Unit>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;


        public RevertChangeCommandHandler(IStructureNodeRepository structureNodeRepository)
        {
            _structureNodeRepository = structureNodeRepository;
        }


        public async Task<Unit> Handle(RevertChangeCommand request, CancellationToken cancellationToken)
        {
            if (request.ObjectType == ChangeTrackingObjectType.Node)
                await RevertDefinition(request, cancellationToken);
            else if (request.ObjectType == ChangeTrackingObjectType.Arista)
                await RevertArist(request, cancellationToken);

            return Unit.Value;
        }

        private async Task RevertDefinition(RevertChangeCommand request, CancellationToken cancellationToken)
        {
            var definitions = await _structureNodeRepository.GetAllNodoDefinitionByNodeIdAsync(request.NodeId);

            if (!definitions.Any())
                return;

            var definition = definitions.OrderByDescending(x => x.Id).SingleOrDefault(w =>
                w.ValidityFrom == request.ValidityFrom &&
                w.MotiveStateId == (int)MotiveStateNode.Confirmed
            );

            if (definition == null)
                return;

            var newDefinition = new StructureNodeDefinition(
                definition.NodeId,
                definition.AttentionModeId,
                definition.RoleId,
                definition.SaleChannelId,
                definition.EmployeeId,
                definition.ValidityFrom,
                definition.Name,
                definition.Active
            );

            switch (request.Field)
            {
                case "AttentionMode":
                    newDefinition.EditAttentionModeId(request.Value.IsNull() ? null : int.Parse(request.Value));
                    break;
                case "EmployeeId":
                    newDefinition.EditEmployeeId(request.Value.IsNull() ? null : int.Parse(request.Value));
                    break;
                case "Name":
                    newDefinition.EditName(request.Value);
                    break;
                case "Active":
                    newDefinition.EditActive(bool.Parse(request.Value));
                    break;
                case "Role":
                    newDefinition.EditRoleId(request.Value.IsNull() ? null : int.Parse(request.Value));
                    break;
                case "SaleChannel":
                    newDefinition.EditSaleChannelId(request.Value.IsNull() ? null : int.Parse(request.Value));
                    break;
            }

            definition.EditMotiveStateId((int)MotiveStateNode.Cancelled);
            newDefinition.EditValidityTo(DateTimeOffset.MaxValue.ToOffset(-3));
            newDefinition.EditVacantPerson(!newDefinition.EmployeeId.HasValue);
            newDefinition.EditMotiveStateId((int)MotiveStateNode.Confirmed);

            _structureNodeRepository.UpdateNodoDefinition(definition);
            _structureNodeRepository.AddNodoDefinition(newDefinition);

            await _structureNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task RevertArist(RevertChangeCommand request, CancellationToken cancellationToken)
        {
            var arists = await _structureNodeRepository.GetAllAristaByNodeTo(request.NodeId);

            if (!arists.Any())
                return;

            var arist = arists.SingleOrDefault(w =>
                w.ValidityFrom == request.ValidityFrom &&
                w.MotiveStateId == (int)MotiveStateNode.Confirmed
            );

            if (arist == null)
                return;

            var newArist = new StructureArista(
                arist.StructureIdFrom,
                int.Parse(request.Value),
                arist.StructureIdTo,
                arist.NodeIdTo,
                arist.TypeRelationshipId,
                arist.ValidityFrom
            );

            arist.EditMotiveStateId((int)MotiveStateNode.Cancelled);

            _structureNodeRepository.UpdateArista(arist);
            _structureNodeRepository.AddArista(newArist);

            await _structureNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
