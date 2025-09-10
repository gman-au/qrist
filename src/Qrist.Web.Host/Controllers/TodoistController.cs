using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Requests;

namespace Qrist.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoistController : ControllerBase
    {
        private readonly ILogger<TodoistController> _logger;
        private readonly ITodoistCardHandler _todoistCardHandler;

        public TodoistController(ILogger<TodoistController> logger, ITodoistCardHandler todoistCardHandler)
        {
            _logger = logger;
            _todoistCardHandler = todoistCardHandler;
        }

        [HttpPost("process")]
        public async Task<ActionResult> Process([FromBody] TodoistRequest request)
        {
            _logger
                .LogInformation("Received request");

            var response =
                await
                    _todoistCardHandler
                        .ProcessAsync(request);

            var jsonString = JsonSerializer.Serialize(response);

            _logger
                .LogInformation($"Response: {jsonString}");

            return
                new OkObjectResult(response);
        }
    }
}