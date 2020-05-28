using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DurableFunctionDemo.ActivityFunctions;
using DurableFunctionDemo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctionDemo.Orchestrators
{
    public static class DemoOrchestrator
    {
        [FunctionName(nameof(DemoOrchestrator))]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            var mode = Environment.GetEnvironmentVariable("Mode");
            var activityFunctionNames = mode switch
            {
                "naive" => new List<string> { nameof(GetUserRepositoryList), nameof(GetRepositoryViewCount) },
                "autofac" => new List<string> { nameof(GetUserRepositoryListAutofac), nameof(GetRepositoryViewCountAutofac) },
                _ => new List<string> { nameof(GetUserRepositoryList), nameof(GetRepositoryViewCount) }
            };

            var getUserRepositoryListFunctionName = activityFunctionNames[0];
            var getRepositoryViewCountFunctionName = activityFunctionNames[1];

            var repos = await context.CallActivityAsync<List<string>>(getUserRepositoryListFunctionName, null);
            var list = string.Join(',', repos);
            log.LogInformation($"Repository list: {list}");

            var tasks = new List<Task<RepoViewCount>>();

            // fan-out
            foreach (var repo in repos)
            {
                var task = context.CallActivityAsync<RepoViewCount>(getRepositoryViewCountFunctionName, repo);
                tasks.Add(task);
            }

            // fan-in
            var repoViewCounts = await Task.WhenAll(tasks);

            await context.CallActivityAsync(nameof(ReportRepoViewCount), repoViewCounts);
        }
    }
}