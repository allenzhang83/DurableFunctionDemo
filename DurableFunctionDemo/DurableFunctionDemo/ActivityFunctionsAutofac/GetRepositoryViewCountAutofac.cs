using AutofacOnFunctions.Services.Ioc;
using DurableFunctionDemo.Models;
using DurableFunctionDemo.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DurableFunctionDemo.ActivityFunctions
{
    public static class GetRepositoryViewCountAutofac
    {
        [FunctionName(nameof(GetRepositoryViewCountAutofac))]
        public static async Task<RepoViewCount> Run(
            [ActivityTrigger] IDurableActivityContext context,
            [Inject] IGitHubApiService gitHubApiService,
            ILogger log)
        {
            var repoName = context.GetInput<string>();
            return await gitHubApiService.GetRepositoryViewCount(repoName);
        }
    }
}
