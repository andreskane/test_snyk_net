using ABI.API.Structure.Application.Commands.RequestsTray;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.Extensions;
using ABI.API.Structure.Application.Queries.RequestsTray;
using ABI.API.Structure.Controllers.Base;
using ABI.Framework.MS.Helpers.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers
{
    //   [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsTrayController : AbiControllerBase
    {
        private readonly IMediator _mediator;


        public RequestsTrayController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("getAllPaginatedSearch")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<PaginatedList<RequestTrayDTO>>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetAllPaginatedSearch([FromQuery] PaginatedSearchDTO model)
        {
            var result = await _mediator.Send(
                new Q.RequestsTray.GetAllPaginatedSearchQuery() 
                { 
                    model=model
                }
            );

            return new GenericResponse<PaginatedList<RequestTrayDTO>>(result);
        }

        [Route("getPaginatedSearchByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<PaginatedList<RequestTrayDTO>>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetPaginatedSearchByStructure(int structureId, DateTimeOffset validityFrom, DateTimeOffset validityTo, [FromQuery] PaginatedSearchDTO model)
        {
            var result = await _mediator.Send(
                new Q.RequestsTray.GetPaginatedSearchByStructureQuery()
                {
                    model = model,
                    structureId = structureId,
                    validityFrom = validityFrom,
                    validityTo = validityTo
                }
            );

            return new GenericResponse<PaginatedList<RequestTrayDTO>>(result);
        }

        [Route("getOneWithDetail")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<RequestTrayDetailDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneWithDetail(int structureId, Guid userId, DateTimeOffset validity)
        {
            var result = await _mediator.Send(new Q.RequestsTray.GetOneWithDetailQuery() { 
                structureId = structureId,
                userId = userId,
                validity = validity
            });

            return new GenericResponse<IList<RequestTrayDetailDTO>>(result);
        }


        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<RequestTrayFiltersDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GenericResponse<RequestTrayFiltersDTO>>> GetFiltersOptions([FromQuery] GetFiltersOptionsQuery filter) =>
           new GenericResponse<RequestTrayFiltersDTO>(await _mediator.Send(filter));


        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GenericResponse<PaginatedList<RequestTrayDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GenericResponse<PaginatedList<RequestTrayDTO>>>> GetPaginatedSearchByParametres([FromQuery] GetPaginatedSearchByParametersQuery filter) =>
   new GenericResponse<PaginatedList<RequestTrayDTO>>(await _mediator.Send(filter));

        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteDataAsync([FromQuery] DeleteChangeCommand command)
        {
            await _mediator.Send(command);
            return new GenericResponse();
        }

        [Route("DeleteGroupItem")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteGroupItemAsync([FromQuery] DeleteChangeGroupCommand command)
        {
            await _mediator.Send(command);
            return new GenericResponse();
        }
    }
}
