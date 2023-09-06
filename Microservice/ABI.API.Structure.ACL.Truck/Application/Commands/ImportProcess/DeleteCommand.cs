using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.ImportProcess
{
    public class DeleteCommand : IRequest
    {
        public DeleteCommand(int[] ids) => Ids = ids;
        public int[] Ids { get; set; }
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IImportProcessRepository _repository;

        public DeleteCommandHandler(IImportProcessRepository repository) =>
            _repository = repository;

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Ids, cancellationToken);
            return Unit.Value;
        }
    }
}