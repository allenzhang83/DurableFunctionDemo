using DurableFunctionDemo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionDemo.ActivityFunctions
{
    public static class GetRepositoryViewCount
    {
        [FunctionName(nameof(GetRepositoryViewCount))]
        public static async Task<RepoViewCount> Run(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger log)
        {            
            var repoName = context.GetInput<string>();
            var result = new RepoViewCount
            {
                RepoName = repoName
            };

            var username = Environment.GetEnvironmentVariable("GitHubUserName");            
            var password = Environment.GetEnvironmentVariable("GitHubPassword");
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("C# agent");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{username}/{repoName}/traffic/views");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            JObject jObject = JsonConvert.DeserializeObject<JObject>(body);
            result.ViewCount = jObject["count"].Value<int>();            
            return result;
        }
    }
}
