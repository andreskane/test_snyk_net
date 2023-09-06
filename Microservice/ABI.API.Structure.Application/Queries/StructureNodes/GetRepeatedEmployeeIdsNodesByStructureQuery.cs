using ABI.API.Structure.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetRepeatedEmployeeIdsNodesByStructureQuery : IRequest<List<EmployeeIdNodesDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }

    public class GetRepeatedEmployeeIdsNodesByStructureHandler : IRequestHandler<GetRepeatedEmployeeIdsNodesByStructureQuery, List<EmployeeIdNodesDTO>>
    {
        private readonly IMediator _mediator;

        public GetRepeatedEmployeeIdsNodesByStructureHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<EmployeeIdNodesDTO>> Handle(GetRepeatedEmployeeIdsNodesByStructureQuery request, CancellationToken cancellationToken)
        {
            var employees = await _mediator.Send(new GetEmployeeIdsNodesByStructureQuery 
            { 
                StructureId = request.StructureId,
                ValidityFrom = request.ValidityFrom
            });

            return employees.Where(x => x.Nodes.Count() > 1 && x.EmployeeId.HasValue).ToList();
        }
    }
}
