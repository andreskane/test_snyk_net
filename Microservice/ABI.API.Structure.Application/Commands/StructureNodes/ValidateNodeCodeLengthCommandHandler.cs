using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Net.RestClient;
using MediatR;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class ValidateNodeCodeLengthCommandHandler : IRequestHandler<ValidateNodeCodeLengthCommand>
    {
        private readonly IStructureRepository _structureRepo;
        private readonly IStructureModelRepository _structureModelRepo;

        public ValidateNodeCodeLengthCommandHandler(IStructureRepository structureRepo, IStructureModelRepository structureModelRepo)
        {
            _structureRepo = structureRepo;
            _structureModelRepo = structureModelRepo;
        }

        public async Task<Unit> Handle(ValidateNodeCodeLengthCommand request, CancellationToken cancellationToken)
        {
            var structure = await _structureRepo.GetAsync(request.StructureId);

            if (structure == null)
                throw new NotFoundException();

            var model = await _structureModelRepo.GetById(structure.StructureModelId);
            var maxLength = 10;

            if (model.CanBeExportedToTruck)
            {
                var mustBeNumeric = true;
                maxLength = 3;

                switch (request.LevelId)
                {
                    case 1: // Pais <-> Direccion
                    case 2: // Area <-> Area
                    case 3: // Region <-> Gerencia
                    case 5: // Subregion <-> Region
                        break;
                    case 6: // Jefatura <-> Jefatura
                        maxLength = 10;
                        mustBeNumeric = false;
                        break;
                    case 7: // Zona <-> Zona
                        maxLength = 4;
                        mustBeNumeric = false;
                        break;
                    case 8: // Territorio <-> Territorio
                        maxLength = 5;
                        break;
                }

                if (mustBeNumeric && Regex.IsMatch(request.Code, "\\D"))
                    throw new NodeCodeNotNumericException();
            }

            if (request.Code.Trim().Length > maxLength)
                throw new NodeCodeLengthException(maxLength);

            return await Task.Run(() => new Unit());
        }
    }
}
