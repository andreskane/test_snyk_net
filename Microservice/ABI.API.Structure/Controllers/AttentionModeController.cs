using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Controllers.Base;
using ABI.Framework.MS.Helpers.Message;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttentionModeController : AbiControllerBase
    {
        private readonly ILogger<AttentionModeController> _logger;
        private readonly IMediator _mediator;


        public AttentionModeController(ILogger<AttentionModeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [Route("getAll")]
        [HttpGet()]
        [ProducesResponseType(typeof(GenericResponse<IList<AttentionModeDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync(bool? active)
        {
            var results = (
                active.HasValue
                    ? await _mediator.Send(new Q.AttentionMode.GetAllActiveOrderQuery { Active = active.Value })
                    : await _mediator.Send(new Q.AttentionMode.GetAllOrderQuery())
            );

            return new GenericResponse<IList<AttentionModeDTO>>(results);
        }

        [Route("getAllForSelect")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<ItemDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllForSelectAsync(bool? active)
        {
            var results = (
                active.HasValue
                    ? await _mediator.Send(new Q.AttentionMode.GetAllActiveForSelectQuery { Active = active.Value })
                    : await _mediator.Send(new Q.AttentionMode.GetAllForSelectQuery())
            );

            return new GenericResponse<IList<ItemDTO>>(results);
        }

        [Route("getOne")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<AttentionModeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneAsync(int id)
        {
            var result = await _mediator.Send(new Q.AttentionMode.GetByIdQuery { Id = id });

            if (result != null)
                return new GenericResponse<AttentionModeDTO>(result);

            throw new NotFoundException(ErrorMessageText.RecordNotExist);
        }

        [Route("AddItem")]
        [ProducesResponseType(typeof(GenericResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<BaseGenericResponse>> CreateDataAsync([FromBody] C.AttentionMode.AddCommand command) =>
            new GenericResponse<int>(await _mediator.Send(command));

        [Route("EditItem")]
        [HttpPut]
        public async Task<ActionResult<BaseGenericResponse>> UpdateDataAsync([FromBody] C.AttentionMode.UpdateCommand command) =>
            new GenericResponse<int>(await _mediator.Send(command));

        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteDataAsync(int id)
        {
            await _mediator.Send(new C.AttentionMode.DeleteCommand(id));
            return new GenericResponse();
        }
    }
}
