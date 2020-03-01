using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.User;
using Finance.Infrastructure.DTO;
using Finance.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Users
{
    public class GetUserHandler : ICommandHandler<GetUser>
    {
        private readonly IUserService _userService;

        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(GetUser command)
        {
           await Task.CompletedTask;
        }
    }
}
