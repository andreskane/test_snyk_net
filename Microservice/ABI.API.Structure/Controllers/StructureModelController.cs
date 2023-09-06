using ABI.API.Structure.Application.Commands.StructureModel;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Controllers.Base;
using ABI.API.Structure.Infrastructure.Extensions;
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
    public class StructureModelController : AbiControllerBase
    {
        private readonly ILogger<StructureModelController> _logger;
        private readonly IMediator _mediator;


        public StructureModelController(ILogger<StructureModelController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [Route("getAll")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureModelDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync(bool? active)
        {
            var results = (
                active.HasValue
                    ? await _mediator.Send(new Q.StructureModels.GetAllActiveOrderQuery { Active = active.Value })
                    : await _mediator.Send(new Q.StructureModels.GetAllOrderQuery())
            );

            return new GenericResponse<IList<StructureModelDTO>>(results);
        }

        [Route("getAllForSelect")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<ItemDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllForSelectAsync(bool? active)
        {
            var results = (
                active.HasValue
                    ? await _mediator.Send(new Q.StructureModels.GetAllActiveForSelectQuery { Active = active.Value })
                    : await _mediator.Send(new Q.StructureModels.GetAllForSelectQuery())
            );

            return new GenericResponse<IList<ItemDTO>>(results);
        }

        [Route("getOne")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<StructureModelDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneAsync(int id)
        {
            var result = await _mediator.Send(new Q.StructureModels.GetByIdQuery { Id = id });

            if (result != null)
                return new GenericResponse<StructureModelDTO>(result);

            throw new NotFoundException(ErrorMessageText.RecordNotExist);
        }

        [Route("AddItem")]
        [ProducesResponseType(typeof(GenericResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> CreateDataAsync([FromBody] C.StructureModel.AddCommand command)
        {
            if (ModelState.IsValid)
            {
                Func<Task<int?>> action = async () =>
                {
                    await _mediator.Send(new ValidateBaseStructureModelCommand
                    {
                        Code = command.Code,
                        Name = command.Name,
                        CountryId = command.CountryId
                    });

                    return await _mediator.Send(command);
                };

                var response = await action.CatchNodeValidation(StatusGenericResponse.OK,0);

                return ParseResponse(response);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Route("EditItem")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse>> UpdateDataAsync([FromBody] C.StructureModel.UpdateCommand command)
        {
            if (ModelState.IsValid)
            {
                Func<Task<int?>> action = async () =>
                {
                    await _mediator.Send(new ValidateBaseStructureModelCommand
                    {
                        Id = command.Id,
                        Code = command.Code,
                        Name = command.Name,
                        CountryId = command.CountryId
                    });

                    await _mediator.Send(command);
                    return null;
                };

                var response = await action.CatchNodeValidation(StatusGenericResponse.OK, 0);

                return ParseResponse(response);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Route("DeleteItem")]
        [HttpDelete]

        public async Task<ActionResult<GenericResponse>> DeleteDataAsync(int id)
        {
            await _mediator.Send(new C.StructureModel.DeleteCommand(id));
            return new GenericResponse();
        }

        [Route("getCountries")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<CountryDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetCountries()
        {
            var results = await _mediator.Send(new Q.Country.GetAllOrderQuery());

            return new GenericResponse<IList<CountryDTO>>(results);
        }
    }
}
