using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.DTO.Compare;
using ABI.API.Structure.ACL.Truck.Application.DTO.ImportProcess;
using ABI.API.Structure.ACL.Truck.Application.DTO.Portal;
using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureModel;
using ABI.API.Structure.ACL.Truck.Application.Queries.StructureNodes;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureNodeAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Service
{
    public class TruckToPortalService : ITruckToPortalService
    {

        public StructureDomain _Structure { get; set; }
        public List<StructureArista> _StructureAristaItems { get; set; }

        private readonly IStructureRepository _repository;
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IMediator _mediator;

        public TruckToPortalService(IStructureRepository repository, IStructureNodeRepository structureNodeRepository, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _structureNodeRepository = structureNodeRepository ?? throw new ArgumentNullException(nameof(structureNodeRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Migrate

        /// <summary>
        /// Gets the nodes by level identifier asynchronous.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="levelId">The level identifier.</param>
        /// <returns></returns>
        public async Task<List<StructureNode>> GetNodesByLevelIdAsync(int structureId, int levelId)
        {
            return await _structureNodeRepository.GetNodesAsync(structureId, levelId);
        }

        public async Task<List<StructureNode>> GetNodesTerritoryAsync(int structureId)
        {
            return await _structureNodeRepository.GetNodesTerritoryAsync(structureId);
        }


        /// <summary>
        /// Migrates the estructure asynchronous.
        /// </summary>
        /// <param name="portal">The portal.</param>
        /// <returns></returns>
        public async Task<int> MigrateEstructureAsync(StructurePortalDTO portal, string name)
        {
            try
            {
                _Structure = new StructureDomain(name, portal.StructureModelId, null, portal.ValidityFrom);

                 var model =  await _mediator.Send(new GetStructureModelByIdQuery { StructureModelId = portal.StructureModelId });

                if (model != null)
                    _Structure.AddCode(GetCode(model, name));

                foreach (var item in portal.Nodes)
                {
                    BrowseNodes(_Structure.Id, item, null, item.IsRootNode);
                }

                var struc = _repository.Add(_Structure);

                await _repository.UnitOfWork.SaveEntitiesAsync();

                return await Task.Run(() => struc.Id);

            }
            catch (Exception ex)
            {
                throw new GenericException(ex.Message, ex);
            }
        }


        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="structureModel">The structure model.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private string GetCode(StructureModelDTO structureModel, string name)
        {
           if(structureModel != null && structureModel.Country != null)
            {
                var country = structureModel.Country.Code;
                var structureCode = structureModel.Code;
                name = name.Trim().Replace(" ", string.Empty);
                var code = $"{country}_{structureCode}_{name}";
     
                return code.Length <= 20 ? code : code.Substring(0, 20);
            }
            return "-";
        }



        /// <summary>
        /// Migrates the estructure clients asynchronous.
        /// </summary>
        /// <param name="clients">The clients.</param>
        /// <param name="date">The date.</param>
        /// <param name="territorys">The territorys.</param>
        /// <exception cref="GenericException"></exception>
        public async Task MigrateEstructureClientsAsync(IList<DataIODto> clients, DateTimeOffset date, List<StructureNode> territorys)
        {
            var nodeCode = string.Empty;
            var node = new StructureNode();

            foreach (var item in territorys)
            {
                var territorysClients = clients.Where(c => c.CliTrrId == item.Code).ToList();

                foreach (var itemClient in territorysClients)
                {

                    var client = new StructureClientNode(item.Id, itemClient.CliNom, itemClient.CliId, itemClient.CliSts, date);
                    item.StructureClientNodes.Add(client);
                }
            }

            await _structureNodeRepository.UnitOfWork.SaveChangesAsync();
        }


        /// <summary>
        /// Browses the nodes.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodeIdParent">The node identifier parent.</param>
        /// <param name="node">The node.</param>
        /// <param name="isRootNode">if set to <c>true</c> [is root node].</param>
        private void BrowseNodes(int structureId, NodePortalDTO node, StructureNode nodeParent, bool isRootNode = false)
        {
            StructureNode nodeA;

            if (nodeParent is null)
            {
                var nodeADefinition = new StructureNodeDefinition(node.AttentionModeId, node.RoleId, node.SaleChannelId, node.EmployeeId, node.ValidityFrom.Value, node.Name, node.Active);
                nodeADefinition.EditVacantPerson(node.VacantPerson);
                nodeA = new StructureNode(node.Code, node.LevelId);
                nodeA.AddDefinition(nodeADefinition);

            }
            else
            {
                nodeA = nodeParent;
            }

            if (isRootNode) { _Structure.Node = nodeA; }

            foreach (var item in node.Nodes)
            {
                var nodeBDefinition = new StructureNodeDefinition(item.AttentionModeId, item.RoleId, item.SaleChannelId, item.EmployeeId, item.ValidityFrom.Value, item.Name, item.Active);
                nodeBDefinition.EditVacantPerson(item.VacantPerson);
                var nodeB = new StructureNode(item.Code, item.LevelId);

                nodeB.AddDefinition(nodeBDefinition);
                var Arista = new StructureArista(_Structure, nodeA, _Structure, nodeB, RelationshipNodeType.Contains.Id, item.ValidityFrom.Value, item.ValidityTo);
                _Structure.AddStructureAristaItems(Arista);

                BrowseNodes(structureId, item, nodeB, item.IsRootNode);
            }
        }

        #endregion

        #region Node save

        /// <summary>
        /// Saves the compare.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public async Task<int> SaveCompare(int structureId, List<NodePortalCompareDTO> nodes)
        {
            var nodesPortal = await _mediator.Send(new GetAllNodeByStructureIdQuery { StructureId = structureId, ValidityFrom = DateTimeOffset.UtcNow.Today(-3) });


            foreach (var item in nodes)
            {
                switch (item.TypeActionNode)
                {
                    case TypeActionNode.New:
                        await SaveNewAsync(structureId, item, nodesPortal);
                        break;
                    case TypeActionNode.Draft:
                        await SaveDraft(item, nodesPortal);
                        break;
                    default:
                        break;
                }
            }

            return await Task.Run(() => structureId);
        }

        /// <summary>
        /// Saves the new.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodes">The nodes.</param>
        private async Task SaveNewAsync(int structureId, NodePortalCompareDTO nodeCompare, IList<PortalAristalDTO> nodesPortal)
        {
            try
            {
                using (var transaction = await _structureNodeRepository.UnitOfWork.BeginTransactionAsync())
                {

                    var validityFrom = DateTimeOffset.UtcNow.Today(-3);
                    var nodeParentNodeId = 0;

                    PortalAristalDTO nodeParent;

                    nodeParent = nodesPortal.FirstOrDefault(n => n.NodeLevelId == nodeCompare.ParentNodeLevelId.Value && n.NodeCode == nodeCompare.ParentNodeCode);

                    if (nodeParent == null)
                    {
                        var nodoParentDB = await _structureNodeRepository.GetNodoOneByCodeLevelAsync(structureId, nodeCompare.ParentNodeCode, nodeCompare.ParentNodeLevelId.Value);

                        if (nodoParentDB != null)
                        {
                            nodeParent = new PortalAristalDTO
                            {
                                NodeCode = nodoParentDB.Code,
                                NodeId = nodoParentDB.Id,
                                NodeLevelId = nodoParentDB.LevelId
                            };

                            nodeParentNodeId = nodeParent.NodeId;
                        }
                    }
                    else
                    {
                        nodeParentNodeId = nodeParent.NodeId;
                    }

                    int? idNull = null;

                    var nodeNew = new StructureNode(nodeCompare.Code, nodeCompare.LevelId);
                    var nodoDefinitionNew = new StructureNodeDefinition(nodeCompare.AttentionModeId ?? idNull, nodeCompare.RoleId ?? idNull, null, nodeCompare.EmployeeId, validityFrom, nodeCompare.Name, nodeCompare.Active);

                    nodeNew.AddDefinition(nodoDefinitionNew);

                    _structureNodeRepository.Add(nodeNew);
                    await _structureNodeRepository.UnitOfWork.SaveChangesAsync();

                    var nodeArista = new StructureArista(structureId, nodeParentNodeId, structureId, nodeNew.Id, RelationshipNodeType.Contains.Id, validityFrom, DateTimeOffset.MaxValue.ToOffset(-3)); //HACER: Ojo multipaís

                    _structureNodeRepository.AddArista(nodeArista);
                    await _structureNodeRepository.UnitOfWork.CommitTransactionAsync(transaction);

                }
            }
            catch (Exception)
            {
                _structureNodeRepository.UnitOfWork.RollbackTransaction();
            }

        }

        /// <summary>
        /// Saves the draft.
        /// </summary>
        /// <param name="structureId">The structure identifier.</param>
        /// <param name="nodes">The nodes.</param>
        private async Task SaveDraft(NodePortalCompareDTO nodeCompare, IList<PortalAristalDTO> nodesPortal)
        {
            try
            {
                using (var transaction = await _structureNodeRepository.UnitOfWork.BeginTransactionAsync())
                {

                    var validityFrom = DateTimeOffset.UtcNow.Today(-3);

                    var node = nodesPortal.FirstOrDefault(n => n.NodeLevelId == nodeCompare.LevelId && n.NodeCode == nodeCompare.Code);

                    var nodeDefininitio = await _structureNodeRepository.GetNodoDefinitionValidityByNodeIdAsync(node.NodeId, validityFrom);

                    nodeDefininitio.EditValidityTo(validityFrom.AddDays(-1));

                    _structureNodeRepository.UpdateNodoDefinition(nodeDefininitio);
                    await _structureNodeRepository.UnitOfWork.SaveEntitiesAsync();

                    var nodoDefinitionNew = new StructureNodeDefinition(node.NodeId, nodeCompare.AttentionModeId, nodeCompare.RoleId,
                                nodeDefininitio.SaleChannelId, nodeCompare.EmployeeId, validityFrom, nodeCompare.Name, nodeCompare.Active);

                    _structureNodeRepository.AddNodoDefinition(nodoDefinitionNew);

                    await _structureNodeRepository.UnitOfWork.SaveEntitiesAsync();
                    await _structureNodeRepository.UnitOfWork.CommitTransactionAsync(transaction);
                }
            }
            catch (Exception)
            {
                _structureNodeRepository.UnitOfWork.RollbackTransaction();
            }
        }

        #endregion

        #region Get Structure

        public void GetStructure(string name)
        {
            var structure = _repository.GetStructureDataCompleteByNameAsync(name);
        }

        #endregion

    }
}
