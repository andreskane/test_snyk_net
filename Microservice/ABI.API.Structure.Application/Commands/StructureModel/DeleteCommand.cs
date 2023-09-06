using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModel
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
        private readonly IStructureModelRepository _repo;
        private readonly IStructureModelDefinitionRepository _structureModelDefinitionRepository;

        public DeleteCommandHandler(IStructureModelRepository repo, IStructureModelDefinitionRepository structureModelDefinitionRepository)
        {
            _repo = repo;
            _structureModelDefinitionRepository = structureModelDefinitionRepository;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var structureModelDefinition = await _structureModelDefinitionRepository.GetAllByStructureModel(request.Id);
            if (structureModelDefinition.Count > 0)
            {
                foreach (var item in structureModelDefinition)
                {
                    await _structureModelDefinitionRepository.Delete(item.Id);
                }
            }

            await _repo.Delete(request.Id);
            return Unit.Value;
        }
    }
}
