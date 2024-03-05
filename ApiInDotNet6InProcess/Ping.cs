using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ApiInDotnet6InProcess
{
    public static class Ping
    {
        [FunctionName(nameof(Ping))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation(".NET6.0 in-process function processed a request.");

            return new OkObjectResult("PONG .NET6.0");
        }
    }
}
