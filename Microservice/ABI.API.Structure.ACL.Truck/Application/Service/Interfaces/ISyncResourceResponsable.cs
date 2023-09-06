using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service.Interfaces
{
    public interface ISyncResourceResponsable
    {
        Task DoWork(CancellationToken cancellationToken);
        Task Synchronize();
    }
}
