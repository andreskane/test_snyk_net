using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.AttentionMode
{
    public class AddCommand : AttentionModeDTO, IRequest<int>, INameValidator
    {
    }

    public class AddCommandHandler : IRequestHandler<AddCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repo;

        public AddCommandHandler(IAttentionModeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<E.AttentionMode>(request);
            await _repo.Create(entity);
            return entity.Id;
        }
    }
}
