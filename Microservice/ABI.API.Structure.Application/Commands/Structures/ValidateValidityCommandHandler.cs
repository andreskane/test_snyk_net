using ABI.API.Structure.Application.Commands.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class ValidateValidityCommandHandler : IRequestHandler<ValidateValidityCommand, ValidateDateStructure>
    {
        public ValidateValidityCommandHandler()
        {
        }

        public async Task<ValidateDateStructure> Handle(ValidateValidityCommand request, CancellationToken cancellationToken)
        {

            var validate = new ValidateDateStructure
            {
                StructureId = request.StructureId,
                ValidityFrom = request.Validity,
                ValidateDate = true
            };

            return await Task.Run(() => validate);

        }

    }

}
