using ABI.API.Structure.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
   
    public class ApiVersionController : AbiControllerBase
    { 
        public ApiVersionController( )
        {
            
        }

         
        [HttpGet]
        [Route("/api/Version")]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var strVersion = await Task.Run(() => typeof(Startup).Assembly.GetName().Version.ToString());
            return Ok(strVersion);
        }
    }
}