using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace Blazor_AoC
{
    public class GetGithub
    {
        public static string GetStringGithub(string path)
        {
            var github = new GitHubClient(new ProductHeaderValue("Blazor-AoC"));
            var repo = GetRepo(github, "Deynai", "Blazor-AoC");

            return repo.Id.ToString();
        }

        private static async Task<Repository> GetRepo(GitHubClient gh, string owner, string repo_name)
        {
            return await gh.Repository.Get(owner, repo_name);
        }
    }
}
