using ABI.API.Structure.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Infrastructure.Repositories.Interfaces
{
    public interface IEstructuraClienteTerritorioIORepository
    {
        Task<IList<EstructuraClienteTerritorioIO>> GetByProcessId(int processId, CancellationToken cancellationToken);
        Task BulkDelete(IList<EstructuraClienteTerritorioIO> EstructuraClienteTerritorioIOs, CancellationToken cancellationToken);
    }
}
