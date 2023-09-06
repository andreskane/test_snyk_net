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
    public class LevelController : AbiControllerBase
    {
        private readonly ILogger<LevelController> _logger;
        private readonly IMediator _mediator;


        public LevelController(ILogger<LevelController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [Route("getAll")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<LevelDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync(bool? active)
        {
            var results = (
                active.HasValue
                    ? await _mediator.Send(new Q.Level.GetAllActiveOrderQuery { Active = active.Value })
                    : await _mediator.Send(new Q.Level.GetAllOrderQuery())
            );

            return new GenericResponse<IList<LevelDTO>>(results);
        }

        [Route("getAllForSelect")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<ItemDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllForSelectAsync(bool? active)
        {
            var results = (
                active.HasValue
                    ? await _mediator.Send(new Q.Level.GetAllActiveForSelectQuery { Active = active.Value })
                    : await _mediator.Send(new Q.Level.GetAllForSelectQuery())
            );

            return new GenericResponse<IList<ItemDTO>>(results);
        }

        [Route("getOne")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<LevelDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneAsync(int id)
        {
            var result = await _mediator.Send(new Q.Level.GetByIdQuery { Id = id });

            if (result != null)
                return new GenericResponse<LevelDTO>(result);

            throw new NotFoundException(ErrorMessageText.RecordNotExist);
        }

        [Route("AddItem")]
        [ProducesResponseType(typeof(GenericResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<BaseGenericResponse>> CreateDataAsync([FromBody] C.Level.AddCommand command) =>
            new GenericResponse<int>(await _mediator.Send(command));

        [Route("EditItem")]
        [HttpPut]
        public async Task<ActionResult<BaseGenericResponse>> UpdateDataAsync([FromBody] C.Level.UpdateCommand command) =>
            new GenericResponse<int>(await _mediator.Send(command));

        [Route("DeleteItem")]
        [HttpDelete]

        public async Task<ActionResult<GenericResponse>> DeleteDataAsync(int id)
        {
            await _mediator.Send(new C.Level.DeleteCommand(id));
            return new GenericResponse();
        }
    }
}
