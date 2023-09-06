using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class VersionedUpdateStateVersionCommand : IRequest<bool>
    {
        public int VersionedId { get; set; }
        public VersionedState State { get; set; }
        public string Message { get; set; }

    }

    public class VersionedUpdateStateVersionCommandHandler : IRequestHandler<VersionedUpdateStateVersionCommand, bool>
    {
        private readonly IVersionedRepository _versionedRepository;
        private readonly IVersionedLogRepository _versionedLogRepository;
        private readonly IVersionedStatusRepository _versionedStatusRepository;

        public VersionedUpdateStateVersionCommandHandler(IVersionedRepository versionedRepository,
                                                            IVersionedLogRepository versionedLogRepository,
                                                            IVersionedStatusRepository versionedStatusRepository)
        {
            _versionedRepository = versionedRepository;
            _versionedLogRepository = versionedLogRepository;
            _versionedStatusRepository = versionedStatusRepository;
        }


        public async Task<bool> Handle(VersionedUpdateStateVersionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var versioned = await _versionedRepository.GetById(request.VersionedId);
                var state = await _versionedStatusRepository.GetById((int)request.State);

                versioned.StatusId = (int)request.State;
                versioned.Version = request.State == VersionedState.PendienteDeEnvio ? null : versioned.Version;

                await _versionedRepository.Update(versioned);

                var log = new VersionedLog
                {
                    Date = DateTimeOffset.UtcNow,
                    VersionedId = request.VersionedId,
                    LogStatusId = GetStateLog(request.State),
                    Detaill = string.IsNullOrEmpty(request.Message) ? $"Cambio a Estado: {state.Name}" : request.Message
                };

                await _versionedLogRepository.Create(log);

                return true;
            }
            catch (Exception ex)
            {
                throw new GenericException("Problemas en guardar el estados", ex);
            }

        }

        /// <summary>
        /// Gets the state log.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        private int GetStateLog(VersionedState state)
        {

            switch (state)
            {
                case VersionedState.PendienteDeEnvio:
                    return (int)VersionedLogState.EPE;
    
                case VersionedState.Procesando:
                    return (int)VersionedLogState.EPR;
           
                case VersionedState.Aceptado:
                    return (int)VersionedLogState.EAC;
               
                case VersionedState.Operativo:
                    return (int)VersionedLogState.EOP;
                case VersionedState.Rechazado:
                    return (int)VersionedLogState.EREC;
                case VersionedState.Cancelado:
                    return (int)VersionedLogState.ECANC;
                case VersionedState.Unificado:
                    return (int)VersionedLogState.EUNIF;
                default:
                    return (int)VersionedLogState.EPE;

            }

        }
    }
}
