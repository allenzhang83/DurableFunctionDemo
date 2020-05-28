using AutofacOnFunctions.Services.Ioc;
using DurableFunctionDemo.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DurableFunctionDemo.ActivityFunctions
{
    public static class GetUserRepositoryListAutofac
    {
        [FunctionName(nameof(GetUserRepositoryListAutofac))]
        public static async Task<List<string>> Run(
            [ActivityTrigger] IDurableActivityContext context,
            [Inject] IGitHubApiService gitHubApiService,
            ILogger log)
        {
            return await gitHubApiService.GetUserRepositoryList();
        }
    }
}
