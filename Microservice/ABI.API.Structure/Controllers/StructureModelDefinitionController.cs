using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Controllers.Base;
using ABI.Framework.MS.Helpers.Message;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using ABI.Framework.MS.Service.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using C = ABI.API.Structure.Application.Commands;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureModelDefinitionController : AbiControllerBase
    {
        private readonly ILogger<StructureModelDefinitionController> _logger;
        private readonly IMediator _mediator;


        public StructureModelDefinitionController(ILogger<StructureModelDefinitionController> logger, IMediator mediator)
        {

            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [Route("getAll")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureModelDefinitionDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync()
        {
            var results = await _mediator.Send(new Q.StructureModelDefinition.GetAllOrderQuery());
            return new GenericResponse<IList<StructureModelDefinitionDTO>>(results);
        }

        [Route("getAllByStructureModel")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureModelDefinitionDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllByStructureModelAsync(int id)
        {
            var result = await _mediator.Send(new Q.StructureModelDefinition.GetAllByStructureModelQuery { Id = id });

            if (result != null)
                return new GenericResponse<IList<StructureModelDefinitionDTO>>(result);

            throw new NotFoundException(ErrorMessageText.RecordNotExist);
        }

        [Route("getOne")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<StructureModelDefinitionDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneAsync(int id)
        {
            var result = await _mediator.Send(new Q.StructureModelDefinition.GetByIdQuery { Id = id });

            if (result != null)
                return new GenericResponse<StructureModelDefinitionDTO>(result);

            throw new NotFoundException(ErrorMessageText.RecordNotExist);
        }

        [Route("AddItem")]
        [ProducesResponseType(typeof(GenericResponse<StatusGenericResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> CreateDataAsync([FromBody] C.StructureModelDefinition.AddCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _mediator.Send(command);

                    return Created("", new GenericResponse(StatusGenericResponse.Created));
                }
                catch (ElementExistsException e)
                {
                    var errors = new Dictionary<string, string[]>
                    {
                        { "LevelId", new string[] { e.Message } }
                    };

                    return BadRequest(new GenericResponse(StatusGenericResponse.BadRequest, errors));
                }
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Route("EditItem")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse>> UpdateDataAsync([FromBody] C.StructureModelDefinition.UpdateCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(command);
                    return new GenericResponse();
                }
                catch (ElementExistsException e)
                {
                    var errors = new Dictionary<string, string[]>
                    {
                        { "LevelId", new string[] { e.Message } }
                    };

                    return BadRequest(new GenericResponse(StatusGenericResponse.BadRequest, errors));
                }
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteDataAsync(int id)
        {
            await _mediator.Send(new C.StructureModelDefinition.DeleteCommand(id));
            return new GenericResponse();
        }
    }
}
