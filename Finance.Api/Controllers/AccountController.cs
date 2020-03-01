using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Services;
using Finance.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("Account")]
    public class AccountController : ApiControllerBase
    {

        private readonly IJwtHandler _jwtHandler;

        public AccountController(IJwtHandler jwtHandler, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _jwtHandler = jwtHandler;
        }

        // GET: Users/5
        [HttpGet]
        [Route("token")]
        public IActionResult Get()
        {
            var token = _jwtHandler.CreateToken(1, "user");

            return Json(token);
        }

        [Authorize]
        [HttpGet]
        [Route("authorize")]
        public IActionResult GetAuth()
        {
            return Content("Auth ok!");

        }


    }
}
