using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctionDemo.Models
{
    public class RepoViewCount
    {
        public string RepoName { get; set; }
        public int ViewCount { get; set; }
    }
}
