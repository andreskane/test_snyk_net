using ABI.Framework.MS.Net.RestClient;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.StructureNodes
{
    public class GetCodeHintQuery : IRequest<string>
    {
        public int StructureId { get; set; }
        public int LevelId { get; set; }
    }

    public class GetCodeHintQueryHandler : IRequestHandler<GetCodeHintQuery, string>
    {
        private readonly IMediator _mediator;

        public GetCodeHintQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(GetCodeHintQuery request, CancellationToken cancellationToken)
        {
            var structure = await _mediator.Send(new GetStructureDomainQuery { StructureId = request.StructureId });

            if (structure == null)
                throw new NotFoundException();

            var nodes = await _mediator.Send(new GetAllNodeQuery { StructureId = request.StructureId });

            var codes = nodes
                .Where(w => w.NodeLevelId == request.LevelId)
                .Select(s => s.NodeCode)
                .OrderBy(o => o)
                .ToList();

            var candidate = Enumerable.Range(1, 100000)
                .First(f => !codes.Any(a => a == f.ToString()));

            return candidate.ToString();
        }
    }
}
