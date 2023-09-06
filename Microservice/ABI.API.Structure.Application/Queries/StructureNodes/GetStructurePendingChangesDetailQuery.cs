using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetStructurePendingChangesDetailQuery : IRequest<IList<ChangesDTO>>
    {
        public int StructureId { get; set; }
        public DateTimeOffset ValidityFrom { get; set; }
    }
    public class GetStructurePendingChangesDetailQueryHandler : IRequestHandler<GetStructurePendingChangesDetailQuery, IList<ChangesDTO>>
    {
        private readonly IMediator _mediator;


        public GetStructurePendingChangesDetailQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IList<ChangesDTO>> Handle(GetStructurePendingChangesDetailQuery request, CancellationToken cancellationToken)
        {
            var listPendiente = await _mediator.Send(new GetStructureNodesPendingQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom });

            var list = listPendiente.Where(n =>
                n.AristaMotiveStateId == (int)MotiveStateNode.Draft ||
                n.NodeMotiveStateId == (int)MotiveStateNode.Draft
            )
            .ToList();


            var listChanges = new List<ChangesDTO>();

            foreach (var item in list)
            {
                var nodes = await _mediator.Send(new GetNodesToCompareQuery { StructureId = request.StructureId, ValidityFrom = request.ValidityFrom, NodeId = item.NodeId });

                var nodeChanges = new ChangesDTO
                {
                    Nodes = (List<ChangeNodeDTO>)nodes
                };

                listChanges.Add(nodeChanges);

            }

            return await Task.Run(() => listChanges);
        }
    }
}
