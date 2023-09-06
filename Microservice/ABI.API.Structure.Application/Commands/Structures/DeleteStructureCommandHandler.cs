using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class DeleteStructureCommandHandler : IRequestHandler<DeleteStructureCommand, bool>
    {
        private readonly IStructureRepository _repository;

        public DeleteStructureCommandHandler(IStructureRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(DeleteStructureCommand request, CancellationToken cancellationToken)
        {

            var structure = await _repository.GetAsync(request.StructureId);

            _repository.Delete(structure);

            var resulSave = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return await Task.Run(() => resulSave);

        }

    }
}
