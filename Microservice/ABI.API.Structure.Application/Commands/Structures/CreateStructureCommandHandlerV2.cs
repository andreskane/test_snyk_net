using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.Structures
{
    public class CreateStructureCommandHandlerV2 : IRequestHandler<CreateStructureCommandV2, StructureDTO>
    {
        private readonly IMapper _mapper;
        private readonly IStructureRepository _repository;

        public CreateStructureCommandHandlerV2(IStructureRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<StructureDTO> Handle(CreateStructureCommandV2 request, CancellationToken cancellationToken)
        {
            var structure = new StructureDomain(request.Name, request.StructureModelId, request.RootNodeId, request.ValidityFrom, request.Code);

            structure = _repository.Add(structure);

            var resulSave = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return await Task.Run(() => _mapper.Map<StructureDomain, StructureDTO>(structure));

        }

    }
}
