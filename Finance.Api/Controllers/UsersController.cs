using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.User;
using Finance.Infrastructure.Database;
using Finance.Infrastructure.Extensions;
using Finance.Infrastructure.Services;
using Finance.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Finance.Api.Controllers
{
    [Route("Users")]
    public class UsersController : ApiControllerBase
    {

        private readonly IUserService _userService;
        private readonly IEncrypter _encrypter;
        private readonly IMemoryCache _cache;


        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, IEncrypter encrypter, IMemoryCache cache ,GeneralSettings settings) : base(commandDispatcher)
        {
            _userService = userService;
            _encrypter = encrypter;
            _cache = cache;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUser command)
        {
            await DispatchAsync(command);

            return Created($"users/{command.Email}", new object());

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var jwt = _cache.GetJwt(command.TokenId);

            return Ok(Json(jwt));
        }

       
    }
}
