using ABI.API.Structure.Application.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class RevertAllChangesCommand : IRequest<bool>
    {
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class RevertAllChangesCommandHandler : IRequestHandler<RevertAllChangesCommand, bool>
    {
        private readonly IStructureNodeRepository _structureNodeRepository;


        public RevertAllChangesCommandHandler(IStructureNodeRepository structureNodeRepository)
        {
            _structureNodeRepository = structureNodeRepository;
        }


        public async Task<bool> Handle(RevertAllChangesCommand request, CancellationToken cancellationToken)
        {
            var result = await RevertArists(request, cancellationToken) | await RevertDefinitions(request, cancellationToken);

            return result;
        }


        private async Task<bool> RevertArists(RevertAllChangesCommand request, CancellationToken cancellationToken)
        {
            var aristas = await _structureNodeRepository.GetAllAristaByNodeTo(request.NodeId);

            aristas = aristas
                .Where(w => w.MotiveStateId == (int)MotiveStateNode.Confirmed)
                .ToList();

            if (!aristas.Any())
                return false;

            var revert = aristas.SingleOrDefault(s => s.ValidityFrom == request.ValidityFrom);

            if (revert == null)
                return false;

            var laterVersions = aristas.Any(a =>
                a.Id != revert.Id &&
                a.ValidityFrom <= revert.ValidityTo &&
                a.ValidityTo >= revert.ValidityTo
            );

            if (laterVersions)
                return false;

            var earlierVersion = aristas
                .Where(w => w.Id != revert.Id)
                .OrderByDescending(o => o.ValidityFrom)
                .FirstOrDefault();

            revert.EditMotiveStateId((int)MotiveStateNode.Cancelled);
            _structureNodeRepository.UpdateArista(revert);

            if (earlierVersion != null)
            {
                earlierVersion.EditValidityTo(DateTimeOffset.MaxValue.ToOffset(-3)); //HACER: Ojo con multipaís.
                _structureNodeRepository.UpdateArista(earlierVersion);
            }

            await _structureNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }

        private async Task<bool> RevertDefinitions(RevertAllChangesCommand request, CancellationToken cancellationToken)
        {
            var definitions = await _structureNodeRepository.GetAllNodoDefinitionByNodeIdAsync(request.NodeId);

            definitions = definitions
                .Where(w => w.MotiveStateId == (int)MotiveStateNode.Confirmed)
                .ToList();

            if (!definitions.Any())
                return false;

            var revert = definitions.SingleOrDefault(s => s.ValidityFrom == request.ValidityFrom);

            if (revert == null)
                return false;

            var laterVersions = definitions.Any(a =>
                a.Id != revert.Id &&
                a.ValidityFrom <= revert.ValidityTo &&
                a.ValidityTo >= revert.ValidityTo
            );

            if (laterVersions)
                return false;

            var earlierVersion = definitions
                .Where(w => w.Id != revert.Id)
                .OrderByDescending(o => o.ValidityFrom)
                .FirstOrDefault();

            revert.EditMotiveStateId((int)MotiveStateNode.Cancelled);
            _structureNodeRepository.UpdateNodoDefinition(revert);

            if (earlierVersion != null)
            {
                earlierVersion.EditValidityTo(DateTimeOffset.MaxValue.ToOffset(-3)); //HACER: Ojo con multipaís.
                _structureNodeRepository.UpdateNodoDefinition(earlierVersion);
            }

            await _structureNodeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
