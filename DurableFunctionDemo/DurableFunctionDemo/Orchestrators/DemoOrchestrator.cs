using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DurableFunctionDemo.ActivityFunctions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunctionDemo.Orchestrators
{
    public static class DemoOrchestrator
    {
        [FunctionName(nameof(DemoOrchestrator))]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>(nameof(HelloActivity), "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(HelloActivity), "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(HelloActivity), "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}