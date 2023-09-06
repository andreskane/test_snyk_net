using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Level
{
    public class GetByIdQuery : IRequest<LevelDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, LevelDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILevelRepository _repository;

        public GetByIdQueryHandler(ILevelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LevelDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetById(request.Id);
            return _mapper.Map<Domain.Entities.Level, LevelDTO>(results);
        }
    }
}
