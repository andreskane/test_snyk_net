using ABI.API.Structure.Application.Exceptions;
using ABI.Framework.MS.Helpers.Message;
using ABI.Framework.MS.Helpers.Response;
using ABI.Framework.MS.Net.RestClient;
using ABI.Framework.MS.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ABI.API.Structure.Infrastructure.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(CorruptedFileException), HandleCorruptedFileException },
                { typeof(ValidationException), HandleValidationException },
                { typeof(NameExistsException), HandleValidationNameException },
                { typeof(ChangeTrackingDateException), HandleBadRequestListException },
                { typeof(ConfirmException), HandleConfirmException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var menssageError = new GenericMessage();
            menssageError.AddMessageError();
            _logger.LogError($"Code: {menssageError.GetCode()} {context.Exception.Message}");

            context.Result = new ObjectResult(menssageError)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;
            var details = new GenericResponse(StatusGenericResponse.BadRequest, exception.Errors);

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }
        private void HandleValidationNameException(ExceptionContext context)
        {
            var exception = context.Exception as NameExistsException;
            var errors = new Dictionary<string, string[]>
            {
                { "Name", new string[] { exception.Message } }
            };
            var details = new GenericResponse(StatusGenericResponse.BadRequest, errors);

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleCorruptedFileException(ExceptionContext context) =>
            HandleException(context, StatusGenericResponse.BadRequest, x => new ObjectResult(x) { StatusCode = 460 });

        private void HandleBadRequestException(ExceptionContext context)
        {
            var exception = context.Exception as BadRequestException;
            var details = new GenericResponse(StatusGenericResponse.BadRequest);
            details.Messages.Add(context.Exception.Message);
            details.StatusResponse.Code = exception.Code ?? 400;

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleBadRequestListException(ExceptionContext context)
        {
            var exception = context.Exception as GenericListException;
            var details = new GenericResponse(StatusGenericResponse.BadRequest);
            details.Messages.Add(context.Exception.Message);
            details.StatusResponse.Code = exception.Code == 0? 407:exception.Code; 
            details.Result = exception.listObject;

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context) =>
            HandleException(context, StatusGenericResponse.NotFound, x => new NotFoundObjectResult(x));

        private void HandleException(ExceptionContext context, StatusGenericResponse status, Func<object, IActionResult> actionResult)
        {
            var details = new GenericResponse(status);
            details.Messages.Add(context.Exception.Message);

            context.Result = actionResult.Invoke(details);
            context.ExceptionHandled = true;
        }

        private void HandleConfirmException(ExceptionContext obj)
        {
            var exception = obj.Exception as ConfirmException;
            var details = new GenericResponse();
            details.StatusResponse = new StatusResponse();
            details.StatusResponse.Code = 210;
            details.Messages.Add(exception.mensaje);

            obj.Result = new ObjectResult(details) { StatusCode = 210 };
            obj.ExceptionHandled = true;
        }
    }
}
