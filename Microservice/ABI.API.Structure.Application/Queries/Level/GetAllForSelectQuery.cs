using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Level
{
    public class GetAllForSelectQuery : IRequest<IList<ItemDTO>>
    {
    }

    public class GetAllForSelectQueryHandler : IRequestHandler<GetAllForSelectQuery, IList<ItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILevelRepository _repo;

        public GetAllForSelectQueryHandler(IMapper mapper, ILevelRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IList<ItemDTO>> Handle(GetAllForSelectQuery request, CancellationToken cancellationToken)
        {
            var results = await _repo.GetAll();
            return _mapper.Map<IList<Domain.Entities.Level>, IList<ItemDTO>>(results);
        }
    }
}
