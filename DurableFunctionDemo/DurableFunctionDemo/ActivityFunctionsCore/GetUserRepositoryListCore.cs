using AutofacOnFunctions.Services.Ioc;
using DurableFunctionDemo.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DurableFunctionDemo.ActivityFunctions
{
    public class GetUserRepositoryListCore
    {
        private readonly IGitHubApiService _gitHubApiService;

        public GetUserRepositoryListCore(IGitHubApiService gitHubApiService)
        {
            _gitHubApiService = gitHubApiService;
        }

        [FunctionName(nameof(GetUserRepositoryListCore))]
        public async Task<List<string>> Run(
            [ActivityTrigger] IDurableActivityContext context,
            [Inject] IGitHubApiService gitHubApiService,
            ILogger log)
        {
            return await _gitHubApiService.GetUserRepositoryList();
        }
    }
}
