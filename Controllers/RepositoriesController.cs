using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using Bot.Models;
using Bot.Repos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private readonly GitHubServices _services;

        public RepositoriesController(GitHubServices services)
        {
            _services = services;
        }


        // GET: api/<RepositoriesController>
        [HttpGet("OlderRepository")]
        public async Task<ActionResult<IEnumerable<Repository>>> GetOldersAsync()
        {
            try
            {
                var response = await _services.GetAvatar();
                var repos = await _services.GetOlderRepos();
                response.Repositories = repos;
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
