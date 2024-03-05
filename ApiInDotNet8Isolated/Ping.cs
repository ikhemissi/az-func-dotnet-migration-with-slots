using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ApiInDotNet8Isolated
{
    public class Ping
    {
        private readonly ILogger<Ping> _logger;

        public Ping(ILogger<Ping> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Ping))]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation(".NET8.0 isolated function processed a request.");

            return new OkObjectResult("PONG .NET8.0");
        }
    }
}
