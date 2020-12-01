using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_provisionador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;
        private readonly GraphApiService _graphApiClient;

        public UserController(ILogger<UserController> logger, GraphApiService graphApiClient)
        {
            _logger = logger;
            _graphApiClient = graphApiClient;
        }

        [HttpGet]
        public async Task<IList<UserDto>> Get()
        {

            var users = await _graphApiClient.GetGraphApiUser()
                .ConfigureAwait(false);
            return users.Select(x=> new UserDto { DisplayName = x.DisplayName }).ToList();
        }
    }
}
