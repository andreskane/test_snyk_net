using ABI.API.Structure.ACL.Truck;
using ABI.API.Structure.ACL.Truck.Application.Commands;
using ABI.API.Structure.ACL.Truck.Application.DTO;
using ABI.API.Structure.Controllers.Base;
using ABI.Framework.MS.Helpers.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationTruckController : AbiControllerBase
    {
        private readonly ILogger<IntegrationTruckController> _logger;
        private readonly IStructureAdapter _structureAdapter;
        private readonly IMediator _mediator;


        public IntegrationTruckController(IStructureAdapter structureAdapter,
                                            ILogger<IntegrationTruckController> logger,
                                             IMediator mediator)
        {

            _logger = logger;
            _structureAdapter = structureAdapter ?? throw new ArgumentNullException(nameof(structureAdapter));
            _mediator = mediator;
        }


        /// <summary>
        /// Integrates the truck to portal.
        /// </summary>
        /// <param name="JsonText">The json text.</param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [Route("AddStructureTruckToPortal")]
        [ProducesResponseType(typeof(GenericResponse<ProcessDTO>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<BaseGenericResponse> IntegrateTruckToPortal(string code)
        {
            var result = await _structureAdapter.StructureTruckToStructurePortal(code);
            return new GenericResponse<ProcessDTO>(result);
        }

        /// <summary>
        /// Integrates the truck to portal.
        /// </summary>
        /// <param name="JsonText">The json text.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AddStructureTruckToPortalCompare")]
        [ProducesResponseType(typeof(GenericResponse<ProcessDTO>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<BaseGenericResponse> IntegrateTruckToPortalCompare(string code)
        {
            var result = await _structureAdapter.StructureTruckToStructurePortalCompare(code);
            return new GenericResponse<ProcessDTO>(result);
        }

        /// <summary>
        /// Integrates the truck to portal.
        /// </summary>
        /// <param name="JsonText">The json text.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AddStructureTruckToPortalJson")]
        [ProducesResponseType(typeof(GenericResponse<ProcessDTO>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<BaseGenericResponse> IntegrateTruckToPortalJson()
        {
            var result = await _structureAdapter.StructureTruckToStructurePortalJson(null);
            return new GenericResponse<ProcessDTO>(result);
        }

        /// <summary>
        /// Integrates the truck to portal.
        /// </summary>
        /// <param name="JsonText">The json text.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("AddStructureTruckToPortalCompareJson")]
        [ProducesResponseType(typeof(GenericResponse<ProcessDTO>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<BaseGenericResponse> IntegrateTruckToPortalCompareJson()
        {
            var result = await _structureAdapter.StructureTruckToStructurePortalCompareJson();
            return new GenericResponse<ProcessDTO>(result);
        }

        [AllowAnonymous]
        [Route("MigrationClientsTruckToPortal")]
        [ProducesResponseType(typeof(GenericResponse<ProcessDTO>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<BaseGenericResponse> IntegrateTruckToPortalCompareJson(string EmpId)
        {
            var result = await _structureAdapter.MigrationClientsTruckToPortal(EmpId);
            return new GenericResponse<ProcessDTO>(result);
        }

        /// <summary>
        /// Integrates the truck to portal.
        /// </summary>
        /// <param name="JsonText">The json text.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("StructurePortalToTruckChanges")]
        [ProducesResponseType(typeof(GenericResponse<ProcessDTO>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<BaseGenericResponse> IntegrateStructurePortalToTruckChanges([FromBody] TruckChangesDTO truck)
        {
            var result = await _structureAdapter.StructurePortalToTruckChanges(truck.StructureId, truck.DateChanges);
            return new GenericResponse<ProcessDTO>(result);
        }

        [AllowAnonymous]
        [Route("getStructureVersionTruckStatus")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<BaseGenericResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> StructureVersionTruckStatus(int structureId, int? version)
        {
            var result = await _mediator.Send(new StructureVersionTruckStatusCommand { StructureId = structureId, VersionTruck = version });

            if (result != null)
            {
                return new GenericResponse<PendingVersionTruckDTO>(result);
            }

            return new GenericResponse<PendingVersionTruckDTO>(new PendingVersionTruckDTO());
        }

        [AllowAnonymous]
        [Route("ProcessVersionedTruck")]
        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<BaseGenericResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> ProcessVersionedTruck()
        {
            await _mediator.Send(new ProcessVersionedCommand());
            return new GenericResponse<bool>(true);
        }
    }
}