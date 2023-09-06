﻿using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.AttentionMode
{
    public class GetAllForSelectQuery : IRequest<IList<ItemDTO>>
    {
    }

    public class GetAllForSelectQueryHandler : IRequestHandler<GetAllForSelectQuery, IList<ItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IAttentionModeRepository _repository;

        public GetAllForSelectQueryHandler(IAttentionModeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<ItemDTO>> Handle(GetAllForSelectQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAll();
            return _mapper.Map<IList<Domain.Entities.AttentionMode>, IList<ItemDTO>>(results);
        }
    }
}
