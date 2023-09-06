using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces.Structure
{
    public interface IStructureNodeDefinitionsRespository 
    {

        ValueTask<StructureNodeDefinition> GetByIdAsync(int id);
    }
}
