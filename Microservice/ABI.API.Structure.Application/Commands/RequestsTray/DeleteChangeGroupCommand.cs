using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.RequestsTray
{
    public class DeleteChangeGroupCommand : IRequest, IDeleteChange
    {
        public int structureId { get; set; }
        public DateTimeOffset validity { get; set; }
    }

    public class DeleteChangeGroupCommandHandler : IRequestHandler<DeleteChangeGroupCommand>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService; 
        private readonly IChangeTrackingRepository _repo;
        private readonly IChangeTrackingStatusRepository _repoStatus;
        private readonly IStructureRepository _structureRepository;

        public DeleteChangeGroupCommandHandler(
            IMediator mediator,
            ICurrentUserService currentUserService,
            IChangeTrackingRepository repo,
            IChangeTrackingStatusRepository repoStatus,
            IStructureRepository structureRepository)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
            _repo = repo;
            _repoStatus = repoStatus;
            _structureRepository = structureRepository;
        }

        public async Task<Unit> Handle(DeleteChangeGroupCommand request, CancellationToken cancellationToken)
        {
            var structure = await _structureRepository.GetStructureDataCompleteAsync(request.structureId);

            if (structure.StructureModel.CanBeExportedToTruck)
            {
                var cancelationResult = await _mediator.Send(new ACL.Truck.Application.Commands.VersionedCancelAllChangesCommand
                {
                    StructureId = request.structureId,
                    ValidityFrom = request.validity
                }, cancellationToken);

                if (!cancelationResult)
                    return Unit.Value;
            }            

            var userId = _currentUserService.UserId;
            var changeTrackingResults = await _repo.GetByStructureId(request.structureId);
            var listChanges = changeTrackingResults
                .Where(chg => chg.User.Id == userId && chg.ValidityFrom == request.validity)
                .ToList();

            var nodesIds = listChanges
                .Select(s =>
                {
                    switch (s.IdObjectType)
                    {
                        case (int)ChangeTrackingObjectType.Arista:
                            return s.IdDestino;

                        default:
                            return s.ChangedValueNode.Node.Id;
                    }
                })
                .Distinct()
                .ToList();

            foreach (var id in nodesIds)
                await _mediator.Send(new RevertAllChangesCommand { NodeId = id.Value, ValidityFrom = request.validity }, cancellationToken);

            foreach (var item in listChanges)
            {
                var changeStatus = await _repoStatus.GetByChangeId(item.Id);

                if (changeStatus != null)
                    await _repoStatus.Delete(changeStatus);

                await _repo.Delete(item);
            }            

            return Unit.Value;
        }
    }
}
