using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Bot.Models;
using Bot.Models.Bot.Models;

namespace Bot.Repos
{
    public class GitHubServices
    {
        private const string Dotnet = "C#";
        private readonly HttpClient _httpClient;
        string restFullRote = "takenet/repos";

        public GitHubServices(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.github.com/orgs/");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ppedroovictorr");

        }


        string _avatarUrl = "";

        public async Task<User> GetResults()
        {
            var results = await GetAvatar();
            var repos = await GetOlderRepos();
            results.Repositories = repos;


            return results;
        }

        private List<Repository> SortReposAsync(List<Repository> repos)
        {
            var dotnetRepositories = repos.Where(r => !string.IsNullOrEmpty(r.Language) && r.Language.Equals(Dotnet)).ToList();
            var reposByDate = dotnetRepositories.OrderBy(d => d.Created_at).Take(5).ToList();
            return reposByDate;

        }

        private async Task<User> GetAvatar()
        {
            try
            {
                var userResponse = await _httpClient.GetFromJsonAsync<User>("takenet");
                return userResponse;
            }
            catch (Exception ex)
            {
                string msg = "Error: " + ex.Message;
                throw new Exception(msg);
            }
        }

        private async Task<List<Repository>> GetOlderRepos()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Repository>>(restFullRote);
                var reposByDate = SortReposAsync(response);
                return reposByDate;
            }
            catch (Exception ex)
            {
                string msg = "Error: " + ex.Message;
                throw new Exception(msg);
            }
        }
    }
}