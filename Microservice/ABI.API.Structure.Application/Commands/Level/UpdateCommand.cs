using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.Level
{
    public class UpdateCommand : LevelDTO, IRequest<int>, INameValidator
    {
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILevelRepository _repo;

        public UpdateCommandHandler(ILevelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<E.Level>(request);

            await _repo.Update(entity);
            return entity.Id;
        }
    }
}
