using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.SaleChannel
{
    public class AddCommand : SaleChannelDTO, IRequest<int>, INameValidator
    {
    }

    public class AddCommandHandler : IRequestHandler<AddCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ISaleChannelRepository _repo;

        public AddCommandHandler(ISaleChannelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<E.SaleChannel>(request);

            await _repo.Create(entity);
            return entity.Id;
        }
    }
}
