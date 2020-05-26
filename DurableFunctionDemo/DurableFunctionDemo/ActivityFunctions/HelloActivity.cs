using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctionDemo.ActivityFunctions
{
    public static class HelloActivity
    {
        [FunctionName(nameof(HelloActivity))]
        public static string SayHello(
            [ActivityTrigger] string name,
            ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}
