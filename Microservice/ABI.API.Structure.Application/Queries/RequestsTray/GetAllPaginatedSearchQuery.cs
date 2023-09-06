using ABI.API.Structure.ACL.Truck.Application;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using ABI.Framework.MS.Helpers.Message;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.RequestsTray
{
    public class GetAllPaginatedSearchQuery : IRequest<PaginatedList<RequestTrayDTO>>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public PaginatedSearchDTO model { get; set; }
    }

    public class GetAllPaginatedSearchQueryHandler : IRequestHandler<GetAllPaginatedSearchQuery, PaginatedList<RequestTrayDTO>>
    {
        private readonly IChangeTrackingRepository _changeTrackingRepository;
        private readonly IStructureNodeRepository _structureNodeRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly IVersionedNodeRepository _versionedNodeRepository;
        private readonly IVersionedRepository _versionedRepository;
        private readonly IVersionedLogRepository _versionedLogRepository;

        public GetAllPaginatedSearchQueryHandler(
            IChangeTrackingRepository repository,
            IStructureNodeRepository structureNodeRepository,
            IStructureRepository structureRepository,
            IVersionedNodeRepository versionedNodeRepository,
            IVersionedRepository versionedRepository,
            IVersionedLogRepository versionedLogRepository
        )
        {
            _changeTrackingRepository = repository;
            _structureRepository = structureRepository;
            _structureNodeRepository = structureNodeRepository;
            _versionedNodeRepository = versionedNodeRepository;
            _versionedRepository = versionedRepository;
            _versionedLogRepository = versionedLogRepository;
        }

        public async Task<PaginatedList<RequestTrayDTO>> Handle(GetAllPaginatedSearchQuery request, CancellationToken cancellationToken)
        {
            var changeTrackingResults = await _changeTrackingRepository.GetAll(true);
            var structures = await _structureRepository.GetAllAsync();
            
            var query = (from str in changeTrackingResults
                         join s in structures on str.IdStructure equals s.Id
                         where str.ValidityFrom > s.ValidityFrom
                         select new
                         {
                             Structure = s.Name,
                             StructureId = s.Id,
                             User = str.User.Id,
                             UserName = str.User.Name,
                             Validity = str.ValidityFrom,
                             ChangeType = str.IdChangeType,
                             ChangeNode = str.ChangedValueNode,
                             ChangeArista = str.ChangedValueArista
                         });

            var list = (from q in query.AsQueryable()
                        group q by new
                        {
                            q.StructureId,
                            q.Structure,
                            q.User,
                            q.UserName,
                            q.Validity
                        } into qin
                        select new RequestTrayDTO
                        {
                            Structure = new ItemDTO
                            {
                                Id = qin.Key.StructureId,
                                Name = qin.Key.Structure
                            },
                            User = new UserDTO
                            {
                                Id = qin.Key.User,
                                Name = qin.Key.UserName
                            },
                            Validity = qin.Key.Validity,
                            ChangeType = GetKindsOfChange(query, qin.Key.StructureId, qin.Key.Validity, qin.Key.UserName),
                            PortalStatus = GetPortalStatus(qin.Key.Validity, false),
                        }).ToList();

            foreach (var item in list)
            {
                item.TruckStatus = await GetTruckStatusAsync(query, item.Validity);
                //item.User = item.TruckStatus != null ? item.User : new UserDTO { Name = "CAMBIO DE TRUCK" };
            }

            var pageNodes = PaginatedList<RequestTrayDTO>.Create(list, request.model.PageIndex, request.model.PageSize, "", "");

            return pageNodes;
        }

        private List<string> GetKindsOfChange(IEnumerable<dynamic> query, int structureId, DateTimeOffset validity, string user)
        {
            var changes = query.Where(q => q.Validity == validity && q.StructureId == structureId && q.UserName == user)
                    .Select(q => q.ChangeType).Distinct().ToList();
            if (changes.Count>0)
            {
                List<string> listChanges = new List<string>();
                foreach(var item in changes)
                {
                    listChanges.Add(GetChangeType(item));
                }
                return listChanges;
            }
            return null;
        }

        private string GetChangeType(int item)
        {
            switch (item)
            {
                case 4:
                    return "Estructura";
                case 5:
                    return "Rol";
                case 6:
                    return "Persona";
                default:
                    return "Estructura";
            }
        }

        /// <summary>
        /// Gets the portal status.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="cancelled">if set to <c>true</c> [cancelled].</param>
        /// <returns></returns>
        private int GetPortalStatus(DateTimeOffset date, bool cancelled)
        {
            if (cancelled)
            {
                return 3;
            }
            else
                if (date.ToUniversalTime() > DateTimeOffset.UtcNow)
            {
                return 1;
            }
            else
                return 2;
        }


        /// <summary>
        /// Gets the truck status asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private async Task<TruckStatusDTO> GetTruckStatusAsync(IEnumerable<dynamic> query, DateTimeOffset date)
        {

            var node = query.FirstOrDefault(q => q.Validity == date);
            var nodedefinition = default(StructureNodeDefinition);

            if (node.ChangeNode != null)
                nodedefinition = await _structureNodeRepository.GetNodoDefinitionValidityByNodeIdAsync(node.ChangeNode.Node.Id);
            else
                nodedefinition = await _structureNodeRepository.GetNodoDefinitionValidityByNodeIdAsync(node.ChangeArista.AristaNueva.NodeIdFrom);

            if (nodedefinition != null)
            {
                var logNodeDefinition = await _versionedNodeRepository.GetOneByNodeDefinitionId(nodedefinition.Id);

                if (logNodeDefinition != null)
                {
                    var truckStatus = await _versionedRepository.GetById(logNodeDefinition.VersionedId);

                    var status = new TruckStatusDTO();

                    if (truckStatus.StatusId == (int)VersionedState.Rechazado)
                    {
                        var log = _versionedLogRepository.GetLastStateByVersionedId(logNodeDefinition.VersionedId);

                        switch (truckStatus.StatusId)
                        {
                            case 100:
                            case 101:
                            case 102:
                            case 103:
                            case 104:
                            case 105:
                            case 106: //errores
                                status.Message = log.Status.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    return status;
                } 
            }

            return null;
        }
    }
}
