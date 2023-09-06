using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class CreateStructureCommandHandler : IRequestHandler<CreateStructureCommand, int>
    {
        private readonly IStructureRepository _repository;

        public CreateStructureCommandHandler(IStructureRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(CreateStructureCommand request, CancellationToken cancellationToken)
        {
            var structure = new StructureDomain(request.Name, request.StructureModelId, request.RootNodeId, request.ValidityFrom, request.Code);

            structure = _repository.Add(structure);

            var resulSave = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return await Task.Run(() => structure.Id);
        }
    }
}
