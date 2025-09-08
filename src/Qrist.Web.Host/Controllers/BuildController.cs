using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qrist.Domain;
using Qrist.Interfaces;

namespace Qrist.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildController(IQristApplication qristApplication) : ControllerBase
    {
        [HttpPost("code")]
        public async Task<ActionResult<string>> BuildCode([FromBody] QrCodeRequest request)
        {
            var cancellationToken = CancellationToken.None;

            return
                await
                    qristApplication
                        .ProduceQrCodeAsync(request, cancellationToken);
        }

        [HttpPost("url")]
        public async Task<ActionResult<string>> BuildUrl([FromBody] QrCodeRequest request)
        {
            var cancellationToken = CancellationToken.None;

            return
                await
                    qristApplication
                        .ProduceFullRequestUrlAsync(request, cancellationToken);
        }
    }
}