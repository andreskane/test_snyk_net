using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureNodes
{
    public class CreateNodeLinkCommandHandler : IRequestHandler<CreateNodeLinkCommand, int>
    {
        public Task<int> Handle(CreateNodeLinkCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
