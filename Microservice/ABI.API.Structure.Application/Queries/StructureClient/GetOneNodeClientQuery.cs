using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;

using AutoMapper;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureClient
{
    public class GetOneNodeClientQuery : IRequest<List<DTO.StructureClientDTO>>
    {
        public int NodeId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }

    }

    public class GetOneNodeClientQueryHandler : IRequestHandler<GetOneNodeClientQuery, List<DTO.StructureClientDTO>>
    {
        private readonly IStructureClientRepository _structureClientRepository;
        private readonly IMapper _mapper;

        public GetOneNodeClientQueryHandler(IStructureClientRepository structureClientRepository, IMapper mapper)
        {
            _structureClientRepository = structureClientRepository;
            _mapper = mapper;
        }

        public async Task<List<DTO.StructureClientDTO>> Handle(GetOneNodeClientQuery request, CancellationToken cancellationToken)
        {
            var clients = _mapper.Map<IList<StructureClientNode>, IList<DTO.StructureClientDTO>>(await _structureClientRepository.GetActivesClientsByNodeId(request.NodeId, request.ValidityFrom, cancellationToken));

            return clients.ToList();
        }
    }
}
