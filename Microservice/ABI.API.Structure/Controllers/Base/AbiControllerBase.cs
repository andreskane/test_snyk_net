using ABI.Framework.MS.Helpers.Message;
using ABI.Framework.MS.Helpers.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Controllers.Base
{
    [ExcludeFromCodeCoverage]
    //[Authorize]
    public class AbiControllerBase : ControllerBase
    {
        [OpenApiIgnore]
        public ObjectResult ApiMenssageError(ILogger _logger, string message)
        {
            var menssageError = new GenericMessage();
            menssageError.AddMessageError();
            _logger.LogError($"Code: {menssageError.GetCode()} {message}");
            return StatusCode(StatusCodes.Status500InternalServerError, menssageError);
        }

        [OpenApiIgnore]
        public ObjectResult ApiMenssageBadRequest(ILogger _logger, string message)
        {
            var menssageError = new GenericMessage();
            menssageError.AddMessage(message);
            _logger.LogError(message);
            return StatusCode(StatusCodes.Status400BadRequest, menssageError);
        }

        public ActionResult<GenericResponse> ParseResponse(GenericResponse response)
        {
            return response.StatusResponse.Code switch
            {
                (int)StatusGenericResponse.OK => Ok(),
                (int)StatusGenericResponse.Created => Created("", response),
                _ => BadRequest(response),
            };
        }
    }
}
