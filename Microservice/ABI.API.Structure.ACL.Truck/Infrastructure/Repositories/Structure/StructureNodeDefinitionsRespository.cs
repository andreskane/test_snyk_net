using ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Interfaces.Structure;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using ABI.Framework.MS.Domain.Common;
using System;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.Repositories.Structure
{
    public class StructureNodeDefinitionsRespository: IStructureNodeDefinitionsRespository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return (IUnitOfWork)_context;
            }
        }
        private readonly StructureContext _context;
        
        public StructureNodeDefinitionsRespository(StructureContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

     
        //public async Task<StructureNodeDefinition> GetByIdAsync(int NodeDefinitionId)
        //{
            
        //    var entity = await _context.StructureNodeDefinitions
        //        .Where(b => b.Id == NodeDefinitionId).SingleOrDefaultAsync();
        //    return entity;
        //}
        public async ValueTask<StructureNodeDefinition> GetByIdAsync(int id) =>
    await _context.StructureNodeDefinitions.FindAsync(id);
        
    }
}
