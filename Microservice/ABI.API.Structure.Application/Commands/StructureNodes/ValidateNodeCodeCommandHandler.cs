using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.StructureNodes;
using ABI.Framework.MS.Net.RestClient;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class ValidateNodeCodeCommandHandler : IRequestHandler<ValidateNodeCodeCommand>
    {
        private readonly IMediator _mediator;

        public ValidateNodeCodeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ValidateNodeCodeCommand request, CancellationToken cancellationToken)
        {
            var structure = await _mediator.Send(new GetStructureDomainQuery { StructureId = request.StructureId });

            if (structure == null)
                throw new NotFoundException();

            var nodes = await _mediator.Send(new GetAllNodeQuery { StructureId = request.StructureId });

            var exists = nodes.FirstOrDefault(w =>
                w.NodeLevelId == request.LevelId &&
                w.NodeCode.Trim() == request.Code.Trim()
            );

            if (exists != null && ((request.NodeId.HasValue  && request.NodeId.Value != exists.NodeId) || !request.NodeId.HasValue ))
                throw new NodeCodeExistsException();

            return new Unit();
        }
    }
}
