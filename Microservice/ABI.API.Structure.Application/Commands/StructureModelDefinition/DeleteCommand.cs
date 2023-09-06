using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModelDefinition
{
    public class DeleteCommand : IRequest
    {
        public int Id { get; private set; }

        public DeleteCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IStructureModelDefinitionRepository _repo;

        public DeleteCommandHandler(IStructureModelDefinitionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetById(request.Id);
            var entities = await _repo.GetAllByStructureModel(request.Id);
            var entityPrevious = entities.SingleOrDefault(e => e.LevelId == entity.ParentLevelId);
            var entityLater = entities.SingleOrDefault(e => e.ParentLevelId == entity.LevelId);

            if (entityLater != null)
            {
                entityLater.ParentLevelId = !entity.ParentLevelId.HasValue ? null : entityPrevious.LevelId;
                await _repo.Update(entityLater);
            }

            await _repo.Delete(request.Id);
            return Unit.Value;
        }
    }
}
