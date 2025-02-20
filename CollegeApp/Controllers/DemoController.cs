using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOnlyGoogle")]
    //[Authorize(AuthenticationSchemes = "JWTforGoogle", Roles ="Admin")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [DisableCors] // It will disable the CORS for this method
        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message from Information method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Error method");
            _logger.LogCritical("Log message from Critical method");

            return Ok();
        }
    }
}
