using DurableFunctionDemo.Models;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionDemo.Services
{
    public interface IGitHubApiService
    {
        Task<List<string>> GetUserRepositoryList();
        Task<RepoViewCount> GetRepositoryViewCount(string repoName);
    }
}
