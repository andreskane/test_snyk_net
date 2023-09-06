using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.Framework.MS.Service.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class EditStructureCommandHandler : IRequestHandler<EditStructureCommand, int>
    {
        private readonly IStructureRepository _repository;

        public EditStructureCommandHandler(IStructureRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(EditStructureCommand request, CancellationToken cancellationToken)
        {

            var structure = await _repository.GetAsync(request.Id);

            if (structure != null)
            {
                var value = await _repository.GetStructureByNameAsync(request.Name);
                if (value != null && value.Id != request.Id)
                    throw new NameExistsException();

                value = await _repository.GetStructureByCodeAsync(request.Code);
                if (value != null && value.Id != request.Id)
                    throw new StructureCodeExistsException();

                if (request.Code.Trim().Length > 20)
                    throw new NodeCodeLengthException(20);

                structure.SetValidityFrom(request.ValidityFrom);
                structure.SetName(request.Name);

                _repository.Update(structure);

                var resulSave = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }

            return await Task.Run(() => request.Id);

        }

    }
}
