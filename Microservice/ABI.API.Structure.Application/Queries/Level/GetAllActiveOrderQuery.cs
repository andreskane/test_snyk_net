using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Level
{
    public class GetAllActiveOrderQuery : IRequest<IList<LevelDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveOrderQueryHandler : IRequestHandler<GetAllActiveOrderQuery, IList<LevelDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILevelRepository _repository;

        public GetAllActiveOrderQueryHandler(ILevelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<LevelDTO>> Handle(GetAllActiveOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.Level>, IList<LevelDTO>>(results);
        }
    }
}
