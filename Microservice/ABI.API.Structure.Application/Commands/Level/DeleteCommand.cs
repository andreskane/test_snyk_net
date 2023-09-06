using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Level
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
        private readonly ILevelRepository _repo;

        public DeleteCommandHandler(ILevelRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _repo.Delete(request.Id);
            return Unit.Value;
        }
    }
}
