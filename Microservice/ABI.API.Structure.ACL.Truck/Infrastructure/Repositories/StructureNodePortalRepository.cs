using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Repositories
{
    public class StructureNodePortalRepository : IStructureNodePortalRepository
    {
        private readonly StructureContext _context;

        public StructureNodePortalRepository(StructureContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public void UpdateNodoDefinition(StructureNodeDefinition entity)
        {
            _context.StructureNodeDefinitions.Update(entity);
        }

        public async Task SaveEntitiesAsync()
        {
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<StructureNodeDefinition> GetNodeDefinitionsByIdAsync(int id)
        {
           return await _context.StructureNodeDefinitions.FindAsync(id);
        }

            
        /// <summary>
        /// Gets all grade changes for truck.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="validityFrom">The date.</param>
        /// <returns></returns>
        public async Task<IList<NodePortalTruckDTO>> GetAllGradeChangesForTruck(int structureId, DateTimeOffset validityFrom)
        {
            //Cambios Programados
            var query1 = await (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdTo).DefaultIfEmpty()
                                join np in _context.StructureNodes.AsNoTracking() on a.NodeIdFrom equals np.Id
                                where a.StructureIdFrom == structureId && nd.ValidityFrom == validityFrom
                                select new NodePortalTruckDTO
                                {
                                    NodeId = nd.NodeId,
                                    Name = nd.Name,
                                    ActiveNode = nd.Active,
                                    Code = n.Code,
                                    LevelId = n.LevelId,
                                    AttentionModeId = nd.AttentionModeId,
                                    RoleId = nd.RoleId,
                                    SaleChannelId = nd.SaleChannelId,
                                    EmployeeId = nd.EmployeeId,
                                    ValidityFrom = nd.ValidityFrom,
                                    ValidityTo = nd.ValidityTo,
                                    IsRootNode = false,
                                    NodeIdParent = a.NodeIdFrom,
                                    ParentNodeCode = np.Code,
                                    ChildNodeCode = null,
                                    NodeDefinitionId = nd.Id
                                }).ToListAsync();



            var query1List1 = query1.Distinct().ToList();

            var nodesQuery1 = query1List1.Select(n => n.NodeId).Distinct().ToList();

            // tipo borrador raiz
            var query2 = await (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                                     join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                                     from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdFrom).DefaultIfEmpty()
                                                     where a.StructureIdFrom == structureId && nd.ValidityFrom == validityFrom
                                                     select new NodePortalTruckDTO
                                                     {
                                                         NodeId = nd.NodeId,
                                                         Name = nd.Name,
                                                         ActiveNode = nd.Active,
                                                         Code = n.Code,
                                                         LevelId = n.LevelId,
                                                         AttentionModeId = nd.AttentionModeId,
                                                         RoleId = nd.RoleId,
                                                         SaleChannelId = nd.SaleChannelId,
                                                         EmployeeId = nd.EmployeeId,
                                                         ValidityFrom = nd.ValidityFrom,
                                                         ValidityTo = nd.ValidityTo,
                                                         IsRootNode = true,
                                                         NodeIdParent = 0,
                                                         ParentNodeCode = string.Empty,
                                                         ChildNodeCode = null,
                                                         NodeDefinitionId = nd.Id
                                                     }).ToListAsync();


            var query1List2 = query2.Where(x => !nodesQuery1.Contains(x.NodeId)).ToList();
            var list = query1List1.Union(query1List2).ToList();

            return  list;

        }

        public async Task<IList<PortalAristalDTO>> GetAllAristasGradeChangesForTruck(int structureId, DateTimeOffset validityFrom)
        {

            var listAristas = await (from a in _context.StructureAristas.AsNoTracking()
                               where a.StructureIdFrom == structureId && a.ValidityFrom == validityFrom
                               select new PortalAristalDTO
                               {
                                   AristaId = a.Id,
                                   NodeId = a.NodeIdTo
                               }

                               ).ToListAsync();

            return  listAristas;

        }

        /// <summary>
        /// Gets all child node for truck.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="validityFrom">The date.</param>
        /// <returns></returns>
        public async Task<IList<NodePortalTruckDTO>> GetAllChildNodeForTruck(int structureId, int nodeId, DateTimeOffset validityFrom)
        {
            var query = await (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                                    join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                                    from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdTo).DefaultIfEmpty()
                                                    join np in _context.StructureNodes.AsNoTracking() on a.NodeIdFrom equals np.Id
                                                    where a.StructureIdFrom == structureId && a.NodeIdFrom == nodeId && nd.ValidityFrom <= validityFrom
                                                    select new NodePortalTruckDTO
                                                    {
                                                        NodeId = nd.NodeId,
                                                        Name = nd.Name,
                                                        ActiveNode = nd.Active,
                                                        Code = n.Code,
                                                        LevelId = n.LevelId,
                                                        AttentionModeId = nd.AttentionModeId,
                                                        RoleId = nd.RoleId,
                                                        SaleChannelId = nd.SaleChannelId,
                                                        EmployeeId = nd.EmployeeId,
                                                        ValidityFrom = nd.ValidityFrom,
                                                        ValidityTo = nd.ValidityTo,
                                                        IsRootNode = false,
                                                        NodeIdParent = a.NodeIdFrom,
                                                        ParentNodeCode = np.Code,
                                                        ChildNodeCode = null,
                                                        NodeDefinitionId = n.Id
                                                    }).ToListAsync();


            return query.Distinct().ToList();
        }

        /// <summary>
        /// Gets the nodo parent.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public async Task<NodePortalTruckDTO> GetNodoParent(int structureId, int nodeId)
        {
              IQueryable<NodePortalTruckDTO> query1 = (from nd in _context.StructureNodeDefinitions.AsNoTracking()
                                                     join n in _context.StructureNodes.AsNoTracking() on nd.NodeId equals n.Id
                                                     from a in _context.StructureAristas.AsNoTracking().Where(a => nd.NodeId == a.NodeIdTo).DefaultIfEmpty()
                                                     join np in _context.StructureNodes.AsNoTracking() on a.NodeIdFrom equals np.Id
                                                     where a.StructureIdFrom == structureId && n.Id == nodeId
                                                     select new NodePortalTruckDTO
                                                     {
                                                         NodeId = nd.NodeId,
                                                         Name = nd.Name,
                                                         ActiveNode = nd.Active,
                                                         Code = n.Code,
                                                         LevelId = n.LevelId,
                                                         AttentionModeId = nd.AttentionModeId,
                                                         RoleId = nd.RoleId,
                                                         SaleChannelId = nd.SaleChannelId,
                                                         EmployeeId = nd.EmployeeId,
                                                         ValidityFrom = nd.ValidityFrom,
                                                         ValidityTo = nd.ValidityTo,
                                                         IsRootNode = false,
                                                         NodeIdParent = a.NodeIdFrom,
                                                         ParentNodeCode = np.Code,
                                                         ChildNodeCode = null,
                                                         NodeDefinitionId = nd.Id
                                                     });

            var value = await query1.FirstOrDefaultAsync();

            return value;

        }

        /// <summary>
        /// Gets the nodo parent by identifier.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public async Task<NodePortalTruckDTO> GetNodoById( int nodeId)
        {
            IQueryable<NodePortalTruckDTO> query1 =  (from n in _context.StructureNodes.AsNoTracking()
                                                     where  n.Id == nodeId
                                                     select new NodePortalTruckDTO
                                                     {
                                                         NodeId = n.Id,
                                                         Code = n.Code,
                                                         LevelId = n.LevelId,
                                                         IsRootNode = false                                          
                                                     });

            var value = await query1.FirstOrDefaultAsync();

            return value;

        }



    }
}