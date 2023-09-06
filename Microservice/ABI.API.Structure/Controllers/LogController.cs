using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace ABI.API.Structure.Controllers
{

    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LogController(ILogger<LogController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {

                var url = $"logs//log{DateTime.Now:yyyyMMdd}.json";

                string texto = "";

                using (var fileStream = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fileStream, leaveOpen: true))
                    {
                        texto = await sr.ReadToEndAsync();
                    }
                }

                return Ok(texto); // returns a FileStreamResult
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
