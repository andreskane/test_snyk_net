
using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace ABI.API.Structure.Application.Commands.ImportProcess
{
    public class EditCommand : IRequest
    {
        public int Id { get; set; }
        public DateTime ProcessDate { get; set; }
        public string Condition { get; set; }
    }

    public class EditCommandHandler : IRequestHandler<EditCommand>
    {
        private readonly IImportProcessRepository _repository;

        public EditCommandHandler(IImportProcessRepository repository) =>
            _repository = repository;

        public async Task<Unit> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            //  await _repository.EditAsync(request, cancellationToken);
            await _repository.EditAsync(request.Id,request.Condition,request.ProcessDate, cancellationToken);
            return Unit.Value;
        }
    }
}
