using Finance.Core.Domain;
using Finance.Core.Domain.Extensions;
using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.User;
using Finance.Infrastructure.Extensions;
using Finance.Infrastructure.Services;
using Finance.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Users
{
    class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;
        private IHandler _handler;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache cache, IHandler handler)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
            _handler = handler;
        }

        public async Task HandleAsync(Login command)
        {
            await _handler
            .RunAsync(async () => await _userService.LoginAsync(command.Email, command.Password))
            .Next()
            .RunAsync( async () =>
            {
                var user = await _userService.GetUserAsync(command.Email);
                var jwt = _jwtHandler.CreateToken(user.UserId, ((UserRole)user.RoleId).ToString());
                _cache.SetJwt(command.TokenId, jwt);
            })
            .ExecuteAsync();;
        }
    }
}
