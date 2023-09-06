using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetAllByScheduledStructureV2Query : IRequest<DTO.StructureDomainV2DTO>
    {
        public int Id { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
        public bool? Active { get; set; }
    }

    public class GetAllByScheduledStructureV2Handler : IRequestHandler<GetAllByScheduledStructureV2Query, DTO.StructureDomainV2DTO>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllByScheduledStructureV2Handler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        public async Task<DTO.StructureDomainV2DTO> Handle(GetAllByScheduledStructureV2Query request, CancellationToken cancellationToken)
        {
            var structure = await _mediator.Send(new GetAllByScheduledStructureQuery { Id = request.Id, Active = request.Active, ValidityFrom = request.ValidityFrom });
            return _mapper.Map<DTO.StructureDomainDTO, DTO.StructureDomainV2DTO>(structure);
        }
    }
}
