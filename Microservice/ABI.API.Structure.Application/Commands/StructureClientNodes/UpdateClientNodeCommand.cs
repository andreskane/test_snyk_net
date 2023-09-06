using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Commands.StructureClientNodes
{
    public class UpdateClientNodeCommand : StructureClientDTO, IRequest<int>
    {
    }
    public class UpdateClientNodeCommandHandler : IRequestHandler<UpdateClientNodeCommand, int>
    {
        private readonly IStructureClientRepository _repositoryClient;

        public UpdateClientNodeCommandHandler(IStructureClientRepository repo)
        {
            _repositoryClient = repo;
        }
        /// <summary>
        /// Recupero por ID de relación, si recupero cierro vigencia y creo una nueva relación
        /// Si el ID de nodo es el mismo que el que recupera entonces seteo los datos para actualizar (salvo el de nodo ya que se supone que sigue siendo el mismo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Handle(UpdateClientNodeCommand request, CancellationToken cancellationToken)
        {
            var clientNodeOld = await _repositoryClient.GetById(request.Id, cancellationToken);
            if (clientNodeOld != null && clientNodeOld.NodeId != request.NodeId)
            {
                clientNodeOld.EditValidityTo(request.ValidityFrom.AddDays(-1));
                await _repositoryClient.Update(clientNodeOld, cancellationToken);
            }
            var clientNodeNew = new StructureClientNode(request.NodeId, request.Name, request.ClientId, request.State, request.ValidityFrom);
            await _repositoryClient.Create(clientNodeNew, cancellationToken);
            return clientNodeNew.Id;
        }
    }
}
