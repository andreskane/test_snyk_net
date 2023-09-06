﻿using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.Role
{
    public class GetAllActiveOrderQuery : IRequest<IList<RoleDTO>>
    {
        public bool Active { get; set; }
    }

    public class GetAllActiveOrderQueryHandler : IRequestHandler<GetAllActiveOrderQuery, IList<RoleDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;

        public GetAllActiveOrderQueryHandler(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<RoleDTO>> Handle(GetAllActiveOrderQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.GetAllActive(request.Active);
            return _mapper.Map<IList<Domain.Entities.Role>, IList<RoleDTO>>(results);
        }
    }
}
