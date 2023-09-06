using ABI.API.Structure.Application.Commands.Entities;
using ABI.API.Structure.Application.Commands.StructureNodes;
using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Application.DTO.Interfaces;
using ABI.API.Structure.Application.Exceptions;
using ABI.API.Structure.Application.Queries.Structure;
using ABI.API.Structure.Controllers.Base;
using ABI.API.Structure.Infrastructure.Extensions;
using ABI.Framework.MS.Helpers.Message;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class StructureNodeController : AbiControllerBase
    {
        private readonly IMediator _mediator;


        public StructureNodeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Structure Model

        [Route("getOneByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<NodeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneByStructureAsync(int id, int structureId, DateTimeOffset validity)
        {
            try
            {
                var result = await _mediator.Send(new Q.StructureNodes.GetOneNodeByIdQuery { StructureId = structureId, NodeId = id, ValidityFrom = validity });
                return new GenericResponse<NodeDTO>(result);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException(ErrorMessageText.RecordNotExist);
            }
        }

        [Route("getOneVersionByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<NodeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetOneVersionByStructureAsync(int id, int structureId, DateTimeOffset validity)
        {
            try
            {
                var result = await _mediator.Send(new Q.StructureNodes.GetOneNodeVersionByIdQuery { StructureId = structureId, NodeId = id, ValidityFrom = validity });
                return new GenericResponse<NodeDTO>(result);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException(ErrorMessageText.RecordNotExist);
            }
        }

        [Route("getAllByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<StructureDomainDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllByStructureIdAsync(int id, bool? active)
        {
            try
            {
                //HACER: Ojo con el UtcNow
                var result = await _mediator.Send(new Q.StructureNodes.GetAllStructureQuery { StructureId = id, Active = active, ValidityFrom = DateTimeOffset.UtcNow.Date });
                return new GenericResponse<StructureDomainDTO>(result);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException(ErrorMessageText.RecordNotExist);
            }

        }

        [Route("getScheduledDatesByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<DateTimeOffset>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetScheduledDatesByStructureAsync(int id)
        {
            try
            {
                var result = await _mediator.Send(new Q.StructureNodes.GetAllScheduledChangesQuery { Id = id });
                return new GenericResponse<IList<DateTimeOffset>>(result);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException(ErrorMessageText.RecordNotExist);
            }
        }

        [Route("getAllByScheduledStructure")]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(GenericResponse<StructureDomainDTO>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetAllByScheduledStructureAsync(int? id, string? code, DateTimeOffset validity, bool? active)
        {
            if (!id.HasValue && string.IsNullOrEmpty(code))
            {
                return ErrorStructureParametersBadRequest();
            }

            if (!string.IsNullOrEmpty(code))
            {
                var structure = await _mediator.Send(new Q.Structure.GetAllByCodeQuery { Code = code });

                if (structure != null)
                {
                    id = structure.Id;
                }
            }

            var results = await _mediator.Send(new Q.StructureNodes.GetAllByScheduledStructureQuery
            {
                Id = id.Value,
                ValidityFrom = validity,
                Active = active
            });

            return new GenericResponse<StructureDomainDTO>(results);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("getCodeHint")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetCodeHint(int structureId, int levelId)
        {
            var result = await _mediator.Send(new Q.StructureNodes.GetCodeHintQuery
            {
                StructureId = structureId,
                LevelId = levelId
            });

            return new GenericResponse<string>(result);
        }

        [Route("getEmployeeIdsNodesByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetEmployeeIdsNodesByStructure(int structureId, DateTimeOffset validity)
        {

            var result = await _mediator.Send(new Q.StructureNodes.GetEmployeeIdsNodesByStructureQuery
            {
                StructureId = structureId,
                ValidityFrom = validity
            });

            return new GenericResponse<List<EmployeeIdNodesDTO>>(result);
        }

        [Route("getRepeatedEmployeeIdsNodesByStructure")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetRepeatedEmployeeIdsNodesByStructure(int structureId, DateTimeOffset validity)
        {

            var result = await _mediator.Send(new Q.StructureNodes.GetRepeatedEmployeeIdsNodesByStructureQuery
            {
                StructureId = structureId,
                ValidityFrom = validity
            });

            return new GenericResponse<List<EmployeeIdNodesDTO>>(result);
        }

        [Route("getBoundedStructureFromNodeId")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IStructureBranchDTO>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetBoundedStructureFromNodeId(int structureId, string societyCode, int fromNodeId, int toLevelId, DateTimeOffset validity)
        {
            var results = await _mediator.Send(new Q.StructureNodes.GetBoundedStructureFromNodeIdQuery
            {
                StructureId = structureId,
                SocietyCode = societyCode,
                FromNodeId = fromNodeId,
                ToLevelId = toLevelId,
                ValidityFrom = validity
            });

            return new GenericResponse<IStructureBranchDTO>(results);
        }

        [Route("getStructureBranchByNodeId")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetStructureBranchByNodeId(int structureId, int nodeId, DateTimeOffset validity)
        {

            var result = await _mediator.Send(new GetAllBranchByNodeIdQuery
            {
                StructureId = structureId,
                NodeId = nodeId,
                ValidityFrom = validity
            }); 

            return new GenericResponse<IStructureBranchDTO>(result);
        }

        [Authorize(Roles = "Admin")]
        [Route("AddItem")]
        [ProducesResponseType(typeof(GenericResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<ActionResult<GenericResponse>> CreateDataAsync([FromBody] CreateNodeCommand command)
        {
            if (ModelState.IsValid)
            {
                Func<Task<int?>> action = async () =>
                {
                    await _mediator.Send(new ValidateTruckIllegalCharacterCommand
                    {
                        StructureId = command.StructureId,
                        Name = command.Name
                    });

                    await _mediator.Send(new ValidateNodeCodeCommand
                    {
                        StructureId = command.StructureId,
                        LevelId = command.LevelId,
                        Code = command.Code
                    });

                    await _mediator.Send(new ValidateNodeCodeLengthCommand
                    {
                        StructureId = command.StructureId,
                        LevelId = command.LevelId,
                        Code = command.Code
                    });

                    return await _mediator.Send(command);
                };

                var response = await action.CatchNodeValidation(StatusGenericResponse.Created, command.LevelId);

                return ParseResponse(response);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("EditItem")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse>> UpdateDataAsync([FromBody] EditNodeCommand command)
        {
            if (ModelState.IsValid)
            {
                Func<Task<int?>> action = async () =>
                {
                    await _mediator.Send(new ValidateTruckIllegalCharacterCommand
                    {
                        StructureId = command.StructureId,
                        Name = command.Name
                    });

                    await _mediator.Send(new ValidateNodeCodeCommand
                    {
                        StructureId = command.StructureId,
                        LevelId = command.LevelId,
                        Code = command.Code,
                        NodeId = command.Id
                    });

                    await _mediator.Send(new ValidateNodeCodeLengthCommand
                    {
                        StructureId = command.StructureId,
                        LevelId = command.LevelId,
                        Code = command.Code
                    });

                    await _mediator.Send(command);
                            
                    return null;
                };

                var response = await action.CatchNodeValidation(StatusGenericResponse.OK, command.LevelId);

                return ParseResponse(response);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteItem")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteDataAsync([FromBody] DeleteNodeCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return new GenericResponse();
            }
            catch (ContainsChildNodesException e)
            {
                var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
                invalidData.AddMessage(e.Message);
                return BadRequest(invalidData);
            }
        }

        [Route("DeleteNodeDraft")]
        [HttpDelete]
        public async Task<ActionResult<BaseGenericResponse>> DeleteNodeDraftDataAsync([FromBody] DeleteNodeDraftCommand command) =>
            new GenericResponse<int>(await _mediator.Send(command));

        [Authorize(Roles = "Admin,User")]
        [Route("ChangesWithoutSaving")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<StructureChangesWithoutSavingDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetChangesWithoutSavingByStructureIdAsync(int structureId)
        {
            var value = await _mediator.Send(new Q.StructureNodes.GetStructureChangesWithoutSavingThereIsQuery
            {
                StructureId = structureId
            });

            var result = new StructureChangesWithoutSavingDTO
            {
                ChangesWithoutSaving = value
            };

            return new GenericResponse<StructureChangesWithoutSavingDTO>(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("validateChanges")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<ValidateStructure>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<object> ValidateChangesAsync(int id, DateTimeOffset validity)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new ValidateStructureCommand(id, validity));
                return new GenericResponse<ValidateStructure>(result);
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("getChangesWithoutSaving")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<StructureNodeChangesDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetChangesWithoutSavingAsync(int id, DateTimeOffset validity)
        {

            var result = await _mediator.Send(new Q.StructureNodes.GetStructureChangesWithoutSavingQuery
            {
                StructureId = id,
                ValidityFrom = validity
            });

            return new GenericResponse<IList<StructureNodeChangesDTO>>(result);

        }

        [Authorize(Roles = "Admin,User")]
        [Route("saveChangesWithoutSaving")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse>> SaveChangesWithoutSavingAsync([FromBody] SaveChangesWithoutSavingCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return new GenericResponse();
            }

            var invalidData = new GenericResponse(StatusGenericResponse.BadRequest);
            invalidData.AddMessage(ErrorMessageText.InvalidData);

            return BadRequest(invalidData);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("deleteChangesWithoutSaving")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse>> DeleteChangesWithoutSavingAsync(int id, DateTimeOffset validity)
        {
            await _mediator.Send(new DeleteNodesChangesWithoutSavingCommand(id, validity));
            return new GenericResponse();
        }

        [Authorize(Roles = "Admin,User")]
        [Route("getAllSameLevelNodesByNode")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<ItemNodeDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllSameLevelNodesByNode(int nodeId, DateTimeOffset validity)
        {

            var results = await _mediator.Send(new Q.StructureNodes.GetAllSameLevelNodesByNode
            {
                NodeId = nodeId,
                ValidityFrom = validity
            });

            return new GenericResponse<IList<ItemNodeDTO>>(results);

        }

        #endregion

        #region Structure NODE V2
        [Route("getAllByScheduledStructure")]
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(GenericResponse<StructureDomainV2DTO>), (int)HttpStatusCode.OK)]
        public async Task<BaseGenericResponse> GetAllByScheduledStructureV2Async(int? id, string? code, DateTimeOffset validity, bool? active)
        {
            if (!id.HasValue && string.IsNullOrEmpty(code))
            {
                return ErrorStructureParametersBadRequest();
            }

            if (!string.IsNullOrEmpty(code))
            {
                var structure = await _mediator.Send(new Q.Structure.GetAllByCodeQuery { Code = code });

                if (structure != null)
                {
                    id = structure.Id;
                }
            }

            var results = await _mediator.Send(new Q.StructureNodes.GetAllByScheduledStructureV2Query
            {
                Id = id.Value,
                ValidityFrom = validity,
                Active = active
            });

            return new GenericResponse<StructureDomainV2DTO>(results);
        }

        /// <summary>
        /// Errors the structure bad request.
        /// </summary>
        /// <returns></returns>
        private static BaseGenericResponse ErrorStructureParametersBadRequest()
        {
            var errors = new Dictionary<string, string[]> {
                    { "id", new string [] { "Id o código obligatorio" } },
                    { "code", new string [] { "Código o id obligatorio" } }
                };

            return new GenericResponse(StatusGenericResponse.BadRequest, errors);
        }
        #endregion
    }
}
