using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.SaleChannel
{
    public class GetByIdQuery : IRequest<SaleChannelDTO>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, SaleChannelDTO>
    {
        private readonly IMapper _mapper;
        private readonly ISaleChannelRepository _repository;

        public GetByIdQueryHandler(ISaleChannelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleChannelDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetById(request.Id);
            return _mapper.Map<Domain.Entities.SaleChannel, SaleChannelDTO>(results);
        }
    }
}
