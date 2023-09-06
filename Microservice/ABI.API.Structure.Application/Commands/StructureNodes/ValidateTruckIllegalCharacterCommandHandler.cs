using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class ValidateTruckIllegalCharacterCommandHandler : IRequestHandler<ValidateTruckIllegalCharacterCommand>
    {
        private readonly IStructureRepository _structureRepo;

        public ValidateTruckIllegalCharacterCommandHandler(IStructureRepository structureRepo)
        {
            _structureRepo = structureRepo;
        }

        public async Task<Unit> Handle(ValidateTruckIllegalCharacterCommand request, CancellationToken cancellationToken)
        {
            var structure = await _structureRepo.GetStructureNodeRootAsync(request.StructureId);

            if (!structure.StructureModel.CanBeExportedToTruck)
                return new Unit();

            if (string.IsNullOrWhiteSpace(request.Name))
                return new Unit();

            var charset = "\"/ÀÁÈÉÌÍÑÒÓÙÚÜ";

            foreach (var c in charset)
            {
                if (request.Name.ToUpper().Contains(c.ToString()))
                    throw new TruckInvalidCharacterException("Name", c);
            }

            return new Unit();
        }
    }

}
