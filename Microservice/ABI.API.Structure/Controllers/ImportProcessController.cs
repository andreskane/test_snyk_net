using ABI.API.Structure.ACL.Truck.Application.Commands.ImportProcess;
using ABI.API.Structure.Application.Commands.ImportProcess;
using ABI.API.Structure.Controllers.Base;
using ABI.Framework.MS.Helpers.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ImportProcessController : AbiControllerBase
    {

        private readonly ILogger<ImportProcessController> _logger;
        private readonly IMediator _mediator;
        public ImportProcessController(ILogger<ImportProcessController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

 
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateCommand command) =>
            Created("", new GenericResponse() { Result = await _mediator.Send(command) });

       

        [AllowAnonymous]
        [HttpPost]
        [Route("NotifyEnd")]
        [ProducesResponseType(typeof(GenericResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<GenericResponse>> NotifyEnd(IOLoadFinishedCommand command) =>
            new GenericResponse() { Result = await _mediator.Send(command) };

       
    }
}
