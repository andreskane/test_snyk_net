﻿using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Interfaces;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using E = ABI.API.Structure.Domain.Entities;

namespace ABI.API.Structure.Application.Commands.AttentionMode
{
    public class UpdateCommand : AttentionModeDTO, IRequest<int>, INameValidator
    {
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repo;

        public UpdateCommandHandler(IAttentionModeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<E.AttentionMode>(request);

            await _repo.Update(entity);
            return entity.Id;
        }
    }
}
