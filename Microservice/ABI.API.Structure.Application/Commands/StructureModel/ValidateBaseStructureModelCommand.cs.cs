using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Service.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureModel
{
    public class ValidateBaseStructureModelCommand : IRequest
    {
        public int? Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class ValidateBaseStructureModelCommandHandler : IRequestHandler<ValidateBaseStructureModelCommand>
    {
        private readonly IStructureModelRepository _structureModelRepo;

        public ValidateBaseStructureModelCommandHandler(IStructureModelRepository structureModelRepo)
        {
            _structureModelRepo = structureModelRepo;
        }
        public async Task<Unit> Handle(ValidateBaseStructureModelCommand request, CancellationToken cancellationToken)
        {
            var value = await _structureModelRepo.GetByNameAndCountry(request.Name, request.CountryId);
            if ((value != null && !request.Id.HasValue) || (request.Id.HasValue && value != null && value.Id != request.Id.Value))
                throw new NameExistsException();

            value = await _structureModelRepo.GetByCodeAndCountry(request.Code, request.CountryId);
            if ((value != null && !request.Id.HasValue) || (request.Id.HasValue && value != null && value.Id != request.Id.Value))
                throw new StructureCodeExistsException();

            if (request.Code.Trim().Length > 3)
                throw new NodeCodeLengthException(3);
            return await Task.Run(() => new Unit());
        }
    }
}
