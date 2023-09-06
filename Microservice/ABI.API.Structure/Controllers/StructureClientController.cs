using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using ABI.API.Structure.ACL.Truck.Application.Queries;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Queries.StructureClient;
using ABI.API.Structure.Controllers.Base;
using ABI.API.Structure.Helper;
using ABI.Framework.MS.Helpers.Response;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ABI.API.Structure.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StructureClientController : AbiControllerBase
    {
        private readonly ILogger<StructureClientController> _logger;
        private readonly IMediator _mediator;


        public StructureClientController(ILogger<StructureClientController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [Route("getOneClienteByTerrytoryNodeId")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureClientDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync(int nodeId, DateTimeOffset validityFrom)
        {
            var results = await _mediator.Send(new GetOneNodeClientQuery {NodeId = nodeId, ValidityFrom = validityFrom });

            return new GenericResponse<IList<StructureClientDTO>>(results);
        }

        [Route("getAllClients")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureClientDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllClientsAsync(string structureCode, DateTimeOffset validity)
        {
            var results = await _mediator.Send(new GetAllClientQuery { StructureCode = structureCode, ValidityFrom = validity });

            return new GenericResponse<IList<StructureClientDTO>>(results);
        }



        [Route("getActualRelation")]
        [HttpGet]
        [AllowAnonymous()]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureClientDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GenericResponse>> GetAllActualAsync([FromQuery] GetLastDataIOQuery request = null)
        {
            try
            {
                var ReturnMenssage = new GenericResponse();
                ReturnMenssage.StatusResponse.Code = 200;
                if (request.Country == "" || request.Country == null)
                {
                    request = new GetLastDataIOQuery();
                    request.Country = "01AR";
                    ReturnMenssage.Messages.Add(String.Format("{0}:{1}", "No se envio el parametro de Country(business), se toma por defecto", request.Country));
                }
                try
                {
                    request.Country = request.Country.ToUpper();
                    var values = await _mediator.Send(new GetLastDataIOQuery { Country = request.Country });
                    ReturnMenssage.Result = values;
                    return Ok(ReturnMenssage);
                }
                catch (Exception ex)
                {
                    return ApiMenssageError(_logger, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return ApiMenssageError(_logger, ex.Message);
            }
        }

        [Route("getClients")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<StructureQuantityAndClientDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetClientsAsync([FromQuery] GetClientsByNodeQuery command) =>
            new GenericResponse<StructureQuantityAndClientDto>(await _mediator.Send(command));

        [Route("getExcelClients")]
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetExcelClientsAsync([FromQuery] GetClientsExcelDataQuery command)
        {
            var dataExcel = await _mediator.Send(command);
            FileStreamResult fr = new FileStreamResult(dataExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            fr.FileDownloadName = "Template.xlsx";
            return fr;
        }
    }
}
