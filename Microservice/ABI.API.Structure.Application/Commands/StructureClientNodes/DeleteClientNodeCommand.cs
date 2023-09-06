using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureClientNodes
{
    public class DeleteClientNodeCommand : IRequest
    {
        public int Id { get; private set; }

        public DeleteClientNodeCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteClientNodeCommandHandler : IRequestHandler<DeleteClientNodeCommand>
    {
        private readonly IStructureClientRepository _repo;

        public DeleteClientNodeCommandHandler(IStructureClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteClientNodeCommand request, CancellationToken cancellationToken)
        {
            var client = await _repo.GetById(request.Id, cancellationToken);
            if (client != null)
                await _repo.Delete(client, cancellationToken);
            return Unit.Value;
        }
    }
}
