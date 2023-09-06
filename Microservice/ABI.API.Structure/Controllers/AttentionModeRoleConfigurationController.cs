using ABI.API.Structure.Application.DTO;
using ABI.API.Structure.Controllers.Base;
using ABI.Framework.MS.Helpers.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Q = ABI.API.Structure.Application.Queries;

namespace ABI.API.Structure.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttentionModeRoleConfigurationController : AbiControllerBase
    {
        private readonly ILogger<AttentionModeRoleConfigurationController> _logger;
        private readonly IMediator _mediator;


        public AttentionModeRoleConfigurationController(ILogger<AttentionModeRoleConfigurationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [Route("getAll")]
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IList<AttentionModeRoleConfigurationDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BaseGenericResponse> GetAllAsync()
        {
            var results = await _mediator.Send(new Q.AttentionModeRole.GetAllConfigurationQuery());
            return new GenericResponse<IList<AttentionModeRoleConfigurationDTO>>(results);
        }
    }
}
