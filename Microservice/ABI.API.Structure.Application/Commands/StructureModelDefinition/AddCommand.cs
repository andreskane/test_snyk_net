using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Service.Exceptions;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.StructureModelDefinition
{
    public class AddCommand : StructureModelDefinitionDTO, IRequest
    {
    }

    public class AddCommandHandler : IRequestHandler<AddCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelDefinitionRepository _repo;

        public AddCommandHandler(IStructureModelDefinitionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var value = await _repo.GetLevelByLevelId(request.StructureModelId, request.LevelId);

            if (value != null)
                throw new ElementExistsException();

            var entity = _mapper.Map<E.StructureModelDefinition>(request);

            await _repo.Create(entity);
            return Unit.Value;
        }
    }
}
