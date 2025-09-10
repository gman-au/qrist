using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Qrist.Adapters.Todoist.UiExtensions;
using Qrist.Domain.Todoist.UiExtensions.Requests;
using Qrist.Infrastructure.Web;

namespace Qrist.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoistController : ControllerBase
    {
        private readonly ILogger<TodoistController> _logger;
        private readonly ITodoistCardHandler _todoistCardHandler;
        private readonly ITodoistRequestValidator _todoistRequestValidator;

        public TodoistController(
            ILogger<TodoistController> logger,
            ITodoistCardHandler todoistCardHandler,
            ITodoistRequestValidator todoistRequestValidator
        )
        {
            _logger = logger;
            _todoistCardHandler = todoistCardHandler;
            _todoistRequestValidator = todoistRequestValidator;
        }

        [HttpPost("process")]
        public async Task<ActionResult> Process([FromBody] TodoistRequest request)
        {
            _logger
                .LogInformation("Received request");

            if (!await
                    _todoistRequestValidator
                        .IsValidAsync(Request))
                throw new Exception("Authentication failed for Todoist request.");

            var response =
                await
                    _todoistCardHandler
                        .ProcessAsync(request);

            _logger
                .LogInformation("Processed request");

            return
                new OkObjectResult(response);
        }
    }
}