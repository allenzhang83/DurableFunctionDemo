using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableFunctionDemo.Orchestrators;

namespace DurableFunctionDemo.TriggerFunctions
{
    public static class HttpTrigger
    {
        [FunctionName(nameof(HttpTrigger))]
        public static async Task<IActionResult> HttpStart(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [DurableClient] IDurableClient client,
            ILogger log)
        {
            // Function input comes from the request content.
            var instanceId = await client.StartNewAsync(nameof(DemoOrchestrator), null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return new OkObjectResult("");
        }
    }
}
