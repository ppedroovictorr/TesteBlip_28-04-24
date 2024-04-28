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

        public GitHubServices(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.github.com/orgs/");
        }
        public async Task<User> GetAvatar()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ppedroovictorr");
                var userResponse = await _httpClient.GetFromJsonAsync<User>("takenet");
                return userResponse;
            }
            catch (Exception ex)
            {
                string msg = "Error: " + ex.Message;
                throw new Exception(msg);
            }
        }

        public async Task<List<Repository>> GetOlderRepos()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ppedroovictorr");
                var userResponse = await _httpClient.GetFromJsonAsync<User>("takenet");
                var response = await _httpClient.GetFromJsonAsync<List<Repository>>("takenet/repos");
                var dotnetRepositories = response.Where(r => !string.IsNullOrEmpty(r.Language) && r.Language.Equals(Dotnet)).ToList();
                var reposByDate = dotnetRepositories.OrderBy(d => d.Created_at).Take(5).ToList();
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