using ABI.API.Structure.Application.Commands.Entities;
using ABI.API.Structure.Application.Commands.Structures;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.Structures;
using ABI.API.Structure.Controllers.Base;
using ABI.API.Structure.Infrastructure.Extensions;
using ABI.Framework.MS.Domain.Exceptions;
using ABI.Framework.MS.Helpers.Message;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using ABI.Framework.MS.Service.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class StructureController : AbiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StructureController> _logger;

        public StructureController(IMediator mediator, ILogger<StructureController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        #region Structure Model V1

        [Route("getAll")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync(string? country)
        {
            var results = await _mediator.Send(new Q.Structures.GetAllOrderQuery { Country = country });
            return new GenericResponse<IList<StructureDTO>>(results);
        }
        [Route("getOne")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<StructureDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneAsync(int id)
        {
            var result = await _mediator.Send(new Q.Structures.GetByIdQuery { Id = id });

            if (result != null)
                return new GenericResponse<StructureDTO>(result);

            throw new NotFoundException(ErrorMessageText.RecordNotExist);
        }

        [Authorize(Roles = "Admin")]
        [Route("AddItem")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GenericResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> CreateDataAsync([FromBody] CreateStructureCommand command)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var result = await _mediator.Send(command);
                    return Created("", new GenericResponse<int>(result, StatusGenericResponse.Created));
                }
                catch (NameExistsException NEx)
                {

                    var errors = new Dictionary<string, string[]>
                    {
                        { "Name", new string[] { NEx.Message } }
                    };

                    return BadRequest(new GenericResponse(StatusGenericResponse.BadRequest, errors));
                }
                catch (StructureCodeExistsException SCEx)
                {

                    var errors = new Dictionary<string, string[]>
                    {
                        { "Code", new string[] { SCEx.Message } }
                    };

                    return BadRequest(new GenericResponse(StatusGenericResponse.BadRequest, errors));
                }


            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Authorize(Roles = "Admin")]
        [Route("EditItem")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse>> UpdateDataAsync([FromBody] EditStructureCommand command)
        {
            if (ModelState.IsValid)
            {
                Func<Task<StructureDTO?>> action = async () =>
                {
                    await _mediator.Send(new ValidateBaseStructureCommand
                    {
                        Id = command.Id,
                        Code = command.Code,
                        Name = command.Name,
                    });

                    await _mediator.Send(command);
                    return null;
                };

                var response = await action.CatchStructureValidation(StatusGenericResponse.OK);

                return ParseResponse(response);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteDataAsync(int id)
        {
            await _mediator.Send(new DeleteStructureCommand(id));
            return new GenericResponse();
        }

        [Authorize(Roles = "Admin")]
        [Route("ValidateValidity")]
        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<ValidateDateStructure>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<object> ValidateValidity([FromBody] ValidateValidityCommand validate)
        {
            try
            {
                var result = await _mediator.Send(validate);
                return new GenericResponse<ValidateDateStructure>(result);
            }
            catch (DateGreaterThanTodayException e)
            {
                var errors = new Dictionary<string, string[]>
                    {
                        { "Validity", new string[] { e.Message } }
                    };

                return BadRequest(new GenericResponse(StatusGenericResponse.BadRequest, errors));
            }
        }

        [Route("getMostVisitedFilters")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<MostVisitedFilterDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetMostVisitedFiltersAsync([FromQuery] GetMostVisitedFiltersQuery command) =>
            new GenericResponse<IList<MostVisitedFilterDto>>(await _mediator.Send(command));

        [Route("saveMostVisitedFilters")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse>> SaveMostVisitedFilters([FromBody] SaveMostVisitedFiltersCommand command)
        {
            await _mediator.Send(command);
            return new GenericResponse();
        }

        #endregion

        #region Structure V2
        [Route("GetMasters")]
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(GenericResponse<MasterDTO>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetMasters()
        {
            var results = await _mediator.Send(new Q.Structures.GetMastersQuery());
            return new GenericResponse<MasterDTO>(results);
        }

        [Authorize(Roles = "Admin")]
        [Route("AddItem")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(GenericResponse<StructureDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> CreateDataV2Async([FromBody] CreateStructureCommandV2 command)
        {
            if (ModelState.IsValid)
            {
                Func<Task<StructureDTO?>> action = async () =>
                {
                    await _mediator.Send(new ValidateBaseStructureCommand
                    {
                        Code = command.Code,
                        Name = command.Name
                    });

                    return await _mediator.Send(command);
                };

                var response = await action.CatchStructureValidation(StatusGenericResponse.OK);

                return ParseResponse(response);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }
        #endregion
    }
}
