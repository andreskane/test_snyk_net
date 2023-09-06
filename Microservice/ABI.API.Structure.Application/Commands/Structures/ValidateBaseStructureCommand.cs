using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Service.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class ValidateBaseStructureCommand : IRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ValidateBaseStructureCommandHandler : IRequestHandler<ValidateBaseStructureCommand>
    {
        private readonly IStructureRepository _repo;

        public ValidateBaseStructureCommandHandler(IStructureRepository structureRepo)
        {
            _repo = structureRepo;
        }

        public async Task<Unit> Handle(ValidateBaseStructureCommand request, CancellationToken cancellationToken)
        {
            var value = await _repo.GetStructureByNameAsync(request.Name);
            if ((value != null && !request.Id.HasValue) || (request.Id.HasValue && value != null && value.Id != request.Id.Value))
                throw new NameExistsException();

            value = await _repo.GetStructureByCodeAsync(request.Code);
            if ((value != null && !request.Id.HasValue) || (request.Id.HasValue && value != null && value.Id != request.Id.Value))
                throw new StructureCodeExistsException();

            if (request.Code.Trim().Length > 20)
                throw new NodeCodeLengthException(20);

            return await Task.Run(() => new Unit());
        }
    }
}
