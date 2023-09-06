using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionMode
{
    public class GetByIdQuery : IRequest<AttentionModeDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, AttentionModeDTO>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repository;

        public GetByIdQueryHandler(IAttentionModeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AttentionModeDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetById(request.Id);
            return _mapper.Map<Domain.Entities.AttentionMode, AttentionModeDTO>(results);
        }
    }
}
