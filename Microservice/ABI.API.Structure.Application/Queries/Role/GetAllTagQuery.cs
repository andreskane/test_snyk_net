using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Role
{
    public class GetAllTagQuery : IRequest<RoleTagDTO>
    {
    }

    public class GetAllTagQueryHandler : IRequestHandler<GetAllTagQuery, RoleTagDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;

        public GetAllTagQueryHandler(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RoleTagDTO> Handle(GetAllTagQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllTag();
            return _mapper.Map<IList<string>, RoleTagDTO>(results);
        }
    }
}
