using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedUpdateVersionCommand : IRequest<bool>
    {
        public Versioned Versioned { get; set; }
        
    }

    public class VersionedUpdateVersionCommandHandler : IRequestHandler<VersionedUpdateVersionCommand, bool>
    {
        private readonly IVersionedRepository _versionedRepository;


        public VersionedUpdateVersionCommandHandler(IVersionedRepository versionedRepository)
        {
            _versionedRepository = versionedRepository;
        }


        public async Task<bool> Handle(VersionedUpdateVersionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                await _versionedRepository.Update(request.Versioned);
                return true;
            }
            catch (Exception ex)
            {
                throw new GenericException("Problemas en el envio de Bandejas", ex);
            }

        }
    }
}
