using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Role
{
    public class GetByIdQuery : IRequest<RoleDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, RoleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;

        public GetByIdQueryHandler(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RoleDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetById(request.Id);
            return _mapper.Map<Domain.Entities.Role, RoleDTO>(results);
        }
    }
}
