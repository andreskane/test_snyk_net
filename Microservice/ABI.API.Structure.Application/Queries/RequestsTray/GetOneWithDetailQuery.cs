using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Domain.AggregatesModel.ChangeTrackingAggregate;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.RequestsTray
{
    public class GetOneWithDetailQuery : IRequest<IList<RequestTrayDetailDTO>>
    {
        public int structureId { get; set; }
        public DateTimeOffset validity { get; set; }
        public Guid userId { get; set; }
    }

    public class GetOneWithDetailQueryHandler : IRequestHandler<GetOneWithDetailQuery, IList<RequestTrayDetailDTO>>
    {
        private readonly IChangeTrackingRepository _changesRepo;
         private readonly IAttentionModeRepository _attentionModeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISaleChannelRepository _saleChannelRepository;
        private readonly IStructureNodeRepository _structureNodeRepository;


        public GetOneWithDetailQueryHandler(
            IChangeTrackingRepository changesRepo, 
            IStructureNodeRepository structureNodeRepository,
            IAttentionModeRepository attentionModeRepository, 
            IRoleRepository roleRepository, 
            ISaleChannelRepository saleChannelRepository)
        {
            _changesRepo = changesRepo;
             _attentionModeRepository = attentionModeRepository;
            _roleRepository = roleRepository;
            _saleChannelRepository = saleChannelRepository;
            _structureNodeRepository = structureNodeRepository;
        }


        public async Task<IList<RequestTrayDetailDTO>> Handle(GetOneWithDetailQuery request, CancellationToken cancellationToken)
        {
            //Cambios en la estructura
            var changeTrackingResults = await _changesRepo.GetByStructureId(request.structureId);

            changeTrackingResults = changeTrackingResults.Where(x => x.ChangeStatus != null && x.ChangeStatus.Status != null && x.ChangeStatus.Status.Id == (int)ChangeTrackingStatusCode.Confirmed).ToList();

            var result = new List<RequestTrayDetailDTO>();
           
            var listChanges = changeTrackingResults
                                .Where(chg => chg.User.Id == request.userId && chg.ValidityFrom == request.validity)
                                .ToList();

            var listChangesByMadeDate = listChanges.GroupBy(x => x.CreateDate.Date).ToList();

            foreach (var item in listChangesByMadeDate)
            {
                var itemDetail = new RequestTrayDetailDTO();
                var dateMade = new DateTimeOffset(item.Key, TimeSpan.FromHours(-3)); //HACER: Ojo multipais
                itemDetail.Made = dateMade;
                itemDetail.ChangesByNode = new List<RequestTrayDetailChangesDTO>();

                var changesNodes = listChanges.Where(x => x.CreateDate.Date == item.Key && x.IdObjectType == (int)ChangeTrackingObjectType.Node).ToList();
                var changesAristas = listChanges.Where(x => x.CreateDate.Date == item.Key && x.IdObjectType == (int)ChangeTrackingObjectType.Arista).ToList();
                foreach(var arista in changesAristas)
                {
                    changesNodes.Add(new ChangeTracking {
                        Id = arista.Id,
                        IdObjectType = (int)ChangeTrackingObjectType.Node,
                        ChangedValueNode = new ChangeTrackingNode
                        {
                            Node = await GetAristaText(arista),
                            OldValue = GetDescription(arista.ChangedValueArista.AristaActual.OldValue),
                            NewValue = GetDescription(arista.ChangedValueArista.AristaActual.NewValue),
                            Field = "NodeParentId",
                            FieldName = "Depende de"
                        },
                        CreateDate = arista.CreateDate
                    });
                }

                foreach (var itemByNode in changesNodes)
                {
                    if (!itemDetail.ChangesByNode.Any(x => x.Node.Id == itemByNode.ChangedValueNode.Node.Id))
                    {
                        var itemChanges = new RequestTrayDetailChangesDTO();
                        itemChanges.Node = new ItemNodeDTO();

                        itemChanges.Node.Id = itemByNode.ChangedValueNode.Node.Id;
                        itemChanges.Node.Name = itemByNode.ChangedValueNode.Node.Name;
                        itemChanges.Node.Code = itemByNode.ChangedValueNode.Node.Code;

                        itemChanges.Changes = new List<RequestTrayDetailChangeValuesDTO>();
                        var changesByNodes = changesNodes.Where(
                                    x => x.CreateDate.Date == item.Key &&
                                    x.ChangedValueNode.Node.Id == itemByNode.ChangedValueNode.Node.Id
                                    ).ToList();

                        foreach (var changeByNode in changesByNodes)
                        {
                            var itemDetailValues = new RequestTrayDetailChangeValuesDTO();
                            itemDetailValues.Id = changeByNode.Id;
                            itemDetailValues.Old = changeByNode.ChangedValueNode.OldValue;
                            itemDetailValues.New = changeByNode.ChangedValueNode.NewValue;
                            itemDetailValues.Field = changeByNode.ChangedValueNode.Field;
                            itemDetailValues.FieldName = changeByNode.ChangedValueNode.FieldName;
                            GetChangesValues(itemDetailValues);
                            itemChanges.Changes.Add(itemDetailValues);
                        }
                        itemDetail.ChangesByNode.Add(itemChanges); 
                    }
                }
                result.Add(itemDetail);
            }

            return result;
        }

        private string GetDescription(ItemAristaCompare value)
        {
            string description = value.Description;
            if (string.IsNullOrEmpty(description))
                description = value.NodeIdFrom.ToString();
            return description;
        }

        private async Task<ItemNode> GetAristaText(ChangeTracking arista) 
        {
            var newArista = await _structureNodeRepository.GetAristaById(arista.ChangedValueArista.AristaNueva.AristaId);
            var nodeName = "n/a";
            var nodeCode = "n/a";
            if (arista.IdDestino != 0)
            {
                var nodeTo = await _structureNodeRepository.GetNodoDefinitionValidityByNodeIdNoTrackingAsync(arista.IdDestino, arista.ValidityFrom);
                if (nodeTo != null)
                {
                    nodeName = nodeTo.Name;
                    nodeCode = nodeTo.Node.Code;
                }
            }
            ItemNode res = new ItemNode
            {
                Id = newArista?.NodeIdTo,
                Code = nodeCode,
                Name= nodeName
            };
            return res;
        }

        private void GetChangesValues(RequestTrayDetailChangeValuesDTO change)
        {
            switch (change.Field)
            {
                case "Active":
                    change.Old = change.Old == "FALSE" ? "Inactivo" : "Activo";
                    change.New = change.New == "FALSE" ? "Inactivo" : "Activo";
                    break;
                case "AttentionMode":
                    if (!string.IsNullOrEmpty(change.Old))
                    {
                        var attentionModeOld = _attentionModeRepository.GetById(Convert.ToInt32(change.Old));
                        if (attentionModeOld.Result != null)
                            change.Old = attentionModeOld.Result.Name;
                    }
                    if (!string.IsNullOrEmpty(change.New))
                    {
                        var attentionModeNew = _attentionModeRepository.GetById(Convert.ToInt32(change.New));
                        if (attentionModeNew.Result != null)
                            change.New = attentionModeNew.Result.Name;
                    }
                    break;
                case "Role":
                    if (!string.IsNullOrEmpty(change.Old))
                    {
                        var roleOld = _roleRepository.GetById(Convert.ToInt32(change.Old));
                        if (roleOld.Result != null)
                            change.Old = roleOld.Result.Name;
                    }

                    if (!string.IsNullOrEmpty(change.New))
                    {
                        var roleNew = _roleRepository.GetById(Convert.ToInt32(change.New));
                        if (roleNew.Result != null)
                            change.New = roleNew.Result.Name;
                    }
                    break;
                case "SaleChannel":
                    if (!string.IsNullOrEmpty(change.Old))
                    {
                        var saleChannelOld = _saleChannelRepository.GetById(Convert.ToInt32(change.Old));
                        if (saleChannelOld.Result != null)
                            change.Old = saleChannelOld.Result.Name;
                    }

                    if (!string.IsNullOrEmpty(change.New))
                    {
                        var saleChannelNew = _saleChannelRepository.GetById(Convert.ToInt32(change.New));
                        if (saleChannelNew.Result != null)
                            change.New = saleChannelNew.Result.Name;
                    }
                    break;
            }
        }
    }
}
