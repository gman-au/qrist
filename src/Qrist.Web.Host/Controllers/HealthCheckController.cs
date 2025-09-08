using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qrist.Interfaces;

namespace Qrist.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController(IHealthChecker healthChecker) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> Get() =>
            await
                healthChecker
                    .CheckHealthAsync();
    }
}