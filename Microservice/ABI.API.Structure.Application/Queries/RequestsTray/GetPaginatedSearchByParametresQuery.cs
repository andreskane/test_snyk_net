using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Exceptions;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.Extensions;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
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
    public class GetPaginatedSearchByParametersQuery : IRequest<PaginatedList<RequestTrayDTO>>
    {


        public List<Int32> sId { get; set; }
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTimeOffset validityFrom { get; set; }
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DateTimeOffset validityTo { get; set; }


        public List<Int32> KindOfChanges { get; set; }
        public List<String> Users { get; set; }
        public List<Int32> portalStates { get; set; }
        public List<Int32> externalSystems { get; set; }

        public List<Int32> filters { get; set; }




        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public int PageIndex { get; set; }
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public int PageSize { get; set; }

        [DataMember]
        // [Required(ErrorMessage = ErrorMessageText.Required)]
        public string SortOrder { get; set; }
        [DataMember]
        //   [Required(ErrorMessage = ErrorMessageText.Required)]
        public string SortDirection { get; set; }






    }


    public class GetPaginatedSearchByParametersQueryHandler : IRequestHandler<GetPaginatedSearchByParametersQuery, PaginatedList<RequestTrayDTO>>
    {
        private readonly IChangeTrackingRepository _changesRepo;
        private readonly IVersionedRepository _versionedRepository;

        public GetPaginatedSearchByParametersQueryHandler(
           IChangeTrackingRepository changesRepo,
           IStructureNodeRepository structureNodeRepository,
           IVersionedNodeRepository versionedNodeRepository,
           IVersionedRepository versionedRepository,
           IVersionedLogRepository versionedLogRepository
            )
        {
            _changesRepo = changesRepo;
            _versionedRepository = versionedRepository;
        }


        public async Task<PaginatedList<RequestTrayDTO>> Handle(GetPaginatedSearchByParametersQuery request, CancellationToken cancellationToken)
        {

            if (request.validityTo < request.validityFrom)
            {
                throw new GenericException($"La Fecha hasta: {request.validityTo} ,tiene que ser mayor que la fecha desde:{request.validityFrom}");
            }


            #region "Repositorios"
            //ver de usar el metodo de iqueriable GetAllAsync
            var changeTrackingResults = await _changesRepo.GetAll(true);

            var extChangeTrackingResults = await _versionedRepository.GetVersionByIdsStructureValidity(request.sId, request.validityFrom, request.validityTo);

           
            var query = (from str in changeTrackingResults
                         where
                         (request.sId == null || request.sId.Contains(str.IdStructure)) &&
                         (str.ValidityFrom <= request.validityTo && str.ValidityFrom >= request.validityFrom) &&
                         (request.KindOfChanges == null || request.KindOfChanges.Contains(str.IdChangeType)) &&
                         (request.Users == null || request.Users.Contains(str.User.Id.ToString())) &&
                          (request.portalStates == null || request.portalStates.Contains(str.GetPortalStatus(false)))&&
                          (request.externalSystems == null || request.externalSystems.Contains(Convert.ToInt32(str.ExternalStatus.Id)))

                         select new
                         {
                             id=str.Id,
                             Structure = str.Structure.Name,
                             StructureId = str.Structure.Id,
                             StructureCode = str.Structure.Code,
                             User = str.User.Id,
                             UserName = str.User.Name,
                             Validity = str.ValidityFrom,
                             ChangeType = str.IdChangeType,
                             ChangeNode = str.ChangedValueNode,
                             ChangeArista = str.ChangedValueArista,
                             Externo = str.Structure.StructureModel.CanBeExportedToTruck,
                             ListaNodos = str.NodePath.Ids,
                             NodoOrigenId = str.IdObjectType == (int)ChangeTrackingObjectType.Node ? str.ChangedValueNode.Node.Id.Value : 0,
                             PortalStatus = str.GetPortalStatus(false)
                         //   , ExternalStatus = str.ExternalStatus
                         });
 

            //aca filtramos listas de listas  de los nodos

            if (request.filters != null)
            {
                var queryf1 = query.Where(t2 => request.filters.Any(t1 => t2.ListaNodos.Contains(t1))).ToList();
                var queryf2 = query.Where(t2 => request.filters.Any(t1 => t2.NodoOrigenId.Equals(t1))).ToList();
                queryf1.AddRange(queryf2);
                query = queryf1;
            }





            List<ChangeTracking> changeStates = new List<ChangeTracking>();
            foreach (var item in extChangeTrackingResults)
            {
                var changeS = changeTrackingResults.Where(x => x.Structure.Id == item.StructureId).Where(f => f.ValidityFrom == item.Validity).ToList();
                changeS.ForEach(x => x.ExternalStatus = new GenericKeyValue { Id = item.VersionedStatus.Id.ToString(), Name = item.VersionedStatus.Name });
                 changeStates.AddRange(changeS);
            }




            var list = (from q in query.AsQueryable()
                        group q by new
                        {
                            q.StructureCode,
                            q.StructureId,
                            q.Structure,
                            q.User,
                            q.UserName,
                            q.Validity ,
                             q.PortalStatus
                        //  ,  q.ExternalStatus
                         //   q.Externo,
                         //   q.NodoOrigenId,
                            //q.ExternalStatus.Id
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
                            PortalStatus = qin.Key.PortalStatus,
                           TruckStatus= GetTruckStatus(changeStates,qin.Key.StructureId, qin.Key.Validity),
                        StructureCode = qin.Key.StructureCode
                        }).ToList();





            var pageNodes = PaginatedList<RequestTrayDTO>.Create(list, request.PageIndex, request.PageSize, "", "");

            return pageNodes;
            #endregion




        }


        private TruckStatusDTO GetTruckStatus(List<ChangeTracking> changeStates, Int32 StructureID,DateTimeOffset ValidPeriod) {

            TruckStatusDTO ExtStatus = new TruckStatusDTO { Code = 0, Message = "" };

            var res = changeStates.Where(x => x.IdStructure == StructureID).Where(f => f.ValidityFrom == ValidPeriod).OrderByDescending(i => i.Id).FirstOrDefault() ;
            if (res != null)
            {
                ExtStatus.Code =Convert.ToInt32( res.ExternalStatus.Id);
                ExtStatus.Message= res.ExternalStatus.Name;
            }

            return ExtStatus;
        }

        private List<string> GetKindsOfChange(IEnumerable<dynamic> query, int structureId, DateTimeOffset validityFrom, string user)
        {
            var changes = query.Where(q => q.Validity == validityFrom && q.StructureId == structureId && q.UserName == user)
                    .Select(q => q.ChangeType).Distinct().ToList();
            if (changes.Count > 0)
            {
                List<string> listChanges = new List<string>();
                foreach (var item in changes)
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

    }
}
