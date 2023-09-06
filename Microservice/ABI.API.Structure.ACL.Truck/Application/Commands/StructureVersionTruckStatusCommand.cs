using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.ACL.Truck.Application.Exceptions;
using ABI.API.Structure.ACL.Truck.Application.Extensions;
using ABI.API.Structure.ACL.Truck.Application.Queries.Structure;
using ABI.API.Structure.ACL.Truck.Application.Service.Interfaces;
using ABI.API.Structure.ACL.Truck.Repositories.Interfaces;
using ABI.API.Structure.APIClient.Truck;
using ABI.API.Structure.APIClient.Truck.Entities.EstructuraVersiones;
using ABI.Framework.MS.Helpers.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABI.API.Structure.ACL.Truck.Application.Commands
{
    public class StructureVersionTruckStatusCommand : IRequest<PendingVersionTruckDTO>
    {
        public int StructureId { get; set; }
        public int? VersionTruck { get; set; }
    }

    public class StructureVersionTruckStatusCommandHandler : IRequestHandler<StructureVersionTruckStatusCommand, PendingVersionTruckDTO>
    {
        private readonly IMediator _mediator;
        private readonly IMapeoTableTruckPortal _mapeoTableTruckPortal;
        private readonly ITruckService _truckService;
        private readonly IApiTruck _apiTruck;

        public StructureVersionTruckStatusCommandHandler(   IMediator mediator,
                                                            IMapeoTableTruckPortal mapeoTableTruckPortal,
                                                            ITruckService truckService,
                                                             IApiTruck apiTruck)
        {
            _mediator = mediator;
            _mapeoTableTruckPortal = mapeoTableTruckPortal;
            _truckService = truckService;
            _apiTruck = apiTruck;
        }


        public async Task<PendingVersionTruckDTO> Handle(StructureVersionTruckStatusCommand request, CancellationToken cancellationToken)
        {
            var structure = await _mediator.Send(new GetByIdQuery { StructureId = request.StructureId }, cancellationToken);

            if (structure.StructureModel.CanBeExportedToTruck)
            {
                var mapping = await _mapeoTableTruckPortal.GetAllBusinessTruckPortal();
                var businessTruckPortal = mapping.FirstOrDefault(b => b.Name == structure.Name);

                if (businessTruckPortal != null)
                {
                    var company = businessTruckPortal.BusinessCode.ToInt();

                    var versions = await GetStructureVersionTruckStatus(company,null);

                    var status = await GetPendingVersionTruck(company, versions);

                    return await Task.Run(() => status);
                }
            }

            return await Task.Run(() => new PendingVersionTruckDTO());
        }


        /// <summary>
        /// Gets the structure version truck status.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        private async Task<EstructuraVersionOutput> GetStructureVersionTruckStatus(int company, string version)
        {
            try
            {
                var ini = await _truckService.GetStructureVersionTruckInput(company, version);

                var data = await _apiTruck.GetStructureVersionStatusTruck(ini);

                return await Task.Run(() => data);

            }
            catch (Exception ex)
            {
                version = !string.IsNullOrEmpty(version) ? version : "Actual";

                throw new GenericException($"Problemas para obtener la version: {version} de Truck", ex);
            }
        }

        /// <summary>
        /// Gets the pending version truck.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        private async Task<PendingVersionTruckDTO> GetPendingVersionTruck(int company, EstructuraVersionOutput versions)
        {
            var ValeDto = new PendingVersionTruckDTO();
            var users = new List<string>
            {
                "AR3ROBOT",
                "TRKSOSSUR",
                ""
            };

            var codeTruck = new List<string>
            {
                "APR",
                "ING"
            };

            //version de Truck Operativa y no vigente por ahora
            var versionTruck = versions.EstructuraVersiones.Level1.FirstOrDefault(l => !users.Contains(l.ECUsuAlt)
                                                        && !users.Contains(l.ECUsuApr)
                                                        && codeTruck.Contains(l.ECStsCod)
                                                        // && !users.Contains(l.ECUsuMod) To Do: Ver si lo modifica un usuario de TRuck
                                                        && l.ECFecDes == versions.EstructuraVersiones.Level1.Max(x => x.ECFecDes)
                                                        && l.ECFecDes.ToDateOffsetUtc() > DateTimeOffset.UtcNow.Date); //TODO: Ojo Multipais

            // version portal aprovada por tueck
            if(versionTruck == null)
            {
                 versionTruck = versions.EstructuraVersiones.Level1.FirstOrDefault(l => users.Contains(l.ECUsuAlt)
                                             && !users.Contains(l.ECUsuApr)
                                             && codeTruck.Contains(l.ECStsCod)
                                             && l.ECFecDes == versions.EstructuraVersiones.Level1.Max(x => x.ECFecDes)
                                             && l.ECFecDes.ToDateOffsetUtc() < DateTimeOffset.UtcNow.Date); //TODO: Ojo Multipais
            }
            
            
            if (versionTruck == null)
            {
                versionTruck = versions.EstructuraVersiones.Level1.FirstOrDefault(l => !users.Contains(l.ECUsuAlt)
                                                     && !users.Contains(l.ECUsuApr)
                                                     && codeTruck.Contains(l.ECStsCod)
                                                       && l.ECFecDes == versions.EstructuraVersiones.Level1.Max(x => x.ECFecDes)
                                                       && l.ECFecDes.ToDateOffsetUtc() >= DateTimeOffset.UtcNow.Date); //TODO: Ojo Multipais

            }

            if (versionTruck != null && versionTruck.ECIndTra == "S")
            {
                ValeDto.IsTrasp = true;
                ValeDto.StructureEdit = true;
            }

            if (versionTruck != null && versionTruck.ECFecDes.ToDateOffsetUtc() > DateTimeOffset.UtcNow.Date)
            {
                ValeDto.LastVersionDate = !string.IsNullOrEmpty(versionTruck.ECFecDes) ? versionTruck.ECFecDes.ToDateOffset() : null; //TODO: Ojo Multipais
                ValeDto.VersionTruck = versionTruck.ECVerNro.ToString();
                ValeDto.StructureEdit = false;
                ValeDto.Message = $"Existe en Truck una versión: {ValeDto.VersionTruck} programada para fecha {ValeDto.LastVersionDate.Value:dd/MM/yyyy zzz} . La estructura no podrá ser editada en este momento. únicamente podrá visualizar a la fecha actual";

                if (versionTruck.ECIndTra == "S")
                    ValeDto.IsTrasp = true;

                return await Task.Run(() => ValeDto);
            }

 
            //Version del Portal Pendiente en truck
            var versionPortal = versions.EstructuraVersiones.Level1.FirstOrDefault(l => users.Contains(l.ECUsuAlt)
                                                  && users.Contains(l.ECUsuApr)
                                                  && codeTruck.Contains(l.ECStsCod)
                                                  && l.ECFecDes == versions.EstructuraVersiones.Level1.Max(x => x.ECFecDes)
                                                  && l.ECTipCre == ""
                                                  );

            if (versionPortal != null)
            {
                if(!string.IsNullOrEmpty(versionPortal.ECUsuMod) && !users.Contains(versionPortal.ECUsuMod))
                {
                    ValeDto.LastVersionDate = !string.IsNullOrEmpty(versionPortal.ECFecMod) ? versionPortal.ECFecMod.ToDateOffset() : null;
                    ValeDto.VersionTruck = versionPortal.ECVerNro.ToString();
                    ValeDto.StructureEdit = false;
                    ValeDto.Message = $"Truck modifico la versión del Portal: {ValeDto.VersionTruck} programada para fecha {ValeDto.LastVersionDate.Value:dd/MM/yyyy zzz} . La estructura no podrá ser editada en este momento. únicamente podrá visualizar a la fecha actual";
                    return await Task.Run(() => ValeDto);
                }

                ValeDto.LastVersionDate = !string.IsNullOrEmpty(versionPortal.ECFecDes) ? versionPortal.ECFecDes.ToDateOffset() : null;
                ValeDto.VersionTruck = versionPortal.ECVerNro.ToString();
                ValeDto.StructureEdit = true;
                ValeDto.TypePortal = true;
                ValeDto.Message = $"Existe en Truck una versión: {ValeDto.VersionTruck} del Portal programada para fecha {ValeDto.LastVersionDate.Value:dd/MM/yyyy zzz} . La version permanece pendiente de envio a truck ";

                if (versionPortal.ECIndTra == "S")
                {
                    ValeDto.IsTrasp = true;
                    ValeDto.Message = $"Existe en Truck una versión: {ValeDto.VersionTruck} del Portal operativa para fecha {ValeDto.LastVersionDate.Value:dd/MM/yyyy zzz}";
                }


            }
            else
            {
                // cuando es forzada la version desde truck
                var versionTruckForzada = versions.EstructuraVersiones.Level1.FirstOrDefault(l => !users.Contains(l.ECUsuAlt)
                                                  && !users.Contains(l.ECUsuApr)
                                                  && codeTruck.Contains(l.ECStsCod));

                if(versionTruckForzada != null && versionTruckForzada.ECFecDes == versionTruckForzada.ECFecMod && versionTruckForzada.ECFecDes == versionTruckForzada.ECFecApr && versionTruckForzada.ECIndTra == "S")
                {
                    ValeDto.IsTrasp = true;
                }
                
                //Forzada sin el dato ECFecMod
                if (!ValeDto.IsTrasp  && versionTruckForzada != null  && versionTruckForzada.ECFecDes == versionTruckForzada.ECFecApr && versionTruckForzada.ECIndTra == "S")
                {
                    ValeDto.IsTrasp = true;
                }


                ValeDto.LastVersionDate = DateTimeOffset.UtcNow.ToOffset().Date; //TODO: Ojo Multipaís
                ValeDto.StructureEdit = true;
                ValeDto.TypePortal = false;
            }


            return await Task.Run(() => ValeDto);
        }
    }
}
