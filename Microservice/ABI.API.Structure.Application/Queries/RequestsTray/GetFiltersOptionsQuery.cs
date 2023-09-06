using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck.Exceptions;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.DTO.FiltresDto;
using ABI.API.Structure.Application.DTO.RequestTray;
using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using ABI.API.Structure.Domain.Entities;
using ABI.API.Structure.Domain.Enums;
using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.Application.Queries.RequestsTray
{
    public class GetFiltersOptionsQuery : FiltersOptionsQueryDTO, IRequest<RequestTrayFiltersDTO>
    {
        public GetFiltersOptionsQuery() : base()
        {



        }
    }

    public class GetFiltersOptionsQueryHandler : IRequestHandler<GetFiltersOptionsQuery, RequestTrayFiltersDTO>
    {
        private readonly IChangeTrackingRepository _changesRepo;
        private readonly IStructureNodeRepository _structureNodeDefinitionRepository;
        private readonly ILogger<GetFiltersOptionsQueryHandler> _logger;
         

        private readonly IVersionedRepository _externalTray;




        public GetFiltersOptionsQueryHandler(
           IChangeTrackingRepository changesRepo,
                IStructureNodeRepository structureNodeDefinitionRepository,
                                                              
                                                   IVersionedRepository externalTray,
            ILogger<GetFiltersOptionsQueryHandler> logger
            )
        {
            _changesRepo = changesRepo;
            _structureNodeDefinitionRepository = structureNodeDefinitionRepository;
           
            _externalTray = externalTray;
            _logger = logger;
        }


        public async Task<RequestTrayFiltersDTO> Handle(GetFiltersOptionsQuery request, CancellationToken cancellationToken)
        {

            if (request.PeriodTo < request.PeriodFrom)
            {
                throw new GenericException($"La Fecha hasta: {request.PeriodTo} ,tiene que ser mayor que la fecha desde:{request.PeriodFrom}");
            }
            var result = new RequestTrayFiltersDTO();
            try

            {
                #region "Repositorios"
                //ver de usar el metodo de iqueriable GetAllAsync
                var changes = await _changesRepo.GetAll(true);
                //if (request.sId.Length > 0)
                //{
                //    changes = changes.Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Node).Where(x => x.ValidityFrom <= request.PeriodTo & x.ValidityFrom >= request.PeriodFrom).Where(s => request.sId.Contains(s.IdStructure)).ToList();
                //}
                //else
                //{
                //    changes = changes.Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Node).Where(x => x.ValidityFrom <= request.PeriodTo & x.ValidityFrom >= request.PeriodFrom).ToList();
                //}

                if (request.sId.Length > 0)
                {
                    changes = changes.Where(x => x.ValidityFrom <= request.PeriodTo & x.ValidityFrom >= request.PeriodFrom).Where(s => request.sId.Contains(s.IdStructure)).ToList();
                }
                else
                {
                    changes = changes.Where(x => x.ValidityFrom <= request.PeriodTo & x.ValidityFrom >= request.PeriodFrom).ToList();
                }




                var extChangeTrackingResults = await _externalTray.GetVersionByIdsStructureValidity(request.sId.ToList(), request.PeriodFrom, request.PeriodTo);

                foreach (var item in extChangeTrackingResults)
                {

                    List<ChangeTracking> change = changes.Where(x => x.Structure.Id == item.StructureId)
                                  .Where(f => f.ValidityFrom == item.Validity).ToList();

                    change.ForEach(x => x.ExternalStatus = new GenericKeyValue { Id = item.VersionedStatus.Id.ToString(), Name = item.VersionedStatus.Name });

                }


                var ListaNodos = changes.GroupBy(x => x.NodePath).ToList();

                List<Int32> IdsNodos = new List<Int32>();
                foreach (var nodo in ListaNodos)
                {
                    IdsNodos.AddRange(nodo.Key.Ids);
                }
                IdsNodos.AddRange(changes.Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Node).Select(z => z.ChangedValueNode.Node.Id.Value).ToList());

                IdsNodos.AddRange(changes.Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Arista).Select(z => z.ChangedValueArista.AristaNueva.NodeIdFrom).ToList());

                IdsNodos = IdsNodos.OrderBy(x => x).Distinct().ToList();


                IList<StructureNodeDefinition> ListaDetalleNodos = await _structureNodeDefinitionRepository.GetNodosDefinitionByIdsNodoAsync(IdsNodos);
                List<filtersLevelDto> ListFilterLevel;


                //aca vamos con los nodos cargados
                

                ListFilterLevel = ListaDetalleNodos.Select(x => new filtersLevelDto
                {
                    Id = x.NodeId.ToString(),
                    Name = x.Name,
                    levelId = x.Node.LevelId,
                    parents =
                    changes
                    .Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Node)
                    .Where(c => c.ChangedValueNode.Node.Id == x.NodeId)
                            .Select(s => s.NodePath.Ids.ToList()
                            ).FirstOrDefault()

                }).Where(x => x.parents != null).ToList();


                //var ListFilterLevelAriastas = ListaDetalleNodos.Select(x => new filtersLevelDto
                //{
                //    Id = x.NodeId.ToString(),
                //    Name = x.Name,
                //    levelId = x.Node.LevelId,
                //    parents =
                //    changes
                //    .Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Arista)
                //    .Where(c => c.ChangedValueNode.Node.Id == x.NodeId)
                //            .Select(s => s.NodePath.Ids.ToList()
                //            ).FirstOrDefault()

                //}).Where(x => x.parents != null).ToList();



                ListFilterLevel = ListFilterLevel.GroupBy(p => p.Id).Select(g => g.First()).OrderBy(o => o.Id).ToList();

                //voy con los que no estan en cambios y arman el menu
                var ListaDetalleNodosSinCambios = ListaDetalleNodos.Where(x => !ListFilterLevel.Any(l2 => l2.Id == x.NodeId.ToString())).ToList();

                var ListFilterLevel2 = ListaDetalleNodosSinCambios.Select(x => new filtersLevelDto
                {
                    Id = x.NodeId.ToString(),
                    Name = x.Name,
                    levelId = x.Node.LevelId,
                    parents = CleanParents(x.NodeId, changes
                       .Where(w => w.IdObjectType == (int)ChangeTrackingObjectType.Node)
                       .Where(c => c.NodePath.Ids.Contains(x.NodeId))
                       .Select(s => s.NodePath.Ids.ToList()).FirstOrDefault())
                }).ToList();

                ListFilterLevel2 = ListFilterLevel2.GroupBy(p => p.Id).Select(g => g.First()).OrderBy(o => o.Id).ToList();

                ListFilterLevel.AddRange(ListFilterLevel2);


                #endregion
                #region "niveles"

                List<GenericKeyValue> ListaNiveles =
                    ListaDetalleNodos
                    .Select(g => new GenericKeyValue { Id = g.Node.Level.Id.ToString(), Name = g.Node.Level.Name }).Distinct().ToList();
                ListaNiveles = ListaNiveles.GroupBy(p => p.Id).Select(g => g.First()).OrderBy(o => o.Id).ToList();



                #endregion




                #region "Filtros Basicos"

                List<StructureFilterDto> ListaEstructuras = changes.GroupBy(s => s.Structure).Select(m => new StructureFilterDto
                {
                    Id = m.Key.Id.ToString(),
                    Name = m.Key.Name
                }).OrderBy(o => o.Name).ToList();


                List<KindOfChangeFilterDto> ListaTipoCambios = changes.GroupBy(x => new { x.IdChangeType, value = x.KindOfChange() })
        .Select(s => new KindOfChangeFilterDto
        {
            Id = s.Key.IdChangeType.ToString(),
            Name = s.Key.value,
            //Name = KindOfChange(s.Key.IdChangeType),

            structureIds = s.Select(i => i.IdStructure.ToString()).Distinct().ToList()
        }).ToList();



                var ListaUsuarios = changes.GroupBy(x => new { x.User.Id, x.User.Name })
                   .Select(g => new UserFilterDto
                   {
                       Id = g.Key.Id.ToString(),
                       Name = g.Key.Name != null ? g.Key.Name : "N/A",
                       structureIds = g.Select(i => i.IdStructure.ToString()).Distinct().ToList(),
                       kindOfChangeIds = g.Select(i => i.IdChangeType.ToString()).Distinct().ToList()
                   }).ToList();

 




                var ListaEstados = changes.GroupBy(x => new { Id = x.GetPortalStatus(false) })
   .Select(g => new PortalStatesFilterDto
   {
       Id = g.Key.Id.ToString(),
       Name = ((StatusPortal)g.Key.Id).ToString(),
       structureIds = g.Select(i => i.IdStructure.ToString()).Distinct().ToList(),
       kindOfChangeIds = g.Select(i => i.IdChangeType.ToString()).Distinct().ToList(),
       userIds = g.Select(i => i.User.Id.ToString()).Distinct().ToList()

   }).ToList();



                //List<Int32> ListaIdAristas = new List<int>();

                //var ListExternalStatus = await _externalTray.GetVersionByIdsValidity(ListaDetalleNodos.Select(x => x.Id).ToList(), ListaIdAristas, request.PeriodTo);



                //saco los estados
                var ListaSistemasExternos = changes.Where(x=>x.ExternalStatus!=null).GroupBy(x => new { id = x.ExternalStatus.Id, name = x.ExternalStatus.Name })
                    .Select(g => new ExternalSystemsFilterDto
                    {
                        Id = g.Key.id.ToString(),
                        Name = g.Key.name,
                        structureIds = g.Select(i => i.IdStructure.ToString()).Distinct().ToList(),
                        kindOfChangeIds = g.Select(i => i.IdChangeType.ToString()).Distinct().ToList(),
                        userIds = g.Select(i => i.User.Id.ToString()).Distinct().ToList(),
                        portalStatusIds = g.Select(i => i.ChangeStatus.Status.Id.ToString()).Distinct().ToList()
                        
                    }).ToList();




 
                #endregion





                result.Structures = ListaEstructuras;
                result.KindOfChanges = ListaTipoCambios;
                result.Users = ListaUsuarios;
                result.portalStates = ListaEstados;
                result.externalSystems = ListaSistemasExternos;
                result.levels = ListaNiveles;
                result.filters = ListFilterLevel;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.StackTrace);
            }
            return result;
        }



        private List<Int32> CleanParents(Int32 parent, List<Int32> Ids)
        {
            List<Int32> res= new List<int>();

            if (Ids != null)
            {
                int idx = Ids.IndexOf(parent);


                if (idx + 1 < Ids.Count)
                {
                    res = Ids.GetRange(idx + 1, Ids.Count - (idx + 1));
                }
                else
                {
                    res = null;
                }

            }


            return res;
        }

    }

}
