using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureModels
{
    public class GetByIdQuery : IRequest<StructureModelDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, StructureModelDTO>
    {
        private readonly IMapper _mapper;
        private readonly IStructureModelRepository _repo;

        public GetByIdQueryHandler(IMapper mapper, IStructureModelRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<StructureModelDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetById(request.Id);
            return _mapper.Map<Domain.Entities.StructureModel, StructureModelDTO>(results);
        }
    }
}
