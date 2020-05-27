using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionDemo.ActivityFunctions
{
    public static class GetUserRepositoryList
    {
        [FunctionName(nameof(GetUserRepositoryList))]
        public static async Task<List<string>> Run(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger log)
        {
            var result = new List<string>();
            var userName = Environment.GetEnvironmentVariable("GitHubUserName");            
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("C# agent");
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/users/{userName}/repos");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            List<JObject> jObjects = JsonConvert.DeserializeObject<List<JObject>>(body);
            foreach (var jObject in jObjects)
            {
                string repoName = jObject["name"].Value<string>();
                result.Add(repoName);
            }
            return result;
        }
    }
}
