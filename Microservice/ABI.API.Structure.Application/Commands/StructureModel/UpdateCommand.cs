using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.StructureModel
{
    public class UpdateCommand : StructureModelDTO, IRequest
    {
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _repo;

        public UpdateCommandHandler(IStructureModelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<E.StructureModel>(request);

            await _repo.Update(entity);
            return Unit.Value;
        }
    }
}
