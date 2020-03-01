using Finance.Core.Domain;
using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.User;
using Finance.Infrastructure.Services;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Users
{
    class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(command.Email, command.Password, command.Username, (int)UserRole.user);
        }
    }
}
