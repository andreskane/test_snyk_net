using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Queries.Level
{
    public class GetAllActiveForSelectQuery : IRequest<IList<ItemDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveForSelectQueryHandler : IRequestHandler<GetAllActiveForSelectQuery, IList<ItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILevelRepository _repo;

        public GetAllActiveForSelectQueryHandler(IMapper mapper, ILevelRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<ItemDTO>> Handle(GetAllActiveForSelectQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAllActive(request.Active, cancellationToken);
            return _mapper.Map<IList<E.Level>, IList<ItemDTO>>(results);
        }
    }
}
