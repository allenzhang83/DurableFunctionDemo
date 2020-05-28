using AutofacOnFunctions.Services.Ioc;
using DurableFunctionDemo.Models;
using DurableFunctionDemo.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DurableFunctionDemo.ActivityFunctions
{
    public class GetRepositoryViewCountCore
    {
        private readonly IGitHubApiService _gitHubApiService;

        public GetRepositoryViewCountCore(IGitHubApiService gitHubApiService)
        {
            _gitHubApiService = gitHubApiService;
        }

        [FunctionName(nameof(GetRepositoryViewCountCore))]
        public async Task<RepoViewCount> Run(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger log)
        {
            var repoName = context.GetInput<string>();
            return await _gitHubApiService.GetRepositoryViewCount(repoName);
        }
    }
}
